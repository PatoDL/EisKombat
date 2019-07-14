using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    float timer = 0f;
    
    void Update()
    {
        if(timer>5f)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision col)
    {
        Destroy(this.gameObject);
    }
}
