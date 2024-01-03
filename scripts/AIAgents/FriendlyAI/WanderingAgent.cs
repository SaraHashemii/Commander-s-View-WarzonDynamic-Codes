using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAgent : MonoBehaviour
{
    private NavMeshAgent m_navMeshAgent;


    #region Wander
    private float m_WanderDistance = 50f;
    private bool m_isWandering;
    private float m_currentWanderTimer;
    private float m_maxWanderTimer = 10f;
    #endregion

    private Animator m_animator;


    private enum m_soldierStates
    {
        Idle,
        Wandering,

    }


    private m_soldierStates m_currentSoldierState;
    // Start is called before the first frame update
    void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_currentSoldierState = m_soldierStates.Idle;
        m_currentWanderTimer = Time.time;
        m_navMeshAgent.stoppingDistance = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isWandering && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            m_currentSoldierState = m_soldierStates.Idle;
            m_isWandering = false;
            m_navMeshAgent.isStopped = true;
            m_currentWanderTimer = Time.time;
        }

        if (!m_isWandering && m_currentWanderTimer + m_maxWanderTimer <= Time.time)
        {
            Wander();
        }

        SetAnimationState(m_currentSoldierState);
    }



    private void Wander()
    {
        m_currentSoldierState = m_soldierStates.Wandering;
        m_isWandering = true;
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.SetDestination(GetRandomPosition());
    }


    public Vector3 GetRandomPosition()

    {
        Vector3 random = Random.insideUnitSphere * m_WanderDistance;
        random.y = 0f;
        Vector3 nextLocation = transform.position + random;
        if (NavMesh.SamplePosition(nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            nextLocation = hit.position;

        }
        return nextLocation;
    }



    private void SetAnimationState(m_soldierStates state)
    {

        switch (state)
        {
            case m_soldierStates.Idle:

                m_animator.SetBool("isWandering", false);
                break;


            case m_soldierStates.Wandering:

                m_animator.SetBool("isWandering", true);
                break;
        }
    }
}
