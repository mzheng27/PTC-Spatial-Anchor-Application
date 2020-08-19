using Azure.StorageServices;
using RESTClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//StorageBundle: provides methods to save the list of AnchorPrefab 
//as a JSON file with filename spatialAnchorData; to LOAD and process the JSON
//file and create a list of PlacementObject
public class StorageBundle : MonoBehaviour
{
    [Header("Cloud storage for anchor and media")]
    [SerializeField]
    private string storageAccount;
    [SerializeField]
    private string accessKey;
    [SerializeField]
    private string container;
    private string saveFileJSON = "spatialAnchorData.json";
    private StorageServiceClient client;
    private BlobService blobService;
    [SerializeField]
    private PlacementCursorManager placement;

    private void Start()
    {
        if (string.IsNullOrEmpty(storageAccount) || string.IsNullOrEmpty(accessKey))
        {
            return;
        }
        client = StorageServiceClient.Create(storageAccount, accessKey);
        blobService = client.GetBlobService();
    }

    public void SaveJSON()
    {
        string filename = "spatialAnchorData";
        TextAsset asset = (TextAsset)Resources.Load(filename);
        string json = asset.text;

        List listOfAnchors = JsonUtility.FromJson<List>(json);

        string jsonString = JsonUtility.ToJson(listOfAnchors);
        StartCoroutine(blobService.PutTextBlob(PutJsonCompleted, jsonString, container, saveFileJSON, "application/json"));
    }

    private void PutJsonCompleted(RestResponse response)
    {
        if (response.IsError)
        {
            Debug.Log("Put Json error:" + response.ErrorMessage);
            return;
        }
        Debug.Log("Put JSON: " + response.Url);
    }

    public void LoadJSON()
    {
        string resourcePath = container + "/" + saveFileJSON;
        StartCoroutine(blobService.GetJsonBlob<List>(LoadJSONComplete, resourcePath));
    }

    private void LoadJSONComplete(IRestResponse<List> response)
    {
        if (!response.IsError)
        {
            Debug.Log("json: " + response.Content);
            ProcessJSONAnchorListData(response.Data);
        }
    }

    private void ProcessJSONAnchorListData(List anchorData)
    {
        Debug.LogFormat("Anchors:", anchorData.prefabs.Length);
        if (anchorData.prefabs.Length <= 0)
        {
            return;
        }
        foreach(AnchorPrefab anchor in anchorData.prefabs)
        {
            anchor.Init();
            AddAnchor(anchor);
        }
    }

    private void AddAnchor(AnchorPrefab anchor)
    {
        PlacementObject newPlaced = placement.PlaceObject(anchor.Position, anchor.Rotation, anchor.Colour, anchor.priority, anchor.filepath_audio, anchor.filepath_text, anchor.filepath_image);
        newPlaced.prefabForSaving = anchor;
        
    }
}
