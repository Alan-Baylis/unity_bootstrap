using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayPanel : BasePanel
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
            case StateManager.END:
                isInteractable = false;
                this.textMesh.gameObject.SetActive(false);
                this.SetDisabled();
                break;
            case StateManager.CHOOSE_COLOR:
                isInteractable = false;
                this.textMesh.gameObject.SetActive(false);
                this.SetColor(TrialManager.GetDisplayColor());
                break;
            case StateManager.RESET_COLORS:
                isInteractable = true;
                this.textMesh.text = "look for this color";
                this.textMesh.gameObject.SetActive(true);
                this.SetColor(TrialManager.GetDisplayColor());
                break;
            case StateManager.BEGIN:
                isInteractable = true;
                this.textMesh.text = "look here to start";
                this.textMesh.gameObject.SetActive(true);
                this.SetDisabled();
                break;
        }
    }

    protected override int GetNextState()
    {
        return StateManager.CHOOSE_COLOR;
    }
}
