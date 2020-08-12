using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextIcon : MonoBehaviour, MenuIcon
{
    public void OnDrop()
    {
        SceneManager.LoadScene("TextScene");
    }
}
