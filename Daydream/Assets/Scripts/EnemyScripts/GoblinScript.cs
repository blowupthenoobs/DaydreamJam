using UnityEngine;

public class GoblinScript : EnemyScript
{
    protected void Move()
    {
        if (PlayerInLOS())
        {
            if (Vector2.Distance(transform.position, target.transform.position) > followDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, target.transform.position) < attackRange)
            {
                Attack();
            }
        }
        else
            Wander();
    }

    protected void Attack()
    {
        
    }
}
