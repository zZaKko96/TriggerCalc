using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public UIManager uiManager;

    private Trigger currentTrigger;

    public void CreateTrigger(string triggerType)
    {
        Debug.Log("Створюємо тригер типу: " + triggerType);
    }

    public void PerformCalculation(SignalData input)
    {
        Debug.Log("Виконуємо обчислення...");
    }

    public void SaveResult(SignalData data)
    {
        Debug.Log("Зберігаємо результат...");
    }
}