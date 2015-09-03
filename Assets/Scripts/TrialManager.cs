using UnityEngine;

public class TrialManager : MonoBehaviour, IStateful
{
    protected static Color[] colors = new Color[] {
        new Color(255f/255f,0f/255f,0f/255f,1f),
        new Color(255f/255f,255f/255f,0f/255f,1f),
        new Color(0f/255f,255f/255f,0f/255f,1f),
        new Color(0f/255f,255f/255f,255f/255f,1f),
        new Color(0f/255f,0f/255f,255f/255f,1f),
        new Color(255f/255f,0f/255f,255f/255f,1f),

        new Color(127f/255f,0f/255f,0f/255f,1f),
        new Color(127f/255f,127f/255f,0f/255f,1f),
        new Color(0f/255f,127f/255f,0f/255f,1f),
        new Color(0f/255f,127f/255f,127f/255f,1f),
        new Color(0f/255f,0f/255f,127f/255f,1f),
        new Color(127f/255f,0f/255f,127f/255f,1f),

        new Color(0f/255f,127f/255f,255f/255f,1f),
        new Color(0f/255f,255f/255f,127f/255f,1f),
        new Color(255f/255f,0f/255f,127f/255f,1f),
    };

    protected static int[] displayColorIndices = new int[] {
        7, 2, 10, 3, 5, 1, 4, 11, 6, 13, 0, 12, 14, 9, 8
    };
    protected static int[][] panelColorIndices = new int[][] {
        new int[] {6, 7, 8, 0, 11, 5, 13, 1, 9, 3, 12, 4, 10, 14, 2},
        new int[] {8, 11, 3, 12, 0, 9, 6, 1, 7, 14, 4, 5, 13, 2, 10},
        new int[] {14, 6, 10, 7, 3, 1, 4, 13, 9, 5, 2, 12, 11, 8, 0},
        new int[] {0, 14, 11, 3, 7, 13, 10, 6, 4, 8, 2, 1, 12, 9, 5},
        new int[] {1, 12, 14, 6, 5, 4, 9, 13, 2, 7, 10, 11, 0, 3, 8},
        new int[] {5, 1, 13, 14, 4, 0, 9, 8, 2, 6, 10, 12, 7, 3, 11},
        new int[] {10, 9, 8, 12, 5, 4, 13, 3, 0, 2, 1, 14, 11, 7, 6},
        new int[] {1, 0, 5, 13, 12, 7, 8, 2, 10, 9, 3, 4, 11, 14, 6},
        new int[] {3, 13, 1, 0, 14, 11, 6, 9, 7, 8, 12, 4, 2, 5, 10},
        new int[] {8, 11, 6, 12, 0, 4, 7, 10, 5, 3, 1, 13, 14, 9, 2},
        new int[] {10, 12, 4, 6, 3, 0, 8, 7, 14, 11, 5, 1, 2, 9, 13},
        new int[] {13, 8, 4, 0, 5, 14, 2, 3, 9, 10, 12, 7, 6, 11, 1},
        new int[] {14, 1, 5, 0, 11, 12, 2, 4, 3, 9, 7, 8, 10, 6, 13},
        new int[] {14, 13, 3, 4, 9, 5, 12, 6, 11, 2, 0, 8, 10, 1, 7},
        new int[] {6, 8, 9, 10, 5, 2, 14, 4, 11, 3, 13, 12, 7, 0, 1},
        new int[] {11, 14, 10, 12, 0, 6, 2, 9, 5, 8, 3, 1, 4, 13, 7}
    };

    protected static int trialNumber;
    [SerializeField]
    protected int maxTrials;
    public static float gazeInterval = 1f;

    protected void Start()
    {
        StateManager.Register(this);
    }

    public void OnStateChange(int stateID)
    {
        switch (stateID)
        {
            case StateManager.PAUSED:
                break;
            case StateManager.CHOOSE_COLOR:
                break;
            case StateManager.RESET_COLORS:
                if (this.maxTrials - 1 < ++TrialManager.trialNumber)
                {
                    StateManager.ChangeState(StateManager.END);
                }
                break;
            case StateManager.BEGIN:
                TrialManager.trialNumber = 0;
                break;
            case StateManager.END:
                // log results of test
                break;
        }
    }

    public static Color GetDisplayColor()
    {
        return TrialManager.colors[TrialManager.displayColorIndices[TrialManager.trialNumber]];
    }

    public static Color GetColorChoice(int index)
    {
        return TrialManager.colors[TrialManager.panelColorIndices[TrialManager.trialNumber][index]];
    }
}
