using UnityEngine;

public class ScoreCrystalScript : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int score;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "playerProjectile")
        {
            hp--;

            if(hp <= 0)
                DestroyCrystal();
        }
    }

    private void DestroyCrystal()
    {
        GameManagerScript.Instance.UpdateScore(score);
        Destroy(gameObject);
    }
}
