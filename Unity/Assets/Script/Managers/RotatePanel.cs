using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePanel : MonoBehaviour
{
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var facinguser = Camera.main.transform.position;
        Panel.transform.LookAt(facinguser);
        var oldrotation = Panel.transform.rotation;
        Panel.transform.rotation = Quaternion.Euler(-20f, oldrotation.eulerAngles.y, 90.0f);

    }
}
