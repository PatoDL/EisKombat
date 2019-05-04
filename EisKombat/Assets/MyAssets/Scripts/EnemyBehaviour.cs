using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum States
    {
        idle,
        chase,
        attack
    }

    public States state;
    Rigidbody rig;
    [SerializeField] float force;
    [SerializeField] float timer;
    GameObject plane;
    public GameObject bulletPF;

    const int cantBullets = 1000;

    float bulletTimer;

    GameObject[] bullets = new GameObject[cantBullets];

    public float bulletVel;

    // Start is called before the first frame update
    void Start()
    {
        state = States.idle;
        rig = GetComponent<Rigidbody>();
        force = 40.0f;
        timer = 0.0f;
        plane = GameObject.Find("AircraftFuselage");
        bulletVel = 2000;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(plane.transform.position, transform.position);
        timer += Time.deltaTime;
        if(timer>23f && state == States.idle)
        {
            transform.Rotate(new Vector3(0, -90, 0));
            timer = 0;
        }

        if(distance<90f)
        {
            if(state!=States.attack)
            state = States.attack;
        }
    }

    void FixedUpdate()
    {
        if (state == States.idle)
        {
            rig.AddForce(transform.forward * force, ForceMode.Force);
        }

        if(state == States.chase  || state == States.attack)
        { 
            transform.LookAt(plane.transform);
            rig.AddForce(Vector3.Normalize((plane.transform.position - transform.position))*force*2, ForceMode.Force);
        }

        if (state == States.attack)
        {

            bulletTimer += Time.deltaTime;
            if (bulletTimer > 0.8f)
            {
                GameObject bullet = Instantiate(bulletPF);
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                bullet.transform.rotation = transform.rotation;
                bullet.GetComponentInChildren<Rigidbody>().AddForce(bullet.transform.forward * bulletVel, ForceMode.Force);
                bulletTimer = 0f;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name== "AircraftFuselage")
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }
}
