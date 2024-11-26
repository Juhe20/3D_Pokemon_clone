using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject UI;
    public TextMeshProUGUI textField;
    public GameObject HealUI;
    public TextMeshProUGUI healText;

    public void ShowText(string text)
    {
        textField.SetText(text);
        UI.SetActive(true);
    }
    public void HideText()
    {
        UI.SetActive(false);
    }
    public void ShowHealText(string text)
    {
        healText.SetText(text);
        HealUI.SetActive(true);
    }
    public void HideHealText()
    {
        HealUI.SetActive(false);
    }
}
