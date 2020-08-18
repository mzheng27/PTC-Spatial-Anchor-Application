using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageIcon : MonoBehaviour, MenuIcon
{
    [SerializeField]
    private GameObject imageCanvas;

    void Awake()
    {
       
    }
    public void OnDrop()
    {
        imageCanvas.SetActive(true);
        
    }
}
