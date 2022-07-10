using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public static SpawnPlayer instance;

    [SerializeField] GameObject[] player;
    [SerializeField] Transform spawnPoint;

    int playerType;
    Transform retrySpawn;

    private void Awake()
    {
        instance = this;
        SpawnPlayerMethod(spawnPoint);
    }

    private void Update()
    {
        if (PlayerTrigger.instance != null)
        {
            transform.position = PlayerTrigger.instance.transform.position + new Vector3(0, 2.25f, 0);
            retrySpawn = transform;
        }
    }

    public void SpawnPlayerMethod(Transform spawnPoint)
    {
        playerType = PlayerPrefs.GetInt("PlayerType");
        Instantiate(player[playerType], spawnPoint.position, Quaternion.identity);
    }

    public Transform GetRetrySpawn()
    {
        return retrySpawn;
    }
}
