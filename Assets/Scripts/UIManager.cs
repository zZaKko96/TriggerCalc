using UnityEngine;
using TMPro; 
using System.Text; 
using System; 

public class UIManager : MonoBehaviour
{
    [Header("Посилання на компоненти")]
    public ApplicationManager appManager;

    [Header("Панелі UI")]
    public GameObject MainMenu_Panel;
    public GameObject DataInput_Panel;
    public GameObject Results_Panel;

    [Header("Елементи Екрану Вводу")]
    public TMP_InputField inputField_1;
    private TextMeshProUGUI inputPlaceholder; 

    [Header("Елементи Екрану Результатів")]
    public TextMeshProUGUI resultText; 

    private SignalData lastResult;
    private string currentTriggerType; 

    void Start()
    {
        inputPlaceholder = inputField_1.placeholder.GetComponent<TextMeshProUGUI>();
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        MainMenu_Panel.SetActive(true);
        DataInput_Panel.SetActive(false);
        Results_Panel.SetActive(false);
    }

    public void ShowDataInputMenu()
    {
        MainMenu_Panel.SetActive(false);
        DataInput_Panel.SetActive(true);
        Results_Panel.SetActive(false);
    }

    public void ShowResultScreen()
    {
        MainMenu_Panel.SetActive(false);
        DataInput_Panel.SetActive(false);
        Results_Panel.SetActive(true);
    }

    public void OnButton_RS_Click()
    {
        appManager.CreateTrigger("RS");
        currentTriggerType = "RS";
        inputField_1.text = ""; 
        inputPlaceholder.text = "Enter R signals (line 1)\nEnter S signals (line 2)";
        ShowDataInputMenu();
    }

    public void OnButton_JK_Click()
    {
        appManager.CreateTrigger("JK");
        currentTriggerType = "JK";
        inputField_1.text = ""; 
        inputPlaceholder.text = "Enter J signals (line 1)\nEnter K signals (line 2)";
        ShowDataInputMenu();
    }

    public void OnButton_D_Click()
    {
        appManager.CreateTrigger("D");
        currentTriggerType = "D";
        inputField_1.text = ""; 
        inputPlaceholder.text = "Enter D signals...";
        ShowDataInputMenu();
    }

    public void OnButton_T_Click()
    {
        appManager.CreateTrigger("T");
        currentTriggerType = "T";
        inputField_1.text = ""; 
        inputPlaceholder.text = "Enter T signals...";
        ShowDataInputMenu();
    }

    public void OnButton_Exit_Click()
    {
        Debug.Log("Вихід з програми...");
        Application.Quit();
    }

    public void OnButton_Calculate_Click()
    {
        string inputText = inputField_1.text;
        SignalData inputData = new SignalData();

        string[] lines = inputText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        if (currentTriggerType == "RS" || currentTriggerType == "JK")
        {
            if (lines.Length < 2)
            {
                Debug.LogError("Для RS/JK тригерів потрібно 2 рядки вводу!");
                resultText.text = "Error:\nДля RS/JK тригерів\nпотрібно 2 рядки вводу!";
                ShowResultScreen();
                return;
            }
            if (lines[0].Trim().Length != lines[1].Trim().Length)
            {
                Debug.LogError("Рядки вводу мають бути однакової довжини!");
                resultText.text = "Error:\nРядки вводу мають\nбути однакової довжини!";
                ShowResultScreen();
                return;
            }

            inputData.inputSequence1 = ConvertStringToBoolArray(lines[0].Trim());
            inputData.inputSequence2 = ConvertStringToBoolArray(lines[1].Trim());
        }
        else 
        {
            if (lines.Length < 1)
            {
                Debug.LogError("Поле вводу пусте!");
                resultText.text = "Error:\nПоле вводу пусте!";
                ShowResultScreen();
                return;
            }
            inputData.inputSequence1 = ConvertStringToBoolArray(lines[0].Trim());
            inputData.inputSequence2 = new bool[lines[0].Trim().Length];
        }

        appManager.PerformCalculation(inputData);
    }
    public void OnButton_Back_Click()
    {
        ShowDataInputMenu();
    }

    public void OnButton_ToMenu_Click()
    {
        ShowMainMenu();
    }

    public void OnButton_Save_Click()
    {
        if (lastResult != null)
        {
            appManager.SaveResult(lastResult);
        }
    }

    public void ShowResult(SignalData result)
    {
        lastResult = result;

        string resultString = ConvertBoolArrayToString(result.outputSequence);

        resultText.text = "Result:\n" + resultString;

        ShowResultScreen();
    }
    private bool[] ConvertStringToBoolArray(string str)
    {
        bool[] arr = new bool[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            arr[i] = (str[i] == '1');
        }
        return arr;
    }

    private string ConvertBoolArrayToString(bool[] arr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (bool b in arr)
        {
            sb.Append(b ? "1" : "0");
        }
        return sb.ToString();
    }
}