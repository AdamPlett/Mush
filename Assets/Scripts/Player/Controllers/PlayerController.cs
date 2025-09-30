using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "ScriptableObjects/InputControllers/Player")]
public class PlayerController : InputController
{
    private PlayerInputManager inputManager;


    //doesnt work with on enable or awake
    private void OnEnable()
    {
        inputManager = PlayerInputManager.Instance;
    }
    public override bool RetrieveJumpInput()
    {
        if (inputManager == null)
        {
            Debug.Log("input manager was null");
            inputManager = PlayerInputManager.Instance;
        }
        //returns the jump triggered bool from player input manager or returns false if the player input mananager is null
        return inputManager != null ? inputManager.JumpTriggered : false;
    }
    public override float RetrieveMoveInput()
    {
        if (inputManager==null)
        {
            Debug.Log("input manager was null");
            inputManager = PlayerInputManager.Instance;
        }

        //returns the move input from player input manager or returns false if the player input mananager is null
        return inputManager != null ? inputManager.MoveInput.x : 0f;
    }
    public override float RetrieveSprintInput()
    {
        return inputManager.SprintValue;
    }

    public override bool RetrieveDashInput()
    {
        if (inputManager == null)
        {
            Debug.Log("input manager was null");
            inputManager = PlayerInputManager.Instance;
        }
        //returns the dash triggered bool from player input manager or returns false if the player input mananager is null
        return inputManager != null ? inputManager.DashTriggered : false;
    }
}
