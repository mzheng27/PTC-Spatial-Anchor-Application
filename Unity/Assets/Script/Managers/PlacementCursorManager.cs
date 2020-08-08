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
    //public GameObject objectToPlace;
    private ARReferencePointManager anchorManager;
    private List<ARReferencePoint> anchors = new List<ARReferencePoint>();

    [SerializeField]
    private Canvas MobileUX;

    [SerializeField]
    private GameObject DragDropBar;


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
        //if (appStateManager.placementCursorIsSurface && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    PlaceObject();
        //}
        if (appStateManager.placementCursorIsSurface)
        {
            MobileUX.gameObject.SetActive(true);
            DragDropBar.gameObject.SetActive(true);

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

    //private void PlaceObject()
    //{
    //    //anchor = Session.CreateAnchor(placementPosition);
    //    //Instantiate(objectToPlace, appStateManager.placementCursorPose.position, appStateManager.placementCursorPose.rotation);
    //    ARReferencePoint newAnchor = anchorManager.AddReferencePoint(appStateManager.placementCursorPose);
    //    anchors.Add(newAnchor);
    //}
}


