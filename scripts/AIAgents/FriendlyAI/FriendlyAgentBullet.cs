using UnityEngine;

public class FriendlyAgentBullet : MonoBehaviour
{
    private Rigidbody m_rb;
    private int m_bulletSpeed = 500;
    public Transform bulletSpawnPoint { set; get; }


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        if (bulletSpawnPoint.transform.parent.gameObject == null)
        {
            Destroy(gameObject);
        }

        transform.position = bulletSpawnPoint.position;
        m_rb.angularVelocity = Vector3.zero;
        m_rb.velocity = Vector3.zero;
        m_rb.angularDrag = 0;



    }
    private void OnEnable()
    {
        if (bulletSpawnPoint == null)
        {
            return;
        }

        transform.position = bulletSpawnPoint.position;
        transform.rotation = bulletSpawnPoint.rotation;
        m_rb.velocity = transform.forward * m_bulletSpeed;


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"collision : {collision.gameObject.name}");
        var explosible = collision.gameObject.GetComponent<IExplosible>();
        if (explosible != null)
        {
            explosible.TakeDamage(10, .25f, transform.position);
            gameObject.SetActive(false);
            return;
        }
        var damageable = collision.gameObject.GetComponent<IDestroyable>();
        if (damageable != null)
        {
            damageable.GetHit(10, .25f);
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(false);
    }
}
