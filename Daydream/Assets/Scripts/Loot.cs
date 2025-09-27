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
    public float homingRange = 5f;           // range within which the orb will start homing towards the player

    private Transform player;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

            rb.linearVelocity = dir * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
            collider.SendMessage("Heal", value);

        Destroy(gameObject);
    }
}
