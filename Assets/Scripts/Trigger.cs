using UnityEngine;

public abstract class Trigger
{
    protected bool currentState;
    public abstract void SetInitialState(bool state);
    public abstract SignalData CalculateOutput(SignalData input);
}