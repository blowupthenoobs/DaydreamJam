using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private int damage;

    public void SetUpBullet(int setDamage)
    {
        damage = setDamage;
    }

    void Update()
    {
        transform.position = transform.position + (transform.up * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("Hurt", damage);
        }

        Destroy(gameObject);
    }
}
