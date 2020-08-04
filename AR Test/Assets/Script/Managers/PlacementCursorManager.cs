using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;

public class PlacementCursorManager : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    //the position and rotation of the point
    private Pose placementPosition;
    private bool isValidPlacement = false;
    public GameObject placementMark;
    public GameObject objectToPlace;
    public bool enabled;
    Anchor anchor;
    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            UpdatePlacementPose();
            UpdatePlacementMarkPosition();
            if (isValidPlacement && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
            }
        }
    }
    void UpdatePlacementPose()
    {
        var centerOfScreen = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        var hitList = new List<ARRaycastHit>();
        //get a flat Surface to place the object
        arRaycastManager.Raycast(centerOfScreen, hitList, TrackableType.Planes);
        isValidPlacement = hitList.Count > 0;

        if (isValidPlacement)
        {
            placementPosition = hitList[0].pose;
            var cameraDirection = Camera.main.transform.forward;
            var cameraRotation = new Vector3(cameraDirection.x, 0f, cameraDirection.z).normalized;
            placementPosition.rotation = Quaternion.LookRotation(cameraRotation);
        }
    }
    
    private void UpdatePlacementMarkPosition()
    {
        if (isValidPlacement)
        {
            placementMark.SetActive(true);
            placementMark.transform.SetPositionAndRotation(placementPosition.position, placementPosition.rotation);

        }
        else
        {
            placementMark.SetActive(false);
        }
    }
    
    public void PlaceObject()
    {
        anchor = Session.CreateAnchor(placementPosition);
        Instantiate(objectToPlace, placementPosition.position, placementPosition.rotation, anchor.transform);
    }
}

