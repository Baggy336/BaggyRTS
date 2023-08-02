using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitHandler.PlayerInput;
using UnityEditor.PackageManager;
using Unity.VisualScripting;
using Units.Base;
using JetBrains.Annotations;

namespace PlayerManagement 
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;

        public Transform playerBuildings;

        private void Awake()
        {
            instance = this;
            SetBasicStats(playerUnits);
            SetBasicStats(enemyUnits);
            SetBasicStats(playerBuildings);

        }
        private void Update()
        {
            UnitSelection.instance.HandleUnitMovement();
        }

        public void SetBasicStats(Transform type)
        {
            foreach (Transform child in type)
            {
                foreach (Transform transform in child)
                {
                    // Relies on the unit folder set up as plural. "Workers" "Warriors"
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();
                    //var stats = Units.Base.UnitHandler.instance.GetBaseStats(unitName);

                    // The specific playerUnit script on this object
                    if (type == playerUnits)
                    {
                        Units.Player.PlayerUnit playerUnit = transform.GetComponent<Units.Player.PlayerUnit>();

                        // Set unit stats
                        playerUnit.baseStats = Units.Base.UnitHandler.instance.GetBaseStats(name);
                    }
                    else if (type == enemyUnits)
                    {
                        // Set enemy unit stats
                        Units.Enemy.EnemyUnit enemyUnit = transform.GetComponent<Units.Enemy.EnemyUnit>();

                        // Set unit stats
                        enemyUnit.baseStats = Units.Base.UnitHandler.instance.GetBaseStats(name);
                    }
                    else if (type == playerBuildings)
                    {
                        Buildings.Player.PlayerBuilding playerBuilding = transform.GetComponent<Buildings.Player.PlayerBuilding>();
                        playerBuilding.baseStats = Buildings.Base.BuildingHandler.instance.GetBaseStats(name);
                    }



                    // Modify base stats
                }
            }
        }
    }
}

