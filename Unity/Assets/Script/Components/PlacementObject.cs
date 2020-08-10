
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

   
    [SerializeField]
    private Canvas canvasComponent;

   

    public void ToggleCanvas()
    {
        canvasComponent?.gameObject.SetActive(IsSelected);
    }
}
