using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private float planeSpeed;
    [SerializeField] private Rigidbody rig;
    [SerializeField] private float yaw, pitch, vSpeed, hSpeed;
    [SerializeField] private float bulletSpeed;
    public float life;
    public GameObject bulletPF;
    private float bulletTimer = 0f;
    bool accelerate = true;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        value = 0.5f;
        planeSpeed = 100;
        yaw = 0.0f;
        pitch = 0.0f;
        vSpeed = 2.0f;
        hSpeed = 2.0f;
        life = 100f;
        bulletSpeed = 1950;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, value);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -value);
        }

        if(Input.GetMouseButton(0))
        {
            Shoot();
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            accelerate = true;
        }

        yaw += hSpeed * Input.GetAxis("Mouse X");
        pitch -= vSpeed * Input.GetAxis("Mouse Y");

        if(pitch<-36)
        {
            pitch = -36;
        }
        if(pitch>50)
        {
            pitch = 50;
        }

        transform.eulerAngles = new Vector3(pitch, yaw, transform.rotation.eulerAngles.z);

        if (transform.position.y < 18.67f)
        {
            transform.position = new Vector3(transform.position.x, 18.67f, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (accelerate)
            {
                rig.AddForce(transform.forward * planeSpeed, ForceMode.Acceleration);
                accelerate = false;
            }
            else
            {
                rig.AddForce(transform.forward * planeSpeed, ForceMode.Force);
            }
        }


        if (Input.GetKey(KeyCode.S))
        {
            rig.AddForce(-transform.forward * planeSpeed, ForceMode.Force);
        }
    }

    void Shoot()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > 0.5f)
        {
            GameObject bullet = Instantiate(bulletPF);
            bullet.transform.position = transform.position + transform.forward * 10;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponentInChildren<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Force);
            bulletTimer = 0f;
        }
    }
}
