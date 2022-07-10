using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Float")]
    [SerializeField] float maxSpeedLimit;
    [SerializeField] float maxEngineLimit;
    [SerializeField] float speedMultiplier;
    [SerializeField] float normalSpeed;
    [SerializeField] float boostSpeed;
    [SerializeField] float engineCoolDown;
    [SerializeField] float torqueAmmount;
    [SerializeField] float camSize;
    [Header("ParticleSystem")]
    [SerializeField] ParticleSystem frontEngineParticle;
    [SerializeField] ParticleSystem backTireParticle;
    [SerializeField] ParticleSystem jetpackFireParticle;

    Slider playerSpeedSlider;
    Slider engineSlider;
    Slider[] allSliders;
    SurfaceEffector2D[] sf2D;
    PlayerUI_CoreGame playerUICore;
    PlayerUI_GameOver playerUIGameOver;
    PlayerTrigger playerTrigger;
    Rigidbody2D bodyRb2D;
    bool isPlayerGotJetpack;
    bool playerFallInVoid;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FindObjectOfType<Camera>().orthographicSize = camSize;
        bodyRb2D = GetComponent<Rigidbody2D>();
        allSliders = FindObjectsOfType<Slider>();
        playerSpeedSlider = allSliders[0];
        engineSlider = allSliders[1];
        playerUICore = FindObjectOfType<PlayerUI_CoreGame>();
        playerUIGameOver = FindObjectOfType<PlayerUI_GameOver>();
        sf2D = FindObjectsOfType<SurfaceEffector2D>();
        playerTrigger = GetComponent<PlayerTrigger>();
        playerSpeedSlider.maxValue = maxSpeedLimit;
        engineSlider.maxValue = maxEngineLimit;
        frontEngineParticle.Stop();
        jetpackFireParticle.Stop();
        playerUIGameOver.ContinueGame();
        playerFallInVoid = false;
    }

    private void Update()
    {
        if (!isPlayerGotJetpack)
        {
            MovePlayer();
        }
    }

    public IEnumerator JetpackBoost(float jetSpeed,float jetDuration)
    {
        isPlayerGotJetpack = true;
        bodyRb2D.freezeRotation = true;
        jetpackFireParticle.Play();
        backTireParticle.Stop();

        for (int i = 0; i < sf2D.Length; i++)
        {
            sf2D[i].speed = jetSpeed;
        }

        yield return new WaitForSeconds(jetDuration);

        isPlayerGotJetpack = false;
        bodyRb2D.freezeRotation = false;
        jetpackFireParticle.Stop();
        backTireParticle.Play();
        for (int i = 0; i < sf2D.Length; i++)
        {
            sf2D[i].speed = boostSpeed;
        }
    }

    public bool GetPlayerGotJetpack()
    {
        return isPlayerGotJetpack;
    }

    public void GetEngineReduce()
    {
        engineSlider.value = 0;
        playerSpeedSlider.value -= 5;
        if (playerSpeedSlider.value < 0)
        {
            playerSpeedSlider.value = 0;
        }
    }

    private void MovePlayer()
    {
        for (int i = 0; i < sf2D.Length; i++)
        {
            if (playerTrigger.GetCanMove())
            {
                //Fall Into Void
                if (transform.position.y <= -10)
                {
                    playerFallInVoid = true;
                    sf2D[i].speed = 0;
                    playerTrigger.SetCanMoveToFalse();
                    backTireParticle.Stop();
                    StartCoroutine(playerUICore.GameOver());
                }

                //Accelerator
                if (playerUICore.GetAcceleratorButton() || Input.GetKey(KeyCode.D))
                {
                    Accelerator(i);
                }
                //Break
                else if (playerUICore.GetBreakButton() || Input.GetKey(KeyCode.A))
                {
                    Break(i);
                }
                //Reduce SliderValue & Speed
                else
                {
                    if (engineSlider.value <= engineSlider.minValue)
                    {
                        playerSpeedSlider.value -= (speedMultiplier * engineCoolDown) * Time.deltaTime;
                    }
                    else
                    {
                        engineSlider.value -= (speedMultiplier * engineCoolDown) * Time.deltaTime;
                    }
                    if (sf2D[i].speed > 0)
                    {
                        sf2D[i].speed -= (speedMultiplier * engineCoolDown) * Time.deltaTime;
                    }
                    else
                    {
                        sf2D[i].speed = 0;
                    }
                }
            }
            else
            {
                sf2D[i].speed = 0;
                backTireParticle.Stop();
                StartCoroutine(playerUICore.GameOver());
                playerUIGameOver.DisplayDistance();
            }
        }
    }

    public bool GetPlayerInVoidBoll()
    {
        return playerFallInVoid;
    }

    private void Accelerator(int i)
    {
        //Boost Limit
        if (playerSpeedSlider.value < playerSpeedSlider.maxValue)
        {
            
            if (bodyRb2D.velocity.y > 0)
            {
                if (sf2D[i].speed < boostSpeed)
                {
                    sf2D[i].speed += boostSpeed * (speedMultiplier * 0.1f) * Time.deltaTime;
                }
                else
                {
                    sf2D[i].speed = boostSpeed;
                }
            }
            else
            {
                sf2D[i].speed = boostSpeed / 2;
            }
            if (!playerTrigger.GetPlayerInGround())
            {
                bodyRb2D.AddTorque(-torqueAmmount * Time.deltaTime);
            }
            playerSpeedSlider.value += speedMultiplier * Time.deltaTime;
        }
        //Normal Speed
        else
        {
            if (engineSlider.value < engineSlider.maxValue)
            {
                engineSlider.value += speedMultiplier * Time.deltaTime;
                if (sf2D[i].speed > normalSpeed)
                {
                    sf2D[i].speed -= normalSpeed * speedMultiplier * Time.deltaTime;
                }
                else
                {
                    sf2D[i].speed = normalSpeed;
                }
            }
            else if(engineSlider.value >= engineSlider.maxValue)
            {
                //SceneManager.LoadScene(0);
                frontEngineParticle.Play();
                backTireParticle.Stop();
                playerTrigger.SetCanMoveToFalse();
                StartCoroutine(playerUICore.GameOver());
            }
        }
    }

    private void Break(int i)
    {
        //Boost Limit
        if (playerSpeedSlider.value < playerSpeedSlider.maxValue)
        {
            playerSpeedSlider.value += speedMultiplier * Time.deltaTime;
            if (!playerTrigger.GetPlayerInGround())
            {
                bodyRb2D.AddTorque(torqueAmmount * Time.deltaTime);
            }
            if (sf2D[i].speed > -boostSpeed)
            {
                sf2D[i].speed += -boostSpeed * speedMultiplier * Time.deltaTime;
            }
            else
            {
                sf2D[i].speed = -boostSpeed;
            }
        }
        //Normal Speed
        else
        {
            if (engineSlider.value < engineSlider.maxValue)
            {
                engineSlider.value += speedMultiplier * Time.deltaTime;
                if (sf2D[i].speed < -normalSpeed)
                {
                    sf2D[i].speed -= -normalSpeed * speedMultiplier * Time.deltaTime;
                }
                else
                {
                    sf2D[i].speed = -normalSpeed;
                }
            }
            else if (engineSlider.value >= engineSlider.maxValue)
            {
                frontEngineParticle.Play();
                backTireParticle.Stop();
                playerTrigger.SetCanMoveToFalse();
                StartCoroutine(playerUICore.GameOver());
            }
        }
    }
}
