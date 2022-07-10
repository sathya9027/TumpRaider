using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerUI_Pause : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI distanceText;
    [Header("GameObject")]
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject coreGameScreen;

    PlayerUI_CoreGame playerUICore;

    private void Start()
    {
        playerUICore = GetComponent<PlayerUI_CoreGame>();
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        distanceText.text = "Distance Travelled: " + playerUICore.GetDistance() + "m";
    }

    public void CallResume()
    {
        StartCoroutine(Resume());
    }

    private IEnumerator Resume()
    {
        Time.timeScale = 1;
        pauseScreen.GetComponent<Animator>().Play("Resume");
        yield return new WaitForSeconds(0.15f);
        pauseScreen.SetActive(false);
        coreGameScreen.SetActive(true);
    }

    public void Retry()
    {
        StartCoroutine(SceneLoader.instance.LoadMainMenu(SceneManager.GetActiveScene().buildIndex));
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(SceneLoader.instance.LoadMainMenu(0));
    }
}
