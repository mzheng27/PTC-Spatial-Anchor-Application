using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private PlacementCursorManager placementManager;

    [SerializeField]
    private List<PlacementObject> placedObjects;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private float rayDistanceFromCamera = 10.0f;

    [SerializeField]
    private float generateRayAfterSeconds = 2.0f;

    private float rayTimer = 0;

    [SerializeField]
    private GameObject selector;

    // Update is called once per frame
    void Update()
    {
        placedObjects = placementManager.getPlacedObjects();
        if (rayTimer >= generateRayAfterSeconds)
        {
            // creates a ray from the screen point origin 
            Ray ray = arCamera.ScreenPointToRay(selector.transform.position);

            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject, rayDistanceFromCamera))
            {
                PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();
                if (placementObject != null)
                {
                    placementManager.Log("detected");
                    ChangeSelectedObject(placementObject);
                    
                }
            }
            else
            {
                ChangeSelectedObject();
            }

            rayTimer = 0;
        }
        else
        {
            rayTimer += Time.deltaTime * 1.0f;
        }
    }

    void ChangeSelectedObject(PlacementObject selected = null)
    {
        foreach (PlacementObject current in placedObjects)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if (selected != current)
            {
                current.Selected = false;
                meshRenderer.material.color = inactiveColor;
            }
            else
            {
                current.Selected = true;
                meshRenderer.material.color = activeColor;
            }
            current.ToggleCanvas();
        }
    }


}
