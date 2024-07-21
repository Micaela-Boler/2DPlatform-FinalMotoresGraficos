using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _bulletRB;
    [SerializeField] float speed;
    [SerializeField] float timeToDestroyBullet;
    [SerializeField] string gameObjectCollision;



    void Awake()
    {
        _bulletRB = GetComponent<Rigidbody2D>();
    }

    

    public void LaunchBullet(Vector2 direction)
    {
        _bulletRB.velocity = direction * speed;

        Destroy(gameObject, timeToDestroyBullet);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(gameObjectCollision))
            Destroy(gameObject);
    }
}
