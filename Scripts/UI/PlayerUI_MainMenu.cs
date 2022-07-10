using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayerUI_MainMenu : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject stageScreen;
    [Header("Text")]
    [SerializeField] TextMeshProUGUI goldCoinText;
    [SerializeField] TextMeshProUGUI bronzeCoinText;
    [SerializeField] TextMeshProUGUI silverCoinText;
    [SerializeField] TextMeshProUGUI distanceText;
    [Header("Sprite")]
    [SerializeField] Sprite[] vehicleSprite;
    [Header("Image")]
    [SerializeField] Image bgImage;

    private void Start()
    {
        mainMenuScreen.SetActive(true);
        settingsScreen.SetActive(false);
        stageScreen.SetActive(false);
    }

    private void Update()
    {
        UpdateMethods();
        CheatCoins();
        ResetCoins();
        ResetKeys();
    }

    private void UpdateMethods()
    {
        goldCoinText.text = PlayerPrefs.GetInt("GoldCoin").ToString();
        bronzeCoinText.text = PlayerPrefs.GetInt("BronzeCoin").ToString();
        silverCoinText.text = PlayerPrefs.GetInt("SilverCoin").ToString();
        int playerType = PlayerPrefs.GetInt("PlayerType");
        distanceText.text = "Distance: " + PlayerPrefs.GetInt(playerType + "Distance").ToString() + "m";
        bgImage.sprite = vehicleSprite[playerType];
        bgImage.SetNativeSize();
    }

    private void ResetKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Default" + "Use", 1);
        }
    }

    private void CheatCoins()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetInt("GoldCoin",10000);
            PlayerPrefs.SetInt("BronzeCoin",10000);
            PlayerPrefs.SetInt("SilverCoin",10000);
        }
    }

    private void ResetCoins()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("GoldCoin", 0);
            PlayerPrefs.SetInt("BronzeCoin", 0);
            PlayerPrefs.SetInt("SilverCoin", 0);
        }
    }

    public void Upgrade()
    {
        mainMenuScreen.SetActive(false);
    }

    public void Play()
    {
        stageScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settingsScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }
}
