using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneManager : MonoBehaviour
{
    private int numplanes;
    public GameObject startcanvas;
    public GameObject scanRoomPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var planeManager = GetComponent<ARPlaneManager>();
        foreach (var plane in planeManager.trackables)
        {
            numplanes++;
        }

        if (numplanes > 2 && scanRoomPanel.activeSelf == true)
        {
            
            startcanvas.gameObject.SetActive(true);
            scanRoomPanel.SetActive(false);
        }
    }
}
