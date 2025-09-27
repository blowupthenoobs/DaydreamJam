using UnityEngine;

public class GhostScript : EnemyScript
{
    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "playerProjectile")
            collider.SendMessage("Touching", gameObject, SendMessageOptions.DontRequireReceiver);
        else if(collider.tag == "Player")
            collider.SendMessage("Hurt", damage);

        Die();
    }
}
