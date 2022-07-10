using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public static PlayerTrigger instance;

    [SerializeField] CircleCollider2D[] wheelColliders;
    [SerializeField] LayerMask groundLayer;

    int goldCoin;
    int bronzeCoin;
    int silverCoin;
    int boostCount;
    bool canMove;
    bool playerInGround;
    PlayerMovement playerMovement;
    PlayerUI_Finish plauerUIFinish;
    JetpackPickup jetpack;
    Transform playerTransform;

    private void Start()
    {
        instance = this;
        canMove = true;
        playerInGround = true;
        playerMovement = GetComponent<PlayerMovement>();
        plauerUIFinish = FindObjectOfType<PlayerUI_Finish>();
    }

    private void Update()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            playerInGround = wheelColliders[i].IsTouchingLayers(groundLayer);
        }
        if (canMove)
        {
            playerTransform = transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "GoldCoin":
                GoldCoin(collision);
                break;
            case "BronzeCoin":
                BronzeCoin(collision);
                break;
            case "SilverCoin":
                SilverCoin(collision);
                break;
            case "Jetpack":
                Jetpack(collision);
                break;
            case "Engine":
                Engine(collision);
                break;
            case "Finish":
                Finish(collision);
                break;
            case "Barrier":
                return;
            default:
                SetCanMoveToFalse();
                break;
        }
    }

    private void GoldCoin(Collider2D collision)
    {
        goldCoin++;
        PlayerPrefs.SetInt("GoldCoin", PlayerPrefs.GetInt("GoldCoin") + 1);
        collision.gameObject.GetComponent<AudioSource>().Play();
        Destroy(collision);
        Destroy(collision.gameObject.GetComponent<SpriteRenderer>());
        Destroy(collision.gameObject, 0.5f);
    }
    private void BronzeCoin(Collider2D collision)
    {
        bronzeCoin += 5;
        PlayerPrefs.SetInt("BronzeCoin", PlayerPrefs.GetInt("BronzeCoin") + 5);
        collision.gameObject.GetComponent<AudioSource>().Play();
        Destroy(collision);
        Destroy(collision.gameObject.GetComponent<SpriteRenderer>());
        Destroy(collision.gameObject, 0.5f);
    }
    private void SilverCoin(Collider2D collision)
    {
        silverCoin += 3;
        PlayerPrefs.SetInt("SilverCoin", PlayerPrefs.GetInt("SilverCoin") + 3);
        collision.gameObject.GetComponent<AudioSource>().Play();
        Destroy(collision);
        Destroy(collision.gameObject.GetComponent<SpriteRenderer>());
        Destroy(collision.gameObject, 0.5f);
    }
    private void Jetpack(Collider2D collision)
    {
        boostCount++;
        jetpack = collision.gameObject.GetComponent<JetpackPickup>();
        collision.gameObject.GetComponent<AudioSource>().Play();
        Destroy(collision);
        Destroy(collision.gameObject.GetComponent<SpriteRenderer>());
        Destroy(collision.gameObject, 0.5f);
    }
    private void Engine(Collider2D collision)
    {
        //collision.gameObject.GetComponent<AudioSource>().Play();
        playerMovement.GetEngineReduce();
        Destroy(collision);
        Destroy(collision.gameObject.GetComponent<SpriteRenderer>());
        Destroy(collision.gameObject, 0.5f);
    }
    private void Finish(Collider2D collision)
    {
        plauerUIFinish.CallFinishScreen();
        Destroy(collision.gameObject);
    }

    public void PlayerBoostTrigger()
    {
        if (boostCount > 0)
        {
            StartCoroutine(playerMovement.JetpackBoost(jetpack.GetJetSpeed(), jetpack.GetJetDuration()));
            boostCount--;
        }
    }

    public  int GetBoostCount()
    {
        return boostCount;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMoveToFalse()
    {
        canMove = false;
    }

    public int GetGoldCoin()
    {
        return goldCoin;
    }
    
    public int GetBronzeCoin()
    {
        return bronzeCoin;
    }
    
    public int GetSilverCoin()
    {
        return silverCoin;
    }

    public bool GetPlayerInGround()
    {
        return playerInGround;
    }
}
