using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static int state         = 3;
    public static int nextState     = -1;

    public const int PAUSED         = 0;
    public const int CHOOSE_COLOR   = 1;
    public const int RESET_COLORS   = 2;
    public const int BEGIN          = 3;
    public const int END            = 4;

    public static StateChangeListener StateChangeCallback;
    public delegate void StateChangeListener(int stateID);

    public static void ChangeState(int stateID)
    {
        if (StateManager.nextState == -1)
        {
            StateManager.state = stateID;
            StateManager.nextState = stateID;
            StateChangeCallback(stateID);
        }
        else
        {
            StateManager.nextState = stateID;
        }
    }

    public static void OnStateChangeComplete(int stateID)
    {
        int nextState = StateManager.nextState;

        StateManager.nextState = -1;

        if (nextState != stateID)
        {
            StateManager.ChangeState(nextState);
        }
    }

    public static void Register(IStateful obj)
    {
        StateManager.StateChangeCallback -= StateManager.OnStateChangeComplete;
        StateManager.StateChangeCallback += obj.OnStateChange;
        StateManager.StateChangeCallback += StateManager.OnStateChangeComplete;

        obj.OnStateChange(StateManager.state);
    }
}
