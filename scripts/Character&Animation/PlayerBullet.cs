using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody m_rb;
    private int m_bulletSpeed = 500;
    public Transform bulletSpawnPoint { set; private get; }
    public CharacterAimManager playerAim { private get; set; }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    private void OnDisable()
    {
        transform.position = bulletSpawnPoint.position;
        m_rb.angularVelocity = Vector3.zero;
        m_rb.velocity = Vector3.zero;


    }
    private void OnEnable()
    {
        if (bulletSpawnPoint == null || playerAim.aimPos == null)
        {
            return;
        }

        bulletSpawnPoint.LookAt(playerAim.aimPos);
        transform.position = bulletSpawnPoint.position;
        transform.rotation = bulletSpawnPoint.rotation;
        m_rb.velocity = transform.forward * m_bulletSpeed;


    }


    private void OnCollisionEnter(Collision collision)
    {

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
