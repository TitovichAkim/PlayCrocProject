using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletedExit : MonoBehaviour
{
    public void DeletedEndExit ()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void Exit ()
    {
        Application.Quit();
    }
}
