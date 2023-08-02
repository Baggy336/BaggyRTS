using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Base
{
    public class UnitStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float cost, aggroRange, attackRange, attackDmg, attackSpeed, health, armor;
        }
    }
}

