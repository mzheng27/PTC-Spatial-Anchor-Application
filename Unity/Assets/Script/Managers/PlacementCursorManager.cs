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

    [SerializeField]
    private GameObject DragDropBar;
    [SerializeField]
    private GameObject createRaycastPanel;
    [SerializeField]
    private GameObject RayCastCanvas;

    public Text anchorText;
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
        
        anchorText.text = placementObjects.Count.ToString();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            changeLabel();
        }
        if (!appStateManager.placementCursorIsSurface)
        {
            logText.text = "Cursor is not on a valid plane";
        } else
        {
            logText.text = "";
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

    public List<PlacementObject> getPlacedObjects()
    {
        return placementObjects;
    }

    public void Log(string x)
    {
        logText.text = x;
    }
    public void PlaceObject()
    {
        if (appStateManager.placementCursorIsSurface)
        {
            //logText.text = placementObjects.Count.ToString();
            ARReferencePoint newAnchor = anchorManager.AddReferencePoint(appStateManager.placementCursorPose);
            PlacementObject newPlaced = Instantiate(objectToPlace, appStateManager.placementCursorPose.position, appStateManager.placementCursorPose.rotation, newAnchor.transform);
            //newPlaced.SetActiveMedia(false);
            placementObjects.Add(newPlaced);
            anchors.Add(newAnchor);
            DragDropBar.gameObject.SetActive(true);
            createRaycastPanel.SetActive(false);
        } 
    }

    public PlacementObject PlaceObject(Vector3 position, Quaternion rotation, Color color, string priority, string audio, string text, string image)
    {
        //ARReferencePoint newAnchor = anchorManager.AddReferencePoint(appStateManager.placementCursorPose);
        PlacementObject newPlaced = Instantiate(objectToPlace, position, rotation);
        newPlaced.gameObject.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        newPlaced.SetOverlayText(priority);
        newPlaced.audioPath = audio;
        newPlaced.imagePath = image;
        newPlaced.textPath = text;
        placementObjects.Add(newPlaced);
        return newPlaced;
    }

    public void backToMainMenu()
    {
        DragDropBar.SetActive(false);
        createRaycastPanel.SetActive(true);
        logText.text = "";
    }

    public void changeSelectedLabel(PlacementObject selected)
    {
        if (string.Equals(selected.getLabel().Trim(), "Emergency"))
        {
            selected.SetOverlayText("Low Priority");
            selected.gameObject.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        }
        else if (string.Equals(selected.getLabel().Trim(), "Medium"))
        {
            selected.SetOverlayText("Emergency");
            selected.gameObject.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        } 
        else if (string.Equals(selected.getLabel().Trim(), "Low Priority"))
        {
            selected.SetOverlayText("medium");
            selected.gameObject.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
        }
    }

    public void changeLabel()
    {
        if (!RayCastCanvas.activeSelf)
        {
            changeSelectedLabel(placementObjects[placementObjects.Count - 1]);
        }
    }
}


