using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI_CoreGame : MonoBehaviour
{
    public static PlayerUI_CoreGame instance;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI goldCoinText;
    [SerializeField] TextMeshProUGUI bronzeCoinText;
    [SerializeField] TextMeshProUGUI silverCoinText;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI boostCount;
    [Header("Sprite")]
    [SerializeField] Sprite breakOn;
    [SerializeField] Sprite breakOff;
    [SerializeField] Sprite acceleratorOn;
    [SerializeField] Sprite acceleratorOff;
    [Header("Image")]
    [SerializeField] Image breakImage;
    [SerializeField] Image acceleratorImage;
    [Header("GameObject")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject coreGameScreen;
    [SerializeField] GameObject pauseScreen;

    PlayerTrigger playerTrigger;
    bool acceleratorButton;
    bool breakButton;
    int distanceTravelled;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerTrigger = FindObjectOfType<PlayerTrigger>();
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (playerTrigger != null)
        {
            goldCoinText.text = playerTrigger.GetGoldCoin().ToString();
            bronzeCoinText.text = playerTrigger.GetBronzeCoin().ToString();
            silverCoinText.text = playerTrigger.GetSilverCoin().ToString();
            boostCount.text = playerTrigger.GetBoostCount().ToString();
            distanceTravelled = (int)playerTrigger.transform.position.x;
            if (distanceTravelled >= 0)
            {
                int playerType = PlayerPrefs.GetInt("PlayerType");
                if (PlayerPrefs.GetInt(playerType + "Distance") < distanceTravelled)
                {
                    PlayerPrefs.SetInt(playerType + "Distance", distanceTravelled);
                }
                distanceText.text = "Distance: " + distanceTravelled + "m";
            }
            else
            {
                distanceTravelled = 0;
            }
        }
    }

    public void CallPause()
    {
        StartCoroutine(Pause());
    }

    private IEnumerator Pause()
    {
        pauseScreen.SetActive(true);
        pauseScreen.GetComponent<Animator>().Play("Pause");
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 0;
        coreGameScreen.SetActive(false);
    }

    public void SetAcceleratorPress()
    {
        acceleratorButton = true;
        acceleratorImage.sprite = acceleratorOff;
    }

    public void SetAcceleratorRelease()
    {
        acceleratorImage.sprite = acceleratorOn;
        acceleratorButton = false;
    }
    public void SetBreakPress()
    {
        breakButton = true;
        breakImage.sprite = breakOff;
    }

    public void SetBreakRelease()
    {
        breakImage.sprite = breakOn;
        breakButton = false;
    }

    public int GetDistance()
    {
        return distanceTravelled;
    }

    public bool GetAcceleratorButton()
    {
        return acceleratorButton;
    }

    public bool GetBreakButton()
    {
        return breakButton;
    }

    public IEnumerator GameOver()
    {
        coreGameScreen.SetActive(false);
        yield return new WaitForSeconds(3);
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<Animator>().Play("GameOver");
    }

    public void BoostPlayer()
    {
        playerTrigger.PlayerBoostTrigger();
    }

    public void RefreshUI()
    {
        playerTrigger = FindObjectOfType<PlayerTrigger>();
    }
}
