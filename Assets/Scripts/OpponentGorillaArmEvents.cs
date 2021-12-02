using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentGorillaArmEvents : MonoBehaviour
{
    public GameObject opponent;

    public void PrintEvent()
    {
        Debug.Log("Arm shoot");
        OpponentGorillaShooting gorillaScript = opponent.GetComponent<OpponentGorillaShooting>();

        if(gorillaScript == null)
            return;

        gorillaScript.ShootBullet();
    }
}
