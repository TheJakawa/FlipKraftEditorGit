﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public override void apply()
    {
        int id = int.Parse(args["id"]);
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScr = model.GetComponent<ModelRessource>();

        //projNameTitle.GetComponent<TextMeshProUGUI>().text = modelScr.find(id, "name");

        inputName.GetComponent<TMP_InputField>().text = modelScr.find(id, "name");
        
        inputDesc.GetComponent<TMP_InputField>().text = modelScr.find(id, "description");

        ConfirmModifyRessource butScr = confirmButton.GetComponent<ConfirmModifyRessource>();
        butScr.setIdToModify(id);
        butScr.setProjectId(args["project_id"]);
        RemoveRessourceButton rmButScr = removeButton.GetComponent<RemoveRessourceButton>();
        rmButScr.setProjectId(args["project_id"]);
        rmButScr.setIdToRemove(id);

    }
}