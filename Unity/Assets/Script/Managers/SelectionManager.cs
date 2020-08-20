using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private PlacementObject placementObject;

    public ImageDemo imageDemo;

    public TextDemo textDemo;

    public GameObject mediaPanel;

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
                placementObject = hitObject.transform.GetComponent<PlacementObject>();
                if (placementObject != null)
                {
                    
                    mediaPanel.SetActive(true);
                    ChangeSelectedObject(placementObject);
                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        placementManager.changeSelectedLabel(placementObject);
                    }
                }
            }
            else
            {
                mediaPanel.SetActive(false);
                ChangeSelectedObject();
            }

            rayTimer = 0;
        }
        else
        {
            rayTimer += Time.deltaTime * 1.0f;
        }
        
    }

    public void setMediaDisplay(string name)
    {
        if (placementObject != null)
        {
            placementObject.SetActiveMedia(name);
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
            
        }
    }

    public void saveImage()
    {
        if (placementObject != null)
        {
            placementObject.imagePath = imageDemo.TappedSave();
        }
    }

    public void updateTextPath()
    {
        if (placementObject != null)
        {
            placementObject.textPath = textDemo.TappedSaveText();
        }
    }

    public void LoadImage()
    {
        if (placementObject != null)
        {
            imageDemo.TappedLoad(placementObject.imagePath);
        }
    }

    public void LoadText()
    {
        if (placementObject != null)
        {
            String textMessage = textDemo.TappedLoadText(placementObject.textPath);
            placementObject.textWindow.text = textMessage;
        }
    }


}
