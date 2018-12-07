using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour {

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
 
    public float range = 15f;

    [Header("Use Bullets(default)")]

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool uselaser = false;

    public int DamageOverTime = 30;
    public float SlowPercent = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem LaserImpactEffect;
    public Light LaserImpactLight;

    [Header("Turret Setup")]

    public string enemyTag = "enemy";

    public Transform PartToRotate;
    public float turnSpeed = 10f;

   
    public Transform firePoint;

    // Use this for initialization
    void Start () {
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortDistance)
            {
                shortDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

	// Update is called once per frame
	void Update ()
    {        

        if (target == null)
        {
            if (uselaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    LaserImpactEffect.Stop();
                    LaserImpactLight.enabled = false;
                }
            }

            return;            
        }

        LockOnTarget();

        if (uselaser)
        {
            Laser();
        }
        else {

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        

    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(DamageOverTime * Time.deltaTime);
        targetEnemy.Slow(SlowPercent);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            LaserImpactEffect.Play();
            LaserImpactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;        

        LaserImpactEffect.transform.position = target.position + dir.normalized * .5f;

        LaserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
   

     void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);     

    }
}
