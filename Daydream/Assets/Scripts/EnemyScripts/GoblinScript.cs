using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GoblinScript : EnemyScript
{
    private bool charging;
    [SerializeField] float chargeSpeed;
    private float currentSpeed;
    [SerializeField] float decellerationSpeed;
    private Vector3 chargeDirection;

    void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (!charging)
        {
            if (PlayerInLOS())
            {
                if(Vector2.Distance(transform.position, target.transform.position) > followDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                }
                if(Vector2.Distance(transform.position, target.transform.position) < attackRange)
                {
                    Attack();
                }
            }
            else
                Wander();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (chargeDirection * 100), currentSpeed * Time.deltaTime);
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, decellerationSpeed * Time.deltaTime);

            if(currentSpeed <= 5)
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, decellerationSpeed * Time.deltaTime);
            if(currentSpeed <= 2)
                charging = false;
        }
    }

    protected void Attack()
    {
        if (currentCooldown >= cooldown)
        {
            charging = true;
            currentSpeed = chargeSpeed;
            chargeDirection = (target.transform.position - transform.position).normalized;
            currentCooldown = 0;
        }
        else
            currentCooldown += Time.deltaTime;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            collision.gameObject.SendMessage("Hurt", damage);
        charging = false;
    }
}
