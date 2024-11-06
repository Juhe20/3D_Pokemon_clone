using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    private bool menuActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Menu.SetActive(false);
                menuActive = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !menuActive)
        {
            Menu.SetActive(true);
            menuActive = true;
        }
    }

    public void ShowTeam()
    {
        Debug.Log("TestTeam");
    }

    public void ShowBag()
    {
        Debug.Log("TestBag");
    }
}
