using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
   
    private ItemDragHandler dragHandler;

    void Awake()
    {
        dragHandler = FindObjectOfType<ItemDragHandler>();
    }
    [SerializeField]
    private GameObject dragDropPanel;
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform menuBar = dragDropPanel.transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(menuBar, Input.mousePosition))
        {
            dragDropPanel.SetActive(false);
            dragHandler.OnEndDrag(eventData);
        }    
    }
}
