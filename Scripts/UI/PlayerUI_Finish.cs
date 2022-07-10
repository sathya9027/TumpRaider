using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerUI_Finish : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI distanceText;
    [Header("GameObject")]
    [SerializeField] GameObject finishScreen;
    [SerializeField] GameObject coreGameScreen;

    PlayerUI_CoreGame playerUICore;

    private void Start()
    {
        finishScreen.SetActive(false);
        coreGameScreen.SetActive(true);
        playerUICore = GetComponent<PlayerUI_CoreGame>();
    }

    private void Update()
    {
        int playerType = PlayerPrefs.GetInt("PlayerType");
        distanceText.text = "Distance: " + PlayerPrefs.GetInt(playerType + "Distance").ToString() + "m";
    }

    public void CallFinishScreen()
    {
        finishScreen.SetActive(true);
        coreGameScreen.SetActive(false);
    }

    public void MainMenu()
    {
        StartCoroutine(SceneLoader.instance.LoadMainMenu(0));
    }

    public void ReloadScene()
    {
        StartCoroutine(SceneLoader.instance.LoadMainMenu(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextStage()
    {
        StartCoroutine(SceneLoader.instance.LoadMainMenu(SceneManager.GetActiveScene().buildIndex + 1));
    }
}
