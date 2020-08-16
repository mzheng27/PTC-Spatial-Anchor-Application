using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageIcon : MonoBehaviour, MenuIcon
{
    [SerializeField]
    private GameObject imageCanvas;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Camera mainCamera;
    void Awake()
    {
       
    }
    public void OnDrop()
    {
        imageCanvas.SetActive(true);
        mainCamera.gameObject.SetActive(true);
        arCamera.gameObject.SetActive(false);
    }
}
