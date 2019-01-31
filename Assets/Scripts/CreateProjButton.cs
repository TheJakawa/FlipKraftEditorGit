﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class CreateProjButton : MonoBehaviour {

    [SerializeField]
    GameObject Model;

    [SerializeField]
    GameObject desc;
	// Use this for initialization
	void Start () {
        Model = GameObject.Find("Model");
        gameObject.GetComponent<Button>().onClick.AddListener(click);

    }

    // Update is called once per frame
    void Update () {
		
	}

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    void applyInServerResponse(string json)
    {
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        int id = int.Parse(resp["id"].ToString());
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("id", id.ToString());
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        print("after api");
        but.setParams(param);
        but.SendToDispatch();
    }

    void click()
    {

        ModelTest ModelScript = Model.GetComponent<ModelTest>();
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("description", desc.GetComponent<TMP_InputField>().text);
        Dictionary<string, string> param = but.getParam();

        ModelScript.addCollections(param["name"], param["min"], param["max"], param["description"], "0", "0", applyInServerResponse);
        param.Clear();
        
    }
}