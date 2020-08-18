
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TextIcon : MonoBehaviour, MenuIcon
{
    [SerializeField]
    private GameObject textCanvas;
    //[SerializeField]
    //private Camera arCamera;
    //[SerializeField]
    //private Camera mainCamera;

    void Awake()
    {
        //textCanvas = GameObject.Find("TextCanvas");
    }
    public void OnDrop()
    {
        textCanvas.SetActive(true);
        //mainCamera.gameObject.SetActive(true);
        //arCamera.gameObject.SetActive(false);
    }
}
