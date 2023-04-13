using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class MenuManager : MonoBehaviour
{
    public Text[] revenuePanelTexts;
    public Text timerText;
    public Text rewardText;
    public ShopManager shopManager;
    public GameObject ReturnPanel;
    public float exitTime;
    public float timeSinceExit;
    private float _reward = 0;

    public void Start ()
    {
        timeSinceExit = (float)(DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("lastSessionEndTime", DateTime.Now.ToString()))).TotalSeconds;
        //timeSinceExit = Time.realtimeSinceStartup - exitTime;
        Debug.Log(Time.realtimeSinceStartup);
        if (timeSinceExit > 6)
        {
            CollectTheReward();
        } else
        {
            StartCoroutine(SaveTheMoment());
        }
    }
    public void CollectTheReward ()
    {
        timerText.text = $"You were not in the game: {timeSinceExit}";


        foreach (ProductPanelManager productPanelManager in shopManager.productPanelsArray)
        {
            Debug.Log("Ïûòàşñü");
            if (productPanelManager.manager)
            {
                float êewardMultiplier = timeSinceExit/productPanelManager.productSO.initialTime;
                _reward += productPanelManager.productRevenue * êewardMultiplier;
                ReturnPanel.SetActive(true);
                Debug.Log("Ïîëó÷èëîñü");
            }
        }
        rewardText.text = _reward.ToString();
        StartCoroutine(SaveTheMoment());
    }

    public void AcceptTheAward()
    {
        shopManager.coins += _reward;
        ReturnPanel.SetActive(false);
        Debug.Log($"Äîáàâëåíî {_reward}");
    }

    public IEnumerator SaveTheMoment ()
    {
        while(true)
        {
            PlayerPrefs.SetString("lastSessionEndTime",  DateTime.Now.ToString());
            PlayerPrefs.Save();
            Debug.Log("Ñîõğàíåíî");
            yield return new WaitForSeconds(5);
        }
    } 

    public void DeletedEndExit ()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit ()
    {
        Application.Quit();
    }
}