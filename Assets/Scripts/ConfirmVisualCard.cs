﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmVisualCard : MonoBehaviour
{

    // Use this for initialization
    int idToModify;
    string projectId;
    GameObject model;
    GameObject modelAssoc;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    private List<Dictionary<string, string>> assocListToModify;

    public void addAssocToModify(string pId, string cId, string rId, string pX, string pY, string val, string aId = null)
    {
        Dictionary<string, string> assoc = new Dictionary<string, string>();
        assoc["project_id"] = pId;
        assoc["card_id"] = cId;
        assoc["ressource_id"] = rId;
        assoc["posX"] = pX;
        assoc["posY"] = pY;
        assoc["assoc_id"] = aId;
        assoc["value"] = val;
        assocListToModify.Add(assoc);
    }

    void Start()
    {
        assocListToModify = new List<Dictionary<string, string>>();
        gameObject.GetComponent<Button>().onClick.AddListener(click);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setIdToModify(int id)
    {
        idToModify = id;
    }

    public void setProjectId(string id)
    {
        projectId = id;
    }



    void CallDispatcher(string json)
    {
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
    
        but.addParam("id", projectId);
        but.addParam("project_id", projectId);
        but.SendToDispatch();
    }

    void sendList()
    {
        print("sending list...");
        modelAssoc = GameObject.Find("ModelAssociation");
        ModelAssociation modelScr = modelAssoc.GetComponent<ModelAssociation>();
        foreach (Dictionary<string,string> l in assocListToModify)
        {
            print("sending e..");
            print(l["posX"]);
         if (l["assoc_id"] == null)
          modelScr.addCollections(l["value"], l["project_id"], l["card_id"], l["ressource_id"], 
              l["posX"],
              l["posY"]);
        else
          modelScr.updateField(l["assoc_id"], l["value"], l["project_id"], l["card_id"], l["ressource_id"],
              l["posX"],
              l["posY"]);
        }
    }

    void click()
    {
        model = GameObject.Find("ModelCard");
        ModelCard modelScr = model.GetComponent<ModelCard>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        sendList();
        modelScr.updateField(idToModify.ToString(), name, desc, projectId, CallDispatcher);
     
    }
}
