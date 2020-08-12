using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTransition : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private Camera mainCamera;

    public void storageBack()
    {
        arCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }
}
