using UnityEngine;
using UnityEngine.EventSystems;

public class ResetPanel : BasePanel
{
    protected override void Start()
    {
        base.Start();
    }

    public override void OnStateChange(int stateID)
    {
        switch (stateID)
        {
            case StateManager.PAUSED:
            case StateManager.CHOOSE_COLOR:
            case StateManager.RESET_COLORS:
            case StateManager.END:
                isInteractable = true;
                break;
            case StateManager.BEGIN:
                isInteractable = false;
                break;
        }
    }

    protected override int GetNextState()
    {
        return StateManager.BEGIN;
    }
}
