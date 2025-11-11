using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public UIManager uiManager;

    private Trigger currentTrigger;

    public void CreateTrigger(string triggerType)
    {
        switch (triggerType)
        {
            case "RS":
                currentTrigger = new RSTrigger();
                break;
            case "JK":
                currentTrigger = new JKTrigger();
                break;
            case "D":
                currentTrigger = new DTrigger();
                break;
            case "T":
                currentTrigger = new TTrigger();
                break;
            default:
                Debug.LogError("Невідомий тип тригера: " + triggerType);
                return;
        }

        Debug.Log($"Створено об'єкт: {currentTrigger.GetType().Name}");

        currentTrigger.SetInitialState(false);
    }

    public void PerformCalculation(SignalData input)
    {
        if (currentTrigger == null)
        {
            Debug.LogError("Тригер не створено перед обчисленням!");
            return;
        }

        Debug.Log("Виконуємо обчислення...");

        SignalData result = currentTrigger.CalculateOutput(input);

        uiManager.ShowResult(result);
    }

    public void SaveResult(SignalData data)
    {
        Debug.Log("Зберігаємо результат...");
    }
}