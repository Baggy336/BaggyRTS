using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings.Base
{
    // Set up the ability to make a new object of Building type
    [CreateAssetMenu(fileName = "New Building", menuName = "New Building")]

    public class BasicBuilding : ScriptableObject
    {
        public enum buildingType
        {
            Barracks
        }

        [Space(10)]
        [Header("Building Settings")]
        [Space(5)]
        public buildingType type;

        public string buildingName;

        public GameObject buildingPrefab;

        public BuildingActions.BuildingUnits units;

        [Space(20)]
        [Header("Building Basic Stats")]
        [Space(5)]
        public BuildingStatTypes.BaseStats baseStats;
    }
}

