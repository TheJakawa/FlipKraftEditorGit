﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections.Generic;

using TMPro;
using Newtonsoft.Json;

public class RessourcesListController : BasicController {

    int projectId;

    [SerializeField]
    GameObject overviewButton;

    [SerializeField]
    GameObject createRessourceButton;

    [SerializeField]
    GameObject elemInList;

    [SerializeField]
    GameObject listOfProj;

    // Use this for initialization
    void Start () {
		
	}

    
    // Update is called once per frame
    void Update () {
		
	}


    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }


    public void applyInServerResponse(string json)
    {
        Dictionary<int, Dictionary<string, string>> allRessource = new Dictionary<int, Dictionary<string, string>>();
        List<object> respList = DeserializeJson<List<object>>(json);
        int i = 0;

        foreach (object obj in respList)
        {
            Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(obj.ToString());
            Dictionary<string, string> ressourceData = new Dictionary<string, string>();

            ressourceData.Add("name", resp["name"].ToString());
            ressourceData.Add("description", resp["description"].ToString());
            ressourceData.Add("id", resp["id"].ToString());
            ressourceData.Add("fk_id_project", resp["fk_id_project"].ToString());
            allRessource.Add(i, ressourceData);
            i++;
        }
        foreach (KeyValuePair<int, Dictionary<string, string>> project in allRessource)
        {
            GameObject toAdd = Instantiate(elemInList) as GameObject;
            print(project.Value["name"]);
            print(project.Value["description"]);
            toAdd.transform.Find("RessourceName").GetComponent<TextMeshProUGUI>().text = project.Value["name"];
            toAdd.transform.Find("RessourceDesc").GetComponent<TextMeshProUGUI>().text = project.Value["description"];
            ModifyRessourceButton toAddScr = toAdd.GetComponent<ModifyRessourceButton>();
            toAddScr.setIdToModify(project.Value["id"]);
            toAddScr.setProjectId(project.Value["fk_id_project"]);
            toAdd.transform.SetParent(listOfProj.transform, false);
        }

    }

    public override void apply()
    {
        GameObject model;

        projectId = int.Parse(args["project_id"]);
        model = GameObject.Find("ModelRessource");
        ModelRessource modelScript = model.GetComponent<ModelRessource>();
        modelScript.getAll(projectId.ToString(), applyInServerResponse);
     
        overviewButton.GetComponent<OverviewButton>().setCurrentProjectId(projectId);
        createRessourceButton.GetComponent<CreateRessourceButton>().setProjectId(projectId);   
    }
}
