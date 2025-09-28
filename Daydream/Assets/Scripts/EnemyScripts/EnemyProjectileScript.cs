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
        collision.gameObject.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);

        Destroy(gameObject);
    }
}
