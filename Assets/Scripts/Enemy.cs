using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    public float health = 100;
    public int worth = 50;
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }
    
    private void Die()
    {
        PlayerStats.Money += worth;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect,5f);
    }


}
