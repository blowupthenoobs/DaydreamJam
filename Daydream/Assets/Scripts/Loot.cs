using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Loot : MonoBehaviour
{
    // Update is called once per frame
    public int value; // Value of the loot orb, used for scoring
    public float scale; // Scale of the loot orb, used for visual size

    [Header("Homing Settings")]
    public float homingRange; // range within which the orb will start homing towards the player

    private Transform player;
    private Rigidbody2D rb;

    [SerializeField] float decellerationSpeed;
    [SerializeField] float maxSpurtDist;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = EnemyScript.target.transform;

        SpurtFromEnemy();
    }

    void FixedUpdate()
    {
        if (player == null) return; // If the player doesn't exist, do nothing
        Vector2 orbPos = transform.position;
        Vector2 playerPos = player.position;
        playerPos += Vector2.down; // Offset player position slightly to avoid z-fighting
        float dist = Vector2.Distance(orbPos, playerPos);

        if (dist <= homingRange)
        {
            Vector2 dir = (playerPos - orbPos).normalized;

            float speed = (homingRange) / dist; // Interpolate speed based on distance            

            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, dir * speed, decellerationSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, new Vector2(), decellerationSpeed);
        }
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
            collider.SendMessage("Heal", value);

        Destroy(gameObject);
    }
    
    protected void SpurtFromEnemy()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        float speed = Random.Range(0.2f, maxSpurtDist);

        float x = Mathf.Cos(angle) * speed;
        float y = Mathf.Sin(angle) * speed;

        rb.linearVelocity = new Vector2(x, y);
    }
}
