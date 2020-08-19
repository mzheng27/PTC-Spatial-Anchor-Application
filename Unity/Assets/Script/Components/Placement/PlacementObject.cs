using TMPro;
using UnityEngine;

public class PlacementObject : MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;

    public bool Selected
    {
        get
        {
            return this.IsSelected;
        }
        set
        {
            IsSelected = value;
        }
    }

    private TextMeshPro OverlayText;

    [SerializeField]
    private GameObject canvasComponent;

    public AnchorPrefab prefabForSaving;
    
    public GameObject[] medias;

    public string imagePath;
    public string textPath;
    public string audioPath;
    private void Awake()
    {
        OverlayText = GetComponentInChildren<TextMeshPro>();
        if (OverlayText != null)
        {
            OverlayText.gameObject.SetActive(true);
        }
        IsSelected = false;
        ToggleCanvas();
    }

    private void Update()
    {
        ToggleCanvas();
        prefabForSaving.filepath_audio = audioPath;
        prefabForSaving.filepath_image = imagePath;
        prefabForSaving.filepath_text = textPath;
        prefabForSaving.Colour = gameObject.GetComponent<Renderer>().material.color;
        prefabForSaving.priority = OverlayText.text;
    }
    public void SetOverlayText(string text)
    {
        if (OverlayText != null)
        {
            OverlayText.gameObject.SetActive(true);
            OverlayText.text = text;
        }
    }

    public string getLabel()
    {
        return OverlayText.text;
    }


    public void ToggleCanvas()
    {
        canvasComponent?.gameObject.SetActive(IsSelected);
        //mediaList?.gameObject.SetActive(IsSelected);

    }

    public void SetActiveMedia(string mediaName)
    {
        foreach (GameObject media in medias)
        {
            if (media.name.Equals(mediaName))
            {
                media.SetActive(true);
            } else
            {
                media.SetActive(false);
            }
        }
    }
}

