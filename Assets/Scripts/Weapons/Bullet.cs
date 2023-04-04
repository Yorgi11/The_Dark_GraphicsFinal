using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private GameObject point;
    private float damage;
    private float headshot;
    private float range;
    private float impactForce;

    private Vector3 lastPos;
    private Vector3 dir;

    private RaycastHit ray;

    void Awake()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        if (IsInitialized())
        {
            dir = transform.position - lastPos;
            if (Physics.Raycast(lastPos, dir.normalized, out ray, dir.magnitude))
            {
                HandleCollision();
                Destroy(gameObject);
            }
            lastPos = transform.position;
        }
    }

    private bool IsInitialized()
    {
        return damage != 0 && headshot != 0 && range != 0 && impactForce != 0;
    }

    private void HandleCollision()
    {
        DamagableObject dmgObj = ray.collider.GetComponent<DamagableObject>();
        if (dmgObj != null)
        {
            if (ray.collider.GetComponentInParent<Enemy>() != null)
            {
                SpawnHitMarker();
            }
            dmgObj.DoDamage(damage, headshot);
        }

        if (ray.collider.CompareTag("LevelPart"))
        {
            SpawnBulletHole();
        }

        Rigidbody rb = ray.collider.GetComponent<Rigidbody>() ?? ray.collider.GetComponentInParent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForceAtPosition(impactForce * dir.normalized, ray.point, ForceMode.Impulse);
        }
    }

    private void SpawnHitMarker()
    {
        GameObject marker = Instantiate(hitMarker, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, GameObject.FindGameObjectWithTag("HitMarker").transform);
        Destroy(marker, 0.125f);
    }

    private void SpawnBulletHole()
    {
        Vector3 holePos = ray.point + ray.normal * 0.025f;
        Quaternion holeRot = Quaternion.LookRotation(-ray.normal);

        GameObject pos = Instantiate(point, holePos, holeRot, ray.collider.gameObject.transform);
        GameObject hole = Instantiate(bulletHole, pos.transform.position, holeRot);
        hole.GetComponent<BulletHole>().SetPos(pos.transform);

        Destroy(hole, 5f);
        Destroy(pos, 5.1f);
    }

    public void SetDamage(float d) => damage = d;
    public void SetHeadShot(float h) => headshot = h;
    public void SetRange(float r) => range = r;
    public void SetImpactForce(float f) => impactForce = f;

}
