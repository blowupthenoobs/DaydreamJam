using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSystem : MonoBehaviour
{
    protected PlayerID playerID;
    private Player player;
    [HideInInspector] public bool selected;
    protected InputActionAsset inputSystem;

    protected InputAction movementActions;
    protected InputAction dashAction;

    protected virtual void Awake()
    {
        player = transform.GetComponent<Player>();

        playerID = player.player;

        movementActions = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Dash");
    }

    protected void OnEnable()
    {
        inputSystem = player.inputSystem;

        inputSystem.FindActionMap("Player").Enable();
    }

    protected void OnDisable()
    {
        inputSystem.FindActionMap("Player").Disable();
    }
}
