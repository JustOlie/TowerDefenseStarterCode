using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    void Start()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile towards the target
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);

        // Check if the projectile has reached the target
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            // Deal damage to the target
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.health -= damage;
                if (enemy.health <= 0)
                {
                    // If the enemy's health is depleted, destroy it
                    Destroy(enemy.gameObject);
                }
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
