using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    [SerializeField] LayerMask playerLayer;
    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
            
        EnemyScript.playerLayer = playerLayer;
    }
}
