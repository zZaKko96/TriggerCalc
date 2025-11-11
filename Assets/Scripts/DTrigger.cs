using UnityEngine;

public class DTrigger : Trigger
{
    public override void SetInitialState(bool state)
    {
        this.currentState = state;
    }

    public override SignalData CalculateOutput(SignalData input)
    {
        int length = input.inputSequence1.Length;
        input.outputSequence = new bool[length];

        bool[] D = input.inputSequence1;

        for (int i = 0; i < length; i++)
        {
            currentState = D[i];
            input.outputSequence[i] = currentState;
        }

        return input;
    }
}