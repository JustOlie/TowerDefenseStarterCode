using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackRate = 1f;
    public int attackDamage = 1;
    public float attackSize = 1f;
    public float projectileSpeed = 5f; // Speed of the projectile
    public GameObject bulletPrefab;
    public TowerType type;

    private float nextAttackTime;

    void Update()
    {
        // Check if it's time to attack
        if (Time.time >= nextAttackTime)
        {
            // Find all enemies within the attack range
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    // Shoot at the enemy
                    ShootAtEnemy(collider.gameObject);
                    // Set the next attack time based on the attack rate
                    nextAttackTime = Time.time + 1f / attackRate;
                    break; // Only shoot at one enemy per attack
                }
            }
        }
    }

    void ShootAtEnemy(GameObject enemy)
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // Set the damage of the bullet
        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.damage = attackDamage;
            projectile.target = enemy.transform;
            projectile.speed = projectileSpeed; // Set the speed of the projectile
            bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f); // Set the scale of the bullet
        }
    }

    // Draw the attack range in the editor for easier debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
