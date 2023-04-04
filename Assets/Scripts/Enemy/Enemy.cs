using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float meleeDmg = 10f;
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTimer = 5f;
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float wanderSpeed = 2f;
    [SerializeField] private float lookAngle = 45f;
    [SerializeField] private float lookDistance = 10f;

    private float currentHp = 0f;

    private NavMeshAgent agent;
    private float timer;
    private Player player;
    private MainHub hub;
    void Start()
    {
        currentHp = maxHp;
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // check if player is in line of sight
        if (InLineOfSight() || Vector3.Distance(transform.position, player.transform.position) < minDistance)
        {
            // chase player
            ChasePlayer();
        }
        else
        {
            // wander around randomly
            Wander();
        }
    }

    bool InLineOfSight()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (Vector3.Angle(direction, transform.forward) < lookAngle)
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, lookDistance))
            {
                if (hit.collider.GetComponent<Player>() != null || hit.collider.GetComponentInParent<Player>() != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        _ = agent.SetDestination(player.transform.position);
        transform.forward = (new Vector3(player.transform.position.x, 0f, player.transform.position.z) - transform.position).normalized;
    }

    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        agent.speed = wanderSpeed;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;
        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);

        return navHit.position;
    }
    public void TakeDamage(float d)
    {
        if (currentHp - d <= 0)
        {
            currentHp = 0;
            Die();
        }
        else currentHp -= d;
        //hub.EnemiesKilled++;
    }
    private void Die()
    {
        currentHp = maxHp;
        gameObject.SetActive(false);
    }

    public float dmg
    {
        get { return meleeDmg; }
    }
}
