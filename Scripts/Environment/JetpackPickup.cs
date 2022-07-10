using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    [SerializeField] float jetpackSpeed;
    [SerializeField] float jetpackDuration;

    public float GetJetSpeed()
    {
        return jetpackSpeed;
    }

    public float GetJetDuration()
    {
        return jetpackDuration;
    }
}
