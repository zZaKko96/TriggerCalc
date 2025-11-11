using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Посилання на компоненти")]
    public ApplicationManager appManager;

    public void ShowResult(SignalData result)
    {
        Debug.Log("Відображуємо результат на екрані");
    }

    public void OnCalculateButtonPress()
    {
        SignalData input = new SignalData();

        appManager.PerformCalculation(input);
    }

    public void OnSaveButtonPress()
    {
    }
}