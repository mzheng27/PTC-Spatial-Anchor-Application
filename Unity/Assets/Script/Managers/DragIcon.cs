
using System.Collections.Generic;
using UnityEngine;

public class DragIcon : MonoBehaviour
{
    private bool allowMove;
    //private ARRaycastManager arRaycastManager;
    public Transform itemBar;

    // Start is called before the first frame update
    void Awake()
    {
        //arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        allowMove = true;
                    }
                    break;
                case TouchPhase.Moved:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && allowMove)
                    {
                        transform.position = touchPos;
                    }
                    break;

                case TouchPhase.Ended:
                    allowMove = false;
                    RectTransform panelOutline = itemBar as RectTransform;
                    if (!RectTransformUtility.RectangleContainsScreenPoint(panelOutline, touchPos))
                    {
                        Debug.Log("Add");
                    }
                    transform.localPosition = Vector3.zero;

                    break;
            }
        }
    }
}