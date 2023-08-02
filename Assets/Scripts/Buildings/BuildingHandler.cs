using System.Collections;
using System.Collections.Generic;
using Units.Base;
using UnityEngine;

namespace Buildings.Base
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;

        [SerializeField]
        private BasicBuilding barracks;

        private void Awake()
        {
            instance = this;
        }

        public BuildingStatTypes.BaseStats GetBaseStats(string buildingType)
        {
            BasicBuilding building;

            switch (buildingType)
            {
                case "barrack":
                    building = barracks;
                    break;
                // Case for each unit type
                default:
                    Debug.Log($"No unit of type {buildingType} exists.");
                    return null;
            }

            return building.baseStats;
        }
    }
}

