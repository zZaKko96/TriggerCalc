using UnityEngine;
using System.IO; 
using System.Text; 
using System;

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

    public void SaveResult(SignalData data, string triggerType)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Trigger Type: {triggerType}");
        sb.AppendLine("---");
        sb.AppendLine($"Input 1 (R/J/D/T): {ConvertBoolArrayToString(data.inputSequence1)}");

        if (triggerType == "RS" || triggerType == "JK")
        {
            sb.AppendLine($"Input 2 (S/K):     {ConvertBoolArrayToString(data.inputSequence2)}");
        }
        sb.AppendLine("---");
        sb.AppendLine($"Result (Q):        {ConvertBoolArrayToString(data.outputSequence)}");

        string path = Path.Combine(Application.persistentDataPath, "TriggerCalc_LastResult.txt");

        try
        {
            File.WriteAllText(path, sb.ToString());
            Debug.Log($"Результат успішно збережено у: {path}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Не вдалося зберегти результат: {e.Message}");
        }
    }

    private string ConvertBoolArrayToString(bool[] arr)
    {
        if (arr == null) return "";
        StringBuilder sb = new StringBuilder();
        foreach (bool b in arr)
        {
            sb.Append(b ? "1" : "0");
        }
        return sb.ToString();
    }
}