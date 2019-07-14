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
    public float speed;
    float timer;
    GameObject plane;
    public GameObject bulletPF;
    public GameObject bulletShooter;
    public float life;

    public GameObject explosion;

    float bulletTimer;
    public float bulletSpeed;

    public delegate void OnEnemyKill();
    public static OnEnemyKill KillEnemy;

    // Start is called before the first frame update
    void Start()
    {
        state = States.idle;
        rig = GetComponent<Rigidbody>();
        timer = 0.0f;
        plane = GameObject.Find("Player");
        KillEnemy += EnemyDeath;
    }

    void OnDestroy()
    {
        KillEnemy -= EnemyDeath;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>23f && state == States.idle)
        {
            transform.Rotate(new Vector3(0, -90, 0));
            timer = 0;
        }

        if (plane)
        {
            float distance = Vector3.Distance(plane.transform.position, transform.position);
            if (distance < 90f)
            {
                if (state != States.attack)
                    state = States.attack;
            }
        }
    }

    void FixedUpdate()
    {
        if (state == States.idle)
        {
            rig.AddForce(transform.forward * speed, ForceMode.Force);
        }

        if(state == States.chase  || state == States.attack)
        {
            if (plane)
            {
                transform.LookAt(plane.transform);
                rig.AddForce(Vector3.Normalize((plane.transform.position - transform.position)) * speed, ForceMode.Force);
            }
        }

        if (state == States.attack)
        {
            bulletTimer += Time.fixedDeltaTime;
            if (bulletTimer > 0.4f)
            {
                GameObject bullet = Instantiate(bulletPF);
                bullet.transform.position = bulletShooter.transform.position;
                bullet.transform.rotation = bulletShooter.transform.rotation;
                bullet.GetComponentInChildren<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Force);
                bulletTimer = 0f;
            }
        }
    }

    void EnemyDeath()
    {
        GameObject exp = Instantiate(explosion);
        exp.transform.position = transform.position;
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="Player" || col.gameObject.tag=="Terrain")
        {
            KillEnemy();
        }
        if(col.gameObject.tag=="Bullet")
        {
            life -= 5f;
            if (life <= 0f)
                KillEnemy();
        }
    }
}
