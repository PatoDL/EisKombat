using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    enum States
    {
        idle,
        chase,
        attack
    }

    States state;
    Rigidbody rig;
    float force;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        state = States.idle;
        rig = GetComponent<Rigidbody>();
        force = 40.0f;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(state == 0)
        {
            rig.AddForce(transform.forward * force, ForceMode.Force);
        }
    }
}
