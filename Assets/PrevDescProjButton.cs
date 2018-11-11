﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrevDescProjButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void click()
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.SendToDispatch();
    }
}
