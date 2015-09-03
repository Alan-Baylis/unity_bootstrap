using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;

abstract public class BasePanel : Complex, IStateful, IPointerEnterHandler, IPointerExitHandler
{
    protected float gazeStartTime;
    protected Coroutine gazeRoutine;
    protected bool isInteractable;
    protected Color disabledColor = new Color(0.2f, 0.2f, 0.2f, 1f);

    protected virtual void Start()
    {
        StateManager.Register(this);
    }

    public abstract void OnStateChange(int stateID);
    protected abstract int GetNextState();
    protected void UpdateMaterial(float percent)
    {
        this.renderer.material.SetFloat("_Selected", percent);
    }

    protected void SetColor(Color color)
    {
        this.renderer.material.SetColor("_Color", color);
    }

    protected void SetDisabled()
    {
        this.SetColor(this.disabledColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInteractable == true)
        {
            gazeStartTime = Time.time;
            gazeRoutine = StartCoroutine(GazeRoutine());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gazeRoutine != default(Coroutine))
        {
            StopCoroutine(gazeRoutine);
            Reset();
        }
    }

    public IEnumerator GazeRoutine()
    {
        while (true)
        {
            float deltaTime = Time.time - gazeStartTime;
            this.UpdateMaterial(deltaTime/TrialManager.gazeInterval);

            if (deltaTime > TrialManager.gazeInterval)
            {
                StateManager.ChangeState(this.GetNextState());
                break;
            }

            yield return null;
        }

        this.Reset();
    }

    public void Reset()
    {
        this.UpdateMaterial(0);
        this.gazeRoutine = default(Coroutine);
    }
}
