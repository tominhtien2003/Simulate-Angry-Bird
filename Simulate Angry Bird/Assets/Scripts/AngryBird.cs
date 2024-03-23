using System.Collections;
using UnityEngine;

public class AngryBird : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private bool hadLaunched;
    private bool changeFaceFollowVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        rb.isKinematic = true;
        circleCollider.enabled = false;
    }
    private void FixedUpdate()
    {
        if (hadLaunched && changeFaceFollowVelocity)
        {
            transform.right = rb.velocity;
        }
    }
    public void LaunchBird(Vector2 _direc , float _force)
    {
        rb.isKinematic = false;
        circleCollider.enabled = true;

        rb.AddForce(_direc * _force, ForceMode2D.Impulse);
        hadLaunched = true;
        changeFaceFollowVelocity = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        changeFaceFollowVelocity = false;
        StartCoroutine(DestroyBirdOldTime());
    }
    private IEnumerator DestroyBirdOldTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
