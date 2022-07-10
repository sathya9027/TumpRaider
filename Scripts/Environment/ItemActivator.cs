using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivator : MonoBehaviour
{
    public static ItemActivator instance;

    [SerializeField] int distanceFromPlayer;

    GameObject player;

    List<ActivatorItem> activatorItems;

    public List<ActivatorItem> addList;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        activatorItems = new List<ActivatorItem>();
        addList = new List<ActivatorItem>();
        StartCoroutine(StartDelay());
        AddToList();
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.1f);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void AddToList()
    {
        if (addList.Count > 0)
        {
            foreach (ActivatorItem item in addList)
            {
                if (item.item != null)
                {
                    activatorItems.Add(item);
                }
            }

            addList.Clear();  
        }

        StartCoroutine(CheckActivation());
    }

    public void RefreshPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator CheckActivation()
    {
        List<ActivatorItem> removeList = new List<ActivatorItem>();

        if (activatorItems.Count > 0)
        {
            foreach (ActivatorItem item in activatorItems)
            {
                if (item.item == null)
                {
                    removeList.Add(item);
                }
                else if (Vector3.Distance(player.transform.position, item.item.transform.position) > distanceFromPlayer)
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(false);
                    }
                }
                else
                {
                    if (item.item == null)
                    {
                        removeList.Add(item);
                    }
                    else
                    {
                        item.item.SetActive(true);
                    }
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        yield return new WaitForSeconds(0.01f);

        if (removeList.Count > 0)
        {
            foreach (ActivatorItem item in removeList)
            {
                activatorItems.Remove(item);
            }
        }

        yield return new WaitForSeconds(0.01f);

        AddToList();
    }
}

public class ActivatorItem
{
    public GameObject item;
}