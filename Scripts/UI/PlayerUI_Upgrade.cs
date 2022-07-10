using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI_Upgrade : MonoBehaviour
{
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject gearScreen;
    [SerializeField] TextMeshProUGUI goldCoinText;
    [SerializeField] TextMeshProUGUI bronzeCoinText;
    [SerializeField] TextMeshProUGUI silverCoinText;

    private void Start()
    {
        mainMenuScreen.SetActive(true);
        gearScreen.SetActive(false);
    }

    private void Update()
    {
        goldCoinText.text = PlayerPrefs.GetInt("GoldCoin").ToString();
        bronzeCoinText.text = PlayerPrefs.GetInt("BronzeCoin").ToString();
        silverCoinText.text = PlayerPrefs.GetInt("SilverCoin").ToString();
    }

    public void BackToMainMneu()
    {
        mainMenuScreen.SetActive(true);
    }

    public void ProceedToGear()
    {
        gearScreen.SetActive(true);
    }
}
