using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] GameObject bloodDrop;
    [SerializeField] GameObject player;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        EnemyScript.playerLayer = playerLayer;
        EnemyScript.wallLayer = wallLayer;
        EnemyScript.bloodDrop = bloodDrop;
        EnemyScript.target = player;
    }
}
