using UnityEngine;
using TMPro; 
using System.Text; 
using System;
using UnityEngine.Localization.Settings; 
using System.Collections.Generic;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

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

    [Header("Елементи Екрану Результатів")]
    public GameObject resultLabelObject; 
    public TextMeshProUGUI resultValueText;

    [Header("Елементи Локалізації")]
    public TMP_Dropdown languageDropdown;
    public LocalizeStringEvent placeholderLocalizer; 

    private SignalData lastResult;
    private string currentTriggerType; 

    void Start()
    {
        ShowMainMenu();
        PopulateLanguageDropdown();
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
        placeholderLocalizer.StringReference.SetReference("UI_Translations", "placeholder_rs");
        ShowDataInputMenu();
    }

    public void OnButton_JK_Click()
    {
        appManager.CreateTrigger("JK");
        currentTriggerType = "JK";
        inputField_1.text = ""; 
        placeholderLocalizer.StringReference.SetReference("UI_Translations", "placeholder_jk");
        ShowDataInputMenu();
    }

    public void OnButton_D_Click()
    {
        appManager.CreateTrigger("D");
        currentTriggerType = "D";
        inputField_1.text = "";
        placeholderLocalizer.StringReference.SetReference("UI_Translations", "placeholder_d");
        ShowDataInputMenu();
    }

    public void OnButton_T_Click()
    {
        appManager.CreateTrigger("T");
        currentTriggerType = "T";
        inputField_1.text = ""; 
        placeholderLocalizer.StringReference.SetReference("UI_Translations", "placeholder_t");
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
                ShowError("error_rsjk_lines");
                return;
            }
            if (lines[0].Trim().Length != lines[1].Trim().Length)
            {
                ShowError("error_length_mismatch");
                return;
            }

            inputData.inputSequence1 = ConvertStringToBoolArray(lines[0].Trim());
            inputData.inputSequence2 = ConvertStringToBoolArray(lines[1].Trim());
        }
        else 
        {
            if (lines.Length < 1)
            {
                ShowError("error_empty_input");
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

        resultLabelObject.SetActive(true);

        resultValueText.text = ConvertBoolArrayToString(result.outputSequence);

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

    void PopulateLanguageDropdown()
    {
        languageDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            options.Add(LocalizationSettings.AvailableLocales.Locales[i].LocaleName);
        }

        languageDropdown.AddOptions(options);

        languageDropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);

        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    public void OnLanguageChanged(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
    async void ShowError(string errorKey)
    {
        resultLabelObject.SetActive(false);

        var localizedString = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI_Translations", errorKey);
        await localizedString.Task; 

        resultValueText.text = localizedString.Task.Result;

        ShowResultScreen();
    }
}