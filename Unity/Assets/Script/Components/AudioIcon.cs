
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioIcon : MonoBehaviour, MenuIcon 
{
    public void OnDrop()
    {
        SceneManager.LoadScene("AudioScene");
    }
   
}
