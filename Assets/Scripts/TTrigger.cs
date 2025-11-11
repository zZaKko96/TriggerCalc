using UnityEngine;

public class TTrigger : Trigger
{
    public override void SetInitialState(bool state)
    {
        this.currentState = state;
    }

    public override SignalData CalculateOutput(SignalData input)
    {
        int length = input.inputSequence1.Length;
        input.outputSequence = new bool[length];

        bool[] T = input.inputSequence1;

        for (int i = 0; i < length; i++)
        {
            if (T[i])
            {
                currentState = !currentState;
            }
            else
            {
            }

            input.outputSequence[i] = currentState;
        }

        return input;
    }
}