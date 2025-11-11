using UnityEngine;

public class JKTrigger : Trigger
{
    public override void SetInitialState(bool state)
    {
        this.currentState = state;
    }

    public override SignalData CalculateOutput(SignalData input)
    {
        int length = input.inputSequence1.Length;
        input.outputSequence = new bool[length];

        bool[] J = input.inputSequence1;
        bool[] K = input.inputSequence2;

        for (int i = 0; i < length; i++)
        {
            if (J[i] && K[i])
            {
                currentState = !currentState;
            }
            else if (J[i])
            {
                currentState = true;
            }
            else if (K[i])
            {
                currentState = false;
            }
            else
            {
            }

            input.outputSequence[i] = currentState;
        }

        return input;
    }
}