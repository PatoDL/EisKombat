using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float life;
    public float rotationValue;
    public float planeSpeed;
    public Rigidbody rig;
    public float yaw, pitch, vSpeed, hSpeed;
    public float fuel;
    bool accelerate = true;

    public float bulletSpeed;
    public GameObject bulletShooter;
    public GameObject bulletPF;
    private float bulletTimer = 0f;

    const float minAltitude = 8f;

    public GameObject explosionPF;

    public float gravityScale;

    public delegate void OnGameOver();
    public static OnGameOver GameOver;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rotationValue = 0.5f;
        planeSpeed = 10f;
        yaw = 0.0f;
        pitch = 0.0f;
        vSpeed = 0.8f;
        hSpeed = 0.8f;
        life = 100f;
        bulletSpeed = 1000f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotationValue);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -rotationValue);
        }

        if(Input.GetMouseButton(0))
        {
            Shoot();
        }

        yaw = hSpeed * Input.GetAxis("Mouse X");
        pitch = vSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles += transform.up * yaw;
        transform.eulerAngles -= transform.right * pitch;

        if (transform.position.y < minAltitude)
        {
            PlaneDeath();
        }
    }

    void PlaneDeath()
    {
        GameObject cam = Instantiate(transform.Find("Main Camera").gameObject);
        cam.transform.position = transform.position - transform.forward * 5f;
        cam.transform.LookAt(transform);
        GameObject exp = Instantiate(explosionPF);
        exp.transform.position = transform.position;
        Destroy(this.gameObject);
        GameOver();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ForceMode fm;

            if (accelerate)
            {
                fm = ForceMode.Acceleration;
                accelerate = false;
            }
            else
            {
                fm = ForceMode.Force;
                fuel -= Time.fixedDeltaTime;
                if (fuel <= 0)
                    PlaneDeath();
                gravityScale -= Time.fixedDeltaTime;
                if (gravityScale < 0)
                    gravityScale = 0;
            }
            
            rig.AddForce(transform.forward * planeSpeed, fm);
        }
        else
        {
            accelerate = true;
            gravityScale += Time.fixedDeltaTime;
            if (gravityScale > 2)
                gravityScale = 2;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (rig.velocity.z > 0)
                rig.velocity -= new Vector3(0, 0, Time.fixedDeltaTime);
            else
                rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y, 0f);
        }

        rig.AddForce(Vector3.down * gravityScale, ForceMode.Force);
    }

    void Shoot()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > 0.1f)
        {
            GameObject bullet = Instantiate(bulletPF);
            bullet.transform.position = bulletShooter.transform.position;
            bullet.transform.rotation = bulletShooter.transform.rotation;
            bullet.GetComponentInChildren<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Force);
            bulletTimer = 0f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="Bullet")
        {
            life -= 5f;
            if(life<=0f)
            {
                PlaneDeath();
            }
        }
        else
        {
            PlaneDeath();
        }
    }
}
