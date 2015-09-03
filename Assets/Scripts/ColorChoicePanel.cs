using UnityEngine;
using UnityEngine.EventSystems;

public class ColorChoicePanel : BasePanel
{
    public int index;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnStateChange(int stateID)
    {
        switch (stateID)
        {
            case StateManager.PAUSED:
                isInteractable = false;
                this.SetDisabled();
                break;
            case StateManager.CHOOSE_COLOR:
                isInteractable = true;
                this.SetColor(TrialManager.GetColorChoice(this.index));
                break;
            case StateManager.RESET_COLORS:
                isInteractable = false;
                this.SetDisabled();
                break;
            case StateManager.BEGIN:
                isInteractable = false;
                this.SetDisabled();
                break;
        }
    }

    protected override int GetNextState()
    {
        return StateManager.RESET_COLORS;
    }
}
