using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject UI;
    public TextMeshProUGUI textField;

    public void ShowText(string text)
    {
        textField.SetText(text);
        UI.SetActive(true);
    }
    public void HideText()
    {
        UI.SetActive(false);
    }
}
