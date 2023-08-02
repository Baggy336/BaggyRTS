using System;
using System.Collections;
using System.Collections.Generic;
using UnitHandler.PlayerInput;
using Units.Base;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {
        private NavMeshAgent agent;

        public UnitStatTypes.Base baseStats;


        private void OnEnable()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void MoveUnit(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
    }
}

