using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private ItemDragHandler dragHandler; 
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform menuBar = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(menuBar, Input.mousePosition))
        {
            dragHandler.OnEndDrag(eventData);
        }    
    }
}
