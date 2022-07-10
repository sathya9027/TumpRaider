using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI_Stage : MonoBehaviour
{
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject stageScreen;


    private void Start()
    {
        mainMenuScreen.SetActive(true);
        stageScreen.SetActive(false);
    }

    public void BackToMainMenu()
    {
        mainMenuScreen.SetActive(true);
        stageScreen.SetActive(false);
    }

    public void CLickStage(GameObject obj)
    {
        StartCoroutine(SceneLoader.instance.LoadLevel(obj));
    }

}
