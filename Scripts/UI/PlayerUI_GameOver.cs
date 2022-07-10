using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerUI_GameOver : MonoBehaviour
{
    public static PlayerUI_GameOver instance;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI totalDistanceText;
    [Header("GameObject")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject coreGameScreen;
    [Header("Button")]
    [SerializeField] Button watchAdButton;

    PlayerUI_CoreGame playerUICore;
    bool isDisplayed;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerUICore = GetComponent<PlayerUI_CoreGame>();
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (PlayerMovement.instance.GetPlayerInVoidBoll())
        {
            watchAdButton.gameObject.SetActive(false);
        }
    }

    public void RetryGame()
    {
        StartCoroutine(SceneLoader.instance.LoadMainMenu(SceneManager.GetActiveScene().buildIndex));
    }

    public void MainMenu()
    {
        StartCoroutine(SceneLoader.instance.LoadMainMenu(0));
    }

    public void DisplayDistance()
    {
        if (!isDisplayed)
        {
            totalDistanceText.text = "Distance Travelled: " + playerUICore.GetDistance() + "m";
            isDisplayed = true;
        }
    }

    public void WatchAd()
    {
        AdManager.instance.ShowRetryAd();
        Destroy(PlayerTrigger.instance.gameObject);
        watchAdButton.interactable = false;
    }

    public void ContinueGame()
    {
        gameOverScreen.SetActive(false);
        coreGameScreen.SetActive(true);
        FollowCamera.instance.RefreshCamera();
        PlayerUI_CoreGame.instance.RefreshUI();
        ItemActivator.instance.RefreshPlayer();
    }
}
