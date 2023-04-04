using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float floatForce;
    [SerializeField] private float walkForce;
    [SerializeField] private float runForce;
    [SerializeField] private float crouchForce;
    [SerializeField] private float noStaminaForce;
    [SerializeField] private float swimForce;
    [SerializeField] private float minStamina;
    [SerializeField] private float maxSwimSpeed;
    [SerializeField] private float slowForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sprintFOV;
    [SerializeField] private float defaultFOV;
    [SerializeField] private float CrouchScale;
    [SerializeField] private float NormalScale;
    [SerializeField] private float crouchRate;
    [SerializeField] private LayerMask groundlayers;
    [SerializeField] private Transform[] GChecks;
    [SerializeField] private Gun[] guns;
    [SerializeField] private GameObject HpBar;
    [SerializeField] private GameObject StaminaBar;
    [SerializeField] private GameObject AmmoImage;
    [SerializeField] private GameObject[] bulletImages;
    [SerializeField] private GameObject ReloadText;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject[] groundSpheres;

    private Vector3 gNrml = Vector3.zero;

    private float stealth = 0f;

    public int pistolammopickup;
    public int mgAmmo;
    public int tomAmmo;

    private float horzin = 0f, vertin = 0f;
    private float speed;
    private float maxSpeed;
    private float gsY;

    private int gunIndex = 0;

    private bool inWater;
    private bool jump;
    private bool crouch;
    private bool isGrounded = true;

    private RaycastHit ray;

    private PlayerCam cam;

    private Rigidbody rb;
    public Collider walkSound;
    private Gun currentGun;
    private StatsSystem stats;
    private MainHub hub;

    void Start()
    {
        hub = FindObjectOfType<MainHub>();
        cam = FindObjectOfType<PlayerCam>();
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<StatsSystem>();

        speed = walkForce;
        maxSpeed = speed * 1.5f;

        currentGun = guns[gunIndex];
        currentGun.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!hub.DisableMouse)
        {
            maxSpeed = speed * 1.5f;

            stealth = transform.localScale.y;

            bool rightMouseButton = Input.GetKey(KeyCode.Mouse1);
            HideObject(rightMouseButton, crossHair, 4f);

            float scrollWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");
            if (scrollWheelInput != 0)
            {
                currentGun.gameObject.SetActive(false);
                gunIndex = scrollWheelInput > 0 ? gunIndex + 1 : gunIndex - 1;
                gunIndex = (gunIndex + guns.Length) % guns.Length;
                currentGun = guns[gunIndex];
                currentGun.gameObject.SetActive(true);
            }

            rb.useGravity = !inWater;
            if (rb.velocity.magnitude > (inWater ? maxSwimSpeed : maxSpeed)) rb.velocity = rb.velocity.normalized * (inWater ? maxSwimSpeed : maxSpeed);

            if (Input.GetKey(KeyCode.LeftShift) && !crouch) speed = runForce;
            else if (crouch) speed = crouchForce;
            else speed = walkForce;

            float staminaChange = rb.velocity.magnitude > walkForce * 1.5f ? rb.velocity.magnitude * 1.25f : rb.velocity.magnitude * 0.25f;
            stats.RemoveStamina(staminaChange);
            if (rb.velocity.magnitude <= 0.001f) stats.RecoverStamina();
            if (stats.GetStamina() <= minStamina) speed = noStaminaForce;

            float targetFOV = (speed == runForce && (horzin != 0 || vertin != 0)) ? sprintFOV : defaultFOV;
            cam.ChangeFOV(runForce, targetFOV);

            jump = Input.GetKey(KeyCode.Space) && !inWater;
            bool shouldCrouch = Input.GetKey(KeyCode.LeftControl) && !jump;

            if (shouldCrouch)
            {
                Crouch(true);
                walkSound.enabled = false;
            }
            else
            {
                Crouch(false);
                walkSound.enabled = true;
            }

            horzin = Input.GetAxisRaw("Horizontal");
            vertin = Input.GetAxisRaw("Vertical");

            if (HpBar != null) HpBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, stats.GetHp() - 96, 0);
            if (StaminaBar != null) StaminaBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(stats.GetStamina() - 96, 0, 0);
        }
        else
        {
            HideObject(true, crossHair, 4f);
        }
        for (int i=0;i<GChecks.Length;i++)
        {
            Physics.Raycast(GChecks[i].transform.position, -transform.up, out ray);
            groundSpheres[i].transform.position = ray.point;
        }
    }

    private void FixedUpdate()
    {
        // if in water move the player in the direction of the camera using the swim force
        if (inWater) rb.AddForce((vertin * cam.transform.forward + horzin * cam.transform.right) * swimForce);
        // if not in the water and on the ground move the player 
        // speed is used for forwards movement
        // walk speed or crouch speed is used for side to side movement
        else if (CheckGround()) rb.AddForce(((Mathf.Abs(vertin) > 0.001f ? vertin : 0f) * 10f * speed * GetMoveDir() + (Mathf.Abs(horzin) > 0.001f ? horzin : 0f) * 10f * (speed != runForce ? speed : walkForce) * GetMoveDirRight()));
        else rb.AddForce(0.1f * (speed * vertin * transform.forward + horzin * (speed != runForce ? speed : walkForce) * cam.transform.right));

        if (CheckGround() && !inWater && jump) Jump();

        // adds upward force if in water and not moving down
        if (inWater && Vector3.Dot(Vector3.one, cam.transform.forward) < 3f) rb.AddForce(floatForce * transform.up);

        // Slow movement in water
        //if (horzin == 0 && r.magnitude > 0.1f && inWater) rb.velocity += slowForce * Time.deltaTime * -r.normalized;
        //if (vertin == 0 && f.magnitude > 0.1f && inWater) rb.velocity += slowForce * Time.deltaTime * -f.normalized;
        //if (vertin == 0 && horzin == 0 && u.magnitude > 0.1f && inWater) rb.velocity += slowForce * Time.deltaTime * -u.normalized;

        // stop the player if moving slow enough
        if (rb.velocity.magnitude <= 0.1f) rb.velocity = Vector3.zero;
    }
    private void Jump()
    {
        // adds an upwards force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
    private void Crouch(bool state)
    {
        Vector3 temp;
        crouch = state;
        if (crouch) temp = new Vector3(1f, CrouchScale, 1f);
        else temp = new Vector3(1f, NormalScale, 1f);
        transform.localScale = Vector3.Lerp(transform.localScale, temp, Time.deltaTime * crouchRate);
    }
    private void HideObject(bool state, GameObject obj, float rate)
    {
        Vector3 temp;
        if (state) temp = new Vector3(0f, 0f, 0f);
        else temp = new Vector3(1f, 1f, 1f);
        obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, temp, Time.deltaTime * rate);
    }
    private Vector3 GetMoveDir()
    {
        // gets the move direction of the player based on the slope of the surface they are standing on
        if (CheckGround()) return Vector3.ProjectOnPlane(transform.forward, ray.normal);
        else return transform.forward;
    }
    private Vector3 GetMoveDirRight()
    {
        // gets the right direction vector based on the move direction
        return -Vector3.Cross(GetMoveDir(), gNrml).normalized;
    }
    private bool CheckGround()
    {
        return isGrounded;
    }
    public void Die()
    {
        // Kills player
        Time.timeScale = 0f;
        SceneManager.LoadScene(2);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            stats.TakeDamage(collision.gameObject.GetComponentInParent<Enemy>().dmg);
            Debug.Log("Got hit");
        }
        if (collision.gameObject.CompareTag("Clue"))
        {
            hub.SetJournal(collision.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            stats.TakeDamage(collision.gameObject.GetComponentInParent<Enemy>().dmg * Time.deltaTime);
            Debug.Log("Got hit");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 4) inWater = true;
        else inWater = false;
        if (other.gameObject.layer == 6)
        {
            isGrounded = true;
            gNrml = other.transform.up;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4) inWater = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) isGrounded = true;
        if (other.transform.tag == "PistolAmmo")
        {
            Destroy(other.gameObject);
            currentGun.collectAmmo(pistolammopickup);
        }
        else if (other.transform.tag == "mgAmmo")
        {
            Destroy(other.gameObject);
            currentGun.collectAmmoMG(mgAmmo);
        }
        else if (other.transform.tag == "TomAmmo")
        {
            Destroy(other.gameObject);
            currentGun.collectAmmoTommy(tomAmmo);
        }

    }
    public float CurrentStealth
    {
        get { return stealth; }
        set { stealth = value; }
    }
}
