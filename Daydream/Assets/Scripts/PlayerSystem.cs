using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    protected PlayerID playerID;
    private Player player;
    [HideInInspector] public bool selected;

    protected virtual void Awake()
    {
        player = transform.GetComponent<Player>();

        playerID = player.player;
    }
}
