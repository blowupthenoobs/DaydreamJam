using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static GameObject target;


    [SerializeField] protected float moveSpeed;

    public float attackRange;
    [SerializeField] protected float followDistance;
    [SerializeField] protected float visionDistance;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;

    public static LayerMask wallLayer;
    public static LayerMask playerLayer;

    public float cooldown;
    protected float currentCooldown;

    //Wandering variables
    public float maxWanderCooldown;
    public int wanderIntervalAmount;
    private float currentWanderCooldown;
    public float maxWanderDist;
    private List<Vector2> wanderPoints = new();


    public void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    public bool PlayerInLOS()
    {
        bool targetInSight = false;

        var targetNearby = Physics2D.OverlapCircle(transform.position, visionDistance, playerLayer);

        if (targetNearby != null)
        {
            var Direction = (target.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, visionDistance, wallLayer);
            Debug.DrawRay(transform.position, Direction * visionDistance, Color.red);

            targetInSight = (hit.collider == null);

            currentWanderCooldown = 0;
            wanderPoints.Clear();
        }

        return targetInSight;
    }

    protected virtual void Wander()
    {
        if (wanderPoints.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, wanderPoints[0], moveSpeed * Time.deltaTime);

            if ((Vector2)transform.position == wanderPoints[0])
                wanderPoints.RemoveAt(0);
        }
        else if (currentWanderCooldown >= maxWanderCooldown)
        {
            currentWanderCooldown = 0;

            for (int i = 0; i < wanderIntervalAmount; i++)
            {
                if (wanderPoints.Count == 0)
                    wanderPoints.Add(GetRandomPositionWithinRadius(transform.position, maxWanderDist));
                else
                    wanderPoints.Add(GetRandomPositionWithinRadius(wanderPoints[i - 1], maxWanderDist));
            }
        }
        else
        {
            currentWanderCooldown += Time.deltaTime;
        }
    }

    protected Vector2 GetRandomPositionWithinRadius(Vector2 center, float radius)
    {
        // Generate a random angle between 0 and 360 degrees
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Generate a random distance within the radius
        float distance = Random.Range(0f, radius);

        // Calculate the x and y offset using the angle and distance
        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        // Calculate the final position by adding the offset to the center position
        Vector2 randomPosition = new Vector2(center.x + x, center.y + y);

        return randomPosition;
    }

    public void Hurt()
    {
        health--;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "playerProjectile")
            collider.SendMessage("Touching", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    public void ShootProjectile(GameObject projectile)
    {
        var Direction = (target.transform.position - transform.position).normalized;

        GameObject bullet = Instantiate(projectile, transform.position + (Direction * .5f), transform.rotation);
    }
}
