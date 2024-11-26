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

    //Function to quickly set the dialog panel to active and takes a message string as parameter to set the text field.
    public void ShowText(string text)
    {
        textField.SetText(text);
        UI.SetActive(true);
    }
    //Hides dialog panel if active.
    public void HideText()
    {
        UI.SetActive(false);
    }

    //Used only for the healing NPC. Does the same as the 2 above functions.
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
