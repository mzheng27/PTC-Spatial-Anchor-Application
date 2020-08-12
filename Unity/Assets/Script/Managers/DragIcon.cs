using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragIcon : MonoBehaviour
{
    private bool allowMove;
    
    public Transform itemBar;

    public Text log;
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
                        //if (isSound)
                        //{
                        //    log.text = "AudioScene";
                        //    SceneManager.LoadScene("AudioScene");
                        //} else if (isText)
                        //{
                        //    log.text = "textScene";
                        //    SceneManager.LoadScene("TextScene");
                        //} else if (isImage)
                        //{
                        //    log.text = "ImageScene";
                        //    SceneManager.LoadScene("ImageScene");
                        //} 
                    }
                    //transform.localPosition = Vector3.zero;
                    break;
            }
        }
    }
}