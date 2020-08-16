
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioIcon : MonoBehaviour, MenuIcon 
{
    [SerializeField]
    private GameObject audioScene;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Camera mainCamera;

    void Awake()
    {
        
    }
    public void OnDrop()
    {
        audioScene.SetActive(true);
        mainCamera.gameObject.SetActive(true);
        arCamera.gameObject.SetActive(false);
    }
   
}
