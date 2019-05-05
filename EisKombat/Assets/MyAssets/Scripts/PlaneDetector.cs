﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "AircraftFuselage")
        {
            transform.GetComponentInParent<EnemyBehaviour>().state = EnemyBehaviour.States.chase;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "AircraftFuselage")
        {
            transform.GetComponentInParent<EnemyBehaviour>().state = EnemyBehaviour.States.idle;
        }
    }
}
