﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaDebug : MonoBehaviour
{
    public Text media;
    public GameObject mediaElements;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        media.text = mediaElements.activeSelf.ToString();
    }
}
