﻿using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

using System;


public class ModelTest : MonoBehaviour {
    apiConnection api;
    static int i = 0;
	// Use this for initialization
	void Start () {
        api = GameObject.Find("api_connection").GetComponent<apiConnection>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void addCollections(string name, string min, string max,
        string desc, string nb_card, string nb_re, Action<string> call)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("async_game", "0");
        toAdd.Add("turn_game", "1");
        toAdd.Add("min_player", min);
        toAdd.Add("max_player", max);
        toAdd.Add("description", desc);
        print("IN MODEL");
        api.request(toAdd, "/api/project", "POST", call);
       
    }

    public void find(int id, Action<string> callback)
    {
        api.request(null, "/api/project/" + id.ToString() + "/", "GET", callback);   
    }


    public int getNbElement()
    {
        return (ModelTest.i);
    }

    public void getAll(Action<string> callback)
    {
        api.request(null, "/api/project", "GET", callback);
    }

    public void updateField(string id, string name, string min, string max,
        string desc)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("async_game", "0");
        toAdd.Add("turn_game", "1");
        toAdd.Add("min_player", min);
        toAdd.Add("max_player", max);
        toAdd.Add("description", desc);
        api.request(toAdd, "/api/project/" + id+ "/", "PUT", null);
       
    }

    public void removeElem(int id)
    {

        api.request(null, "/api/project/" +id.ToString()+"/", "DELETE", null);
    }
}