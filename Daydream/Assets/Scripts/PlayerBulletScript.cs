using Unity.VisualScripting;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    // Update is called once per frame
    void Awake()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position = transform.position + (transform.up * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("Hurt");
        }

        Destroy(gameObject);
    }

    public void Touching(GameObject collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.SendMessage("Hurt");
            Destroy(gameObject);
        }

    }
}
