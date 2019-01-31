﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ModifyRessourceController : BasicController
{

    // Use this for initialization
    // Use this for initialization
    GameObject model;
    [SerializeField]
    GameObject confirmButton;
    [SerializeField]
    GameObject removeButton;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject projRessource;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void applyInServerResponse(string json)
    {
        Dictionary<string, string> ressourceData = new Dictionary<string, string>();
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ressourceData.Add("name", resp["name"].ToString());
        ressourceData.Add("description", resp["description"].ToString());
        ressourceData.Add("fk_id_project", resp["fk_id_project"].ToString());
        inputName.GetComponent<TMP_InputField>().text = ressourceData["name"];
        inputDesc.GetComponent<TMP_InputField>().text = ressourceData["description"];
    }

    public override void apply()
    {
        int id = int.Parse(args["id"]);

        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();
        modelScr.find(id, applyInServerResponse);  
        ConfirmModifyRessource butScr = confirmButton.GetComponent<ConfirmModifyRessource>();
        butScr.setIdToModify(id);
        butScr.setProjectId(args["project_id"]);
        RemoveRessourceButton rmButScr = removeButton.GetComponent<RemoveRessourceButton>();
        rmButScr.setProjectId(args["project_id"]);
        rmButScr.setIdToRemove(id);
    }
}