using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerUI_Gear : MonoBehaviour
{
    public static PlayerUI_Gear instance;

    [SerializeField] GameObject gearScreen;
    [Header("Coin Display")]
    [SerializeField] TextMeshProUGUI bronzeCoin;
    [SerializeField] TextMeshProUGUI silverCoin;
    [SerializeField] TextMeshProUGUI goldCoin;
    [Header("Coin Exchange")]
    [SerializeField] TextMeshProUGUI exchangeBronze;
    [SerializeField] TextMeshProUGUI exchangeSilver;
    [Header("Exchange Result")]
    [SerializeField] TextMeshProUGUI bronzeToGold;
    [SerializeField] TextMeshProUGUI silverToGold;
    [Header("Exchange Value")]
    [SerializeField] int bronzeValue;
    [SerializeField] int silverValue;
    [Header("Screen Display")]
    [SerializeField] GameObject mainGearScreen;
    [SerializeField] GameObject watchAdScreen;

    int bronze;
    int silver;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gearScreen.SetActive(false);
        mainGearScreen.SetActive(true);
        watchAdScreen.SetActive(false);
    }

    private void Update()
    {
        //Coin Display
        goldCoin.text = PlayerPrefs.GetInt("GoldCoin").ToString();
        bronzeCoin.text = PlayerPrefs.GetInt("BronzeCoin").ToString();
        silverCoin.text = PlayerPrefs.GetInt("SilverCoin").ToString();

        //Coin Exchange
        exchangeBronze.text = PlayerPrefs.GetInt("BronzeCoin").ToString();
        exchangeSilver.text = PlayerPrefs.GetInt("SilverCoin").ToString();

        //Exchange Result
        bronze = PlayerPrefs.GetInt("BronzeCoin") / bronzeValue;
        silver = PlayerPrefs.GetInt("SilverCoin") / silverValue;
        bronzeToGold.text = bronze.ToString();
        silverToGold.text = silver.ToString();
    }

    public void GiveReward()
    {
        PlayerPrefs.SetInt("GoldCoin", PlayerPrefs.GetInt("GoldCoin") + 50);
        PlayerPrefs.SetInt("BronzeCoin", PlayerPrefs.GetInt("BronzeCoin") + 200);
        PlayerPrefs.SetInt("SilverCoin", PlayerPrefs.GetInt("SilverCoin") + 100);
        CloseWatchAd();
    }

    public void CloseWatchAd()
    {
        mainGearScreen.SetActive(true);
        watchAdScreen.SetActive(false);
    }

    public void OpenWatchAd()
    {
        mainGearScreen.SetActive(false);
        watchAdScreen.SetActive(true);
    }

    public void WatchRewardAd()
    {
        AdManager.instance.ShowRewardAd();
    }

    public void BackToUpgrade()
    {
        gearScreen.SetActive(false);
    }

    public void BronzeExchange()
    {
        PlayerPrefs.SetInt("GoldCoin", PlayerPrefs.GetInt("GoldCoin") + bronze);
        PlayerPrefs.SetInt("BronzeCoin", 0);
    }
    public void SilverExchange()
    {
        PlayerPrefs.SetInt("GoldCoin", PlayerPrefs.GetInt("GoldCoin") + silver);
        PlayerPrefs.SetInt("SilverCoin", 0);
    }
}
