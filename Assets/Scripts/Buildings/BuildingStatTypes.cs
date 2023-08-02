using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings.Base
{
    public class BuildingStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class BaseStats
        {
            public float health, armor;
        }
    }
}

