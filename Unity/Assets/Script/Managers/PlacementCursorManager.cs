using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PlacementCursorManager : MonoBehaviour
{
    private AppStateManager appStateManager;
    private GeneralConfiguration generalConfiguration;
    private ARRaycastManager arRaycastManager;
    private GameObject placementCursor;
    public PlacementObject objectToPlace;
    private ARReferencePointManager anchorManager;
    private List<ARReferencePoint> anchors = new List<ARReferencePoint>();
    private List<PlacementObject> placementObjects = new List<PlacementObject>();

    public Text logText;

    void Awake()
    {
        appStateManager = FindObjectOfType<AppStateManager>();
        generalConfiguration = FindObjectOfType<GeneralConfiguration>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        placementCursor = Instantiate(generalConfiguration.placementCursorPrefab) as GameObject;
        anchorManager = FindObjectOfType<ARReferencePointManager>();

    }

    void Update()
    {
        UpdateCursorPose();
        UpdateCursorIndicator();
        if (appStateManager.placementCursorIsSurface && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            logText.text = placementObjects.Count.ToString();
            PlaceObject();
        }

    }

    private void UpdateCursorPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var arRaycastHits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, arRaycastHits, TrackableType.Planes);
        appStateManager.placementCursorIsSurface = arRaycastHits.Count > 0;
        if (appStateManager.placementCursorIsSurface)
        {
            appStateManager.placementCursorPose = arRaycastHits[0].pose;
        }
    }

    private void UpdateCursorIndicator()
    {
        if (appStateManager.placementCursorIsSurface)
        {
            placementCursor.SetActive(true);
            placementCursor.transform.SetPositionAndRotation(appStateManager.placementCursorPose.position, appStateManager.placementCursorPose.rotation);
        }
        else
        {
            placementCursor.SetActive(false);
        }
    }

    public void PlaceObject()
    {
        ARReferencePoint newAnchor = anchorManager.AddReferencePoint(appStateManager.placementCursorPose);
        PlacementObject newPlaced = Instantiate(objectToPlace, appStateManager.placementCursorPose.position, appStateManager.placementCursorPose.rotation, newAnchor.transform);
        placementObjects.Add(newPlaced);
        anchors.Add(newAnchor);
        //DragDropBar.gameObject.SetActive(true);
    }
}


