using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static FollowCamera instance;

    [SerializeField] float cameraFarPoint = -10;
    [SerializeField] float screenDampingX = 3;
    [SerializeField] float jetPackCamSize;
    [SerializeField] float gameOverCamSize;

    Camera cam;
    PlayerTrigger playerTrigger;
    PlayerMovement playerMovement;
    float defaultSize;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
        playerTrigger = FindObjectOfType<PlayerTrigger>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        StartCoroutine(GetDefaultSize());
    }

    private IEnumerator GetDefaultSize()
    {
        yield return new WaitForSeconds(0.01f);
        defaultSize = cam.orthographicSize;
        screenDampingX = defaultSize;
    }

    private void LateUpdate()
    {
        if (playerMovement != null && playerTrigger != null)
        {
            if (playerTrigger.GetCanMove())
            {
                if (playerMovement.GetPlayerGotJetpack())
                {
                    JetpackCamera();
                }
                else
                {
                    CoreGameCamera();
                }
            }
            else
            {
                GameOverCamera();
            }
        }
    }

    private void CoreGameCamera()
    {
        if (cam.orthographicSize > defaultSize)
        {
            cam.orthographicSize -= Time.deltaTime;
        }
        else
        {
            cam.orthographicSize = defaultSize;
        }
        transform.position = playerTrigger.transform.position + new Vector3(screenDampingX, 0, cameraFarPoint);
    }

    private void GameOverCamera()
    {
        if (cam.orthographicSize > gameOverCamSize)
        {
            cam.orthographicSize -= Time.deltaTime;
            transform.position = playerTrigger.transform.position - new Vector3(Time.deltaTime, 0, 0);
            transform.position = playerTrigger.transform.position + new Vector3(0, 0, cameraFarPoint);
        }
        else
        {
            cam.orthographicSize = gameOverCamSize;
            transform.position = playerTrigger.transform.position + new Vector3(0, 0, cameraFarPoint);
        }
    }

    private void JetpackCamera()
    {
        if (cam.orthographicSize < jetPackCamSize)
        {
            cam.orthographicSize += Time.deltaTime;
        }
        else
        {
            cam.orthographicSize = jetPackCamSize;
        }
        transform.position = playerTrigger.transform.position + new Vector3(screenDampingX, 0, cameraFarPoint);
    }

    public void RefreshCamera()
    {
        playerTrigger = FindObjectOfType<PlayerTrigger>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        StartCoroutine(GetDefaultSize());
    }
}
