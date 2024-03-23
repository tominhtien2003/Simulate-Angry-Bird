using UnityEngine;

public class Piggy : MonoBehaviour
{
    [SerializeField] private float  maxHealth = 10f;
    [SerializeField] private float damgaeThreshold = .2f;
    private float curHealth;
    private void Awake()
    {
        curHealth = maxHealth;
    }
    public void DamgePiggy(float damge)
    {
        curHealth -= damge;
        if (curHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;
        if (impactVelocity > damgaeThreshold)
        {
            DamgePiggy(impactVelocity);
        }
    }
}
