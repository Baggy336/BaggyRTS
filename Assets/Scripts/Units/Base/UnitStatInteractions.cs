using System;
using System.Collections;
using System.Collections.Generic;
using UnitHandler.PlayerInput;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Base
{
    public class UnitStatInteractions : MonoBehaviour
    {
        public float maxHealth, armor, currentHealth;

        [SerializeField]
        private Image healthBarAmount;

        private bool isPlayerUnit = false;

        private void Start()
        {
            try
            {
                maxHealth = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.health;
                armor = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.armor;
                isPlayerUnit = true;
            }
            catch(Exception)
            {
                Debug.Log("No player units, trying enemy unit.");
                try
                {
                    maxHealth = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.health;
                    armor = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.armor;
                    isPlayerUnit = false;
                }
                catch(Exception)
                {
                    Debug.Log("No unit scripts found in game objects.");
                }
            }

            currentHealth = maxHealth;
        }

        private void Update()
        {
            HandleHealth();
        }

        public void TakeDamage(float dmg)
        {
            float totalDmg = dmg - armor;

            currentHealth -= totalDmg;
        }

        private void HandleHealth()
        {
            Camera cam = Camera.main;

            // Rotate health bar to be perpendicular to the camera
            gameObject.transform.LookAt(gameObject.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);

            healthBarAmount.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if(isPlayerUnit)
            {
                UnitSelection.instance.selectedUnits.Remove(gameObject.transform);
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            
        }
    }
}

