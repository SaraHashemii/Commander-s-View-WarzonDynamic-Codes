using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    const int NUMBEROFBULLETS = 60;
    //this variable is used as a reference to the current bullet in the queue
    private GameObject m_currentBullet;
    [Header("FireRate")]
    [SerializeField] private float m_fireRate;
    private float m_fireRateTimer;

    [Header("Bullet")]
    private Queue<GameObject> m_bulletQueue = new Queue<GameObject>(NUMBEROFBULLETS);
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_bulletSpawnPoint;
    private CharacterAimManager m_aim;

    [SerializeField] private AudioClip m_ShootingSound;
    private AudioSource m_audioSource;


    private void Awake()
    {
        m_aim = GetComponentInParent<CharacterAimManager>();
        m_fireRateTimer = m_fireRate;
        m_audioSource = GetComponent<AudioSource>();
        InitializeObjectPools();
    }


    // Update is called once per frame
    void Update()
    {
        if (CanFire())
        {
            Fire();
        }
    }

    bool CanFire()
    {
        m_fireRateTimer += Time.deltaTime;
        if (m_fireRateTimer <= m_fireRate)
        {

            return false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))

        {

            return true;
        }
        return false;
    }

    void Fire()
    {
        m_fireRate = 0;

        m_currentBullet = m_bulletQueue.Dequeue();
        m_audioSource.PlayOneShot(m_ShootingSound);
        m_currentBullet.SetActive(true);
        m_bulletQueue.Enqueue(m_currentBullet);

    }

    public void InitializeObjectPools()
    {
        for (int i = 0; i < NUMBEROFBULLETS; i++)
        {
            var bullet = Instantiate(m_bulletPrefab, m_bulletSpawnPoint.position, m_bulletSpawnPoint.rotation);
            bullet.GetComponent<PlayerBullet>().bulletSpawnPoint = m_bulletSpawnPoint;
            bullet.GetComponent<PlayerBullet>().playerAim = m_aim;
            bullet.SetActive(false);
            m_bulletQueue.Enqueue(bullet);
        }

    }
}
