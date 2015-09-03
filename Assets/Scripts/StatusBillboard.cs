using UnityEngine;

public class StatusBillboard : Complex, IStateful
{
    protected void Start()
    {
        StateManager.Register(this);
    }

    public void OnStateChange(int stateID)
    {
        switch (stateID)
        {
            case StateManager.PAUSED:
                this.textMesh.text = "PAUSED";
                this.textMesh.gameObject.SetActive(true);
                break;
            case StateManager.BEGIN:
                this.textMesh.text = "look →";
                this.textMesh.gameObject.SetActive(true);
                break;
            case StateManager.END:
                this.textMesh.text = "END";
                this.textMesh.gameObject.SetActive(true);
                break;
            case StateManager.CHOOSE_COLOR:
            case StateManager.RESET_COLORS:
                this.textMesh.gameObject.SetActive(false);
                break;
        }
    }
}
