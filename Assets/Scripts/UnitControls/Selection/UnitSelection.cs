using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Units.Player;
using Interaction;

namespace UnitHandler.PlayerInput
{
    // Aka input manager
    public class UnitSelection : MonoBehaviour
    {
        public static UnitSelection instance;

        // hit results of a ray
        private RaycastHit hit;

        public List<Transform> selectedUnits = new List<Transform>();
        public List<Transform> selectedBuildings = new List<Transform>();

        public LayerMask interactable = new LayerMask();

        private bool draggingMouse = false;

        private Vector3 mousePos;

        private void Awake()
        {
            instance = this;
        }

        private void OnGUI()
        {
            if(draggingMouse)
            {
                Rect rect = GroupSelect.GetScreenBox(mousePos, Input.mousePosition);
                GroupSelect.DrawSelectionBox(rect, new Color(0f, 100f, 0f, .25f));
                GroupSelect.DrawBoxBorder(rect, 3, Color.green);
            }
        }

        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // set the start mouse pos
                mousePos = Input.mousePosition;

                // Create a ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // check the ray for hit objects
                if(Physics.Raycast(ray, out hit, 100, interactable))
                {
                    if(AddedUnit(hit.transform, Input.GetKey(KeyCode.LeftShift)))
                    {
                        // Do something with the selected unit
                    }
                    else if(AddedBuilding(hit.transform))
                    {
                        // Do something with selected building
                    }
                }
                else
                {
                    draggingMouse = true;
                    DeselectUnits();
                }
            }

            // When the player releases the mouse button
            if (Input.GetMouseButtonUp(0)) 
            {
                // All folders within the playerUnits folder
                foreach(Transform child in PlayerManagement.PlayerManager.instance.playerUnits)
                {
                    // Each child object in the playerUnits folder
                    foreach(Transform unit in child)
                    {
                        // If the unit is within the selection bounds
                        if(UnitInSelectionBox(unit))
                        {
                            AddedUnit(unit, true);
                        }                    
                    }
                }

                // Set dragging back to false
                draggingMouse = false;
            } 

            if(Input.GetMouseButtonDown(1) && HasUnitsSelected())
            {
                // Create a ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // check the ray for hit objects
                if (Physics.Raycast(ray, out hit))
                {
                    // react to the hit, store the unit hit layer
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    // returns a numerical value
                    switch (layerHit.value)
                    {
                        case 8: //Unit layer
                            // Do something
                            break;
                        case 10: //Enemy Unit
                            // Do something
                            break;
                        default:
                            foreach(Transform unit in selectedUnits)
                            {
                                // Get the playerunit component for every unit selected
                                PlayerUnit playerUnit = unit.gameObject.GetComponent<PlayerUnit>();
                                playerUnit.MoveUnit(hit.point);
                            }
                            break;
                    }
                }
            }
        }

        private void DeselectUnits()
        {
            // loop through the selection list, and remove the unit
            for(int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].gameObject.GetComponent<Interaction.IUnit>().OnInteractExit();
            }
            selectedUnits.Clear();
        }

        private void DeselectBuildings()
        {
            // loop through the selection list, and remove the unit
            for (int i = 0; i < selectedBuildings.Count; i++)
            {
                selectedBuildings[i].gameObject.GetComponent<Interaction.IBuilding>().OnInteractExit();
            }
            selectedBuildings.Clear();
        }

        // Check for a unit object within the drawn rect
        private bool UnitInSelectionBox(Transform unit)
        {
            if(!draggingMouse)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds viewPortBounds = GroupSelect.GetViewBounds(cam, mousePos, Input.mousePosition);

            return viewPortBounds.Contains(cam.WorldToViewportPoint(unit.position));
        }

        private bool HasUnitsSelected()
        {
            if (selectedUnits.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Interaction.IUnit AddedUnit(Transform selection, bool canMultiSelect = false)
        {
            Interaction.IUnit iUnit = selection.GetComponent<Interaction.IUnit>();
            if(iUnit)
            {
                if(!canMultiSelect)
                {
                    DeselectBuildings();
                    DeselectUnits();
                }
                DeselectBuildings();

                selectedUnits.Add(iUnit.gameObject.transform);

                iUnit.OnInteractEnter();

                return iUnit;
            }
            else
            {
                Debug.Log("No IUnit found to interact");
                return null;
            }
        }

        private Interaction.IBuilding AddedBuilding(Transform selection, bool canMultiSelect = false)
        {
            Interaction.IBuilding iBuilding = selection.GetComponent<Interaction.IBuilding>();
            if(iBuilding)
            {
                if(!canMultiSelect)
                {
                    DeselectUnits();
                    DeselectBuildings();
                }
                DeselectUnits();

                selectedBuildings.Add(iBuilding.gameObject.transform);

                iBuilding.OnInteractEnter();

                return iBuilding;
            }
            else
            {
                Debug.Log("No IBuilding found to interact");
                return null;
            }
        }
    }
}

