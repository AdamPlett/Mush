using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIBasicController", menuName = "ScriptableObjects/InputControllers/AIBasic")]
public class AIBasicController : InputController
{
    private bool count=false;
    public override bool RetrieveJumpInput()
    {
        if (count==false)
        {
            count = true;
            return count;
        }
        count = false;
        return count;
    }

    public override float RetrieveMoveInput()
    {
        return 1f;
    }

    public override float RetrieveSprintInput()
    {
        return 0f;
    }

    public override bool RetrieveDashInput()
    {
        return false;
    }
}
