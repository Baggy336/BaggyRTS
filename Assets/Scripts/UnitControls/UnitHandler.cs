using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerManagement;

namespace Units.Base
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        [SerializeField]
        private Unit worker;

        public LayerMask playerUnitLayer, enemyUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        public UnitStatTypes.Base GetBaseStats(string unitType)
        {
            Unit unit;

            switch(unitType)
            {
                case "worker":
                    unit = worker;
                    break;
                // Case for each unit type
                default:
                    Debug.Log($"No unit of type {unitType} exists.");
                    return null;
            }

            return unit.baseStats;
        }
    }
}


