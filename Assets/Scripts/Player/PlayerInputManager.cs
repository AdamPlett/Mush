using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Input Action Map")]
    [SerializeField] private string actionMapName = "PlayerBasic";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string dash = "Dash";

    //actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction dashAction;

    //refrences to actions current values
    public Vector2 MoveInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; }
    public bool DashTriggered { get; private set; }


    public static PlayerInputManager Instance { get; private set; }
    private void Awake()
    {
        //if there is Instance of static PlayerInputManager already then make it this GameObject and make it a singleton
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //if there is already an Instance of static PlayerInputManager than destroy this GameObject
        else
        {
            Destroy(gameObject);
        }

        //finds the actions from the input action asset
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);

        //subscribes the action logic to the actions
        RegisterInputActions();

    }

    //subscribes the action logic to the actions
    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        jumpAction.started += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;

        dashAction.started += context => DashTriggered = true;
        dashAction.canceled += context => DashTriggered = false;
    }

    //Allows for actions when gameObject is enabled
    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        dashAction.Enable();
    }

    //stops actions when gameObject is disabled
    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        dashAction.Disable();
    }
}
