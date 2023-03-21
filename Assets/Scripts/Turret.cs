using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : MonoBehaviour
{
    [Header("Unity Setup")]
    private Transform target;
    private Enemy targetEnemy;
    public String enemyTag = "Enemy";
    public Transform partToRotate;
    public Transform firePoint;
    
    [Header("General")]
    public float range = 15f;
    public float turretTurnSpeed = 10;
    
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    private float fireCountdown = 0f;
    public float fireRate = 1f;
    
    [Header("Use Laser")] 
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 30;
    [FormerlySerializedAs("slowPercentage")] public float slowAmount = 0.5f;
    private void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
                    
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
            
        
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1,target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        impactEffect.transform.position = target.position + dir.normalized;
    }

    private void LockOnTarget()
    {
        // Target lock-on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turretTurnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }

    private void Shoot()
    {
       GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
       Bullet bullet = bulletGameObject.GetComponent<Bullet>();
       
       if(bullet != null)
           bullet.Seek(target);
    }
}
