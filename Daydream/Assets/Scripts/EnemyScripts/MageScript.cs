using UnityEngine;

public class MageScript : EnemyScript
{
    [SerializeField] GameObject MagicBullet;
    
    public int cowerDistance;
    private bool isStrafing;
    private Vector2 strafePos;
    [SerializeField] float strafeDist;


    [SerializeField] float bulletSpawnDist;
    void Update()
    {
        Move();
    }

    protected void Move()
    {
        if(isStrafing)
        {
            if(Vector2.Distance(transform.position, target.transform.position) < cowerDistance)
                isStrafing = false;
            else
                transform.position = Vector2.MoveTowards(transform.position, strafePos, moveSpeed*Time.deltaTime);

            if((Vector2)transform.position == strafePos)
                isStrafing = false;        
        }
        else if(PlayerInLOS())
        {
            
            if(Vector2.Distance(transform.position, target.transform.position) < cowerDistance)
            {
                Vector2 distDifference = (transform.position - target.transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + distDifference, moveSpeed*Time.deltaTime);
            }
            if(Vector2.Distance(transform.position, target.transform.position) > followDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed*Time.deltaTime);
            }
            if(Vector2.Distance(transform.position, target.transform.position) < attackRange)
            {
                Attack();
            }
        }
        else
            Wander();
    }


    protected void Attack()
    {
        if (currentCooldown >= cooldown)
        {
            ShootProjectile(MagicBullet, bulletSpawnDist);
            currentCooldown = 0;

            Strafe();
        }
        else
            currentCooldown += Time.deltaTime;
    }
    
    private void Strafe()
    {
        isStrafing = true;
        Vector2 randomPosition = new Vector2();

        bool withinCombatRange = false;

        while(!withinCombatRange)
        {
            randomPosition = GetRandomPositionWithinRadius(transform.position, strafeDist);

            if(Vector2.Distance(randomPosition, target.transform.position) < attackRange && Vector2.Distance(randomPosition, target.transform.position) > cowerDistance)
                withinCombatRange = true;
        }

        strafePos = randomPosition;
        
    }
}
