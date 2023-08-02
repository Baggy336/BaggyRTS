using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Base
{
    // Set up the ability to make a new object of Unit type
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit")]
    // Doesn't derive from monobehavior, because this will not be on an object
    public class Unit : ScriptableObject
    {
        public enum unitType
        {
            Worker,
            Army
        }

        [Space(10)]
        [Header("Unit Settings")]
        [Space(5)]
        public unitType type;

        public string unitName;

        public GameObject unitPrefab;

        [Space(20)]
        [Header("Unit Basic Stats")]
        [Space(5)]

        public UnitStatTypes.Base baseStats;
    }
}

