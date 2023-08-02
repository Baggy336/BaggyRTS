using System.Collections;
using System.Collections.Generic;
using UnitHandler.PlayerInput;
using Units.Base;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        private NavMeshAgent agent;

        public UnitStatTypes.Base baseStats;

        private Collider[] rangeColliders;

        private Transform target;

        public GameObject unitStatDisplay;

        public Image healthBarAmount;

        private UnitStatInteractions aggroUnit;

        private float distanceToTarget;

        private float attackCD;
       
        private bool hasAggro = false;


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(attackCD > 0)
            {
                attackCD -= Time.deltaTime;
            }

            if(!hasAggro)
            {           
                CheckTargetRange();
            }
            else
            {
                AttackTarget();
                PursueTarget();
            }
        }

        private void CheckTargetRange()
        {
            // Create a collider
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange);

            // Check the overlapped layer
            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.layer == Base.UnitHandler.instance.playerUnitLayer)
                {
                    target = rangeColliders[i].gameObject.transform;

                    aggroUnit = target.gameObject.GetComponentInChildren<UnitStatInteractions>();

                    hasAggro = true;
                    break;
                }
            }
        }

        private void PursueTarget()
        {
            if(target == null)
            {
                agent.SetDestination(transform.position);
                hasAggro = false;
            }
            else
            {
                distanceToTarget = Vector3.Distance(target.position, transform.position);

                agent.stoppingDistance = baseStats.attackRange + 1f;

                if (distanceToTarget <= baseStats.aggroRange)
                {
                    agent.SetDestination(target.position);
                }
            }
        }

        private void AttackTarget()
        {
            if(attackCD <= 0 && distanceToTarget <= baseStats.attackRange + 1)
            {
                aggroUnit.TakeDamage(baseStats.attackDmg);
                attackCD = baseStats.attackSpeed;
            }
        }
    }
}

