﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveRessourceButton : MonoBehaviour {

    int idToRemove = 0;
    string projectId = "";
    string projectName;
    GameObject model;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setIdToRemove(int id)
    {
        idToRemove = id;
    }

    public void setProjectId(string id)
    {
        projectId = id;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }
    void callDispatcher(string json)
    {
        
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("project_id", projectId);
        but.addParam("id", projectId);
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }

    void click()
    {
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();

        modelScr.removeElem(idToRemove, projectName, callDispatcher);

     
    }
}
