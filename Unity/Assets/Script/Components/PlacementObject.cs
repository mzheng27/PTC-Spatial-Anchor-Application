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
    private Canvas canvasComponent;

    [SerializeField]
    private GameObject mediaList;


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
}
