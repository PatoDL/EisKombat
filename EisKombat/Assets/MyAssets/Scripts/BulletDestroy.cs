using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer>20f)
        {
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "AircraftFuselage")
        {
            col.gameObject.GetComponent<PlaneController>().life -= 5f;
        }

        Destroy(this.gameObject);
    }
}
