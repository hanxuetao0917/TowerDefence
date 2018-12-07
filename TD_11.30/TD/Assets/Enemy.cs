using UnityEngine;

public class Enemy : MonoBehaviour {

    public float StartSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float health = 100;

    public int Bouns = 50;

    public GameObject deathEffect;

    private void Start()
    {
        speed = StartSpeed;
    }


    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float percent)
    {
        speed = StartSpeed * (1f - percent);
    }

    void Die()
    {
        PlayerStats.Money += Bouns;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);       
        Destroy(gameObject);
    }

    
}
