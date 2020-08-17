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

    [SerializeField]
    private GameObject mediaList;

    [SerializeField]
    private GameObject video;

    [SerializeField]
    private GameObject image;

    [SerializeField]
    private GameObject text;


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
        mediaList?.gameObject.SetActive(IsSelected);
        
    }

    public void SetActiveMedia(bool value)
    {
        video.SetActive(value);
        text.SetActive(value);
        image.SetActive(value);
    }
}

