using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFarAway : MonoBehaviour
{
    ItemActivator activationScript;

    private void Start()
    {
        activationScript = FindObjectOfType<ItemActivator>();

        StartCoroutine(AddToList());
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);

        activationScript.addList.Add(new ActivatorItem { item = this.gameObject });
    }

}
