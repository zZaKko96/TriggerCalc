using UnityEngine;

public class RSTrigger : Trigger
{
    public override void SetInitialState(bool state)
    {
        this.currentState = state;
    }

    public override SignalData CalculateOutput(SignalData input)
    {
        int length = input.inputSequence1.Length;
        input.outputSequence = new bool[length];

        bool[] R = input.inputSequence1;
        bool[] S = input.inputSequence2;

        for (int i = 0; i < length; i++)
        {
            if (R[i] && S[i])
            {
                Debug.LogWarning($"RS-Trigger: Заборонена комбінація на кроці {i}. R=1, S=1.");
                input.outputSequence[i] = currentState;
            }
            else if (S[i])
            {
                currentState = true;
            }
            else if (R[i])
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