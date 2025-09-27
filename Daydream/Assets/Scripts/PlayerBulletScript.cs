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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("Hurt");
        }

        Destroy(gameObject);
    }
}
