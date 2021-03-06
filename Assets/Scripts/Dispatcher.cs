﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dispatcher : MonoBehaviour {
    static public Dispatcher Instance = new Dispatcher();
    [SerializeField]
    private GameObject LoginUi;
    [SerializeField]
    private GameObject HomeUi;

    [SerializeField]
    private GameObject NewProjUi;

    [SerializeField]
    private GameObject DescProjUi;

    [SerializeField]
    private GameObject CreatePhaseUi;

    [SerializeField]
    private GameObject ModifyPhaseUi;

    [SerializeField]
    private GameObject ActiveUi;

    [SerializeField]
    private GameObject ProjectListUi;

    [SerializeField]
    private GameObject ModifyProjectUi;

    [SerializeField]
    private GameObject RessourceListUi;

    [SerializeField]
    private GameObject ModifyRessourceUi;

    [SerializeField]
    private GameObject CardListUi;

    [SerializeField]
    private GameObject ModifyCardUi;

    [SerializeField]
    private GameObject EditVisualCardUi;
    [SerializeField]
    private GameObject OverViewUi;
    [SerializeField]
    private GameObject NewRessourceUi;
    [SerializeField]
    private GameObject CreateRuleUi;
    [SerializeField]
    private GameObject ModifyRuleUi;
    [SerializeField]
    private GameObject RulesListUi;

    [SerializeField]
    private GameObject CreateCardRuleUi;

    [SerializeField]
    private GameObject CardRulesListUi;

    [SerializeField]
    private GameObject ModifyCardRuleUi;
    [SerializeField]
    private GameObject playFirstCardFromPhase;
    [SerializeField]
    private GameObject playAnyCardFromPhase;
    [SerializeField]
    private GameObject evaluateCardDuel;
    [SerializeField]
    private GameObject determineWinner;
    [SerializeField]
    private GameObject drawNCard;
    private Dictionary<string, GameObject> UiMap;
	// Use this for initialization
	void Start () {
        UiMap = new Dictionary<string, GameObject>();
        UiMap["LoginUi"] = LoginUi;
        UiMap["HomeUi"] = HomeUi;
        UiMap["NewProjUi"] =  NewProjUi;
        // UiMap["DescProjUi"] =   Instantiate(DescProjUi) as GameObject;
        UiMap["ModifyPhaseUi"] = ModifyPhaseUi;
        UiMap["CreatePhaseUi"] =  CreatePhaseUi;
        UiMap["ProjectListUi"] = ProjectListUi;
        UiMap["ModifyProjectUi"] =  ModifyProjectUi;
       // UiMap["RessourceListUi"] =   Instantiate(RessourceListUi) as GameObject; 
        UiMap["ModifyRessourceUi"] = ModifyRessourceUi;
        UiMap["CardListUi"] = CardListUi;
        UiMap["ModifyCardUi"] =  ModifyCardUi;
        UiMap["EditVisualCardUi"] = EditVisualCardUi;
        UiMap["NewRessourceUi"] =  NewRessourceUi;
        UiMap["OverviewProjUi"] =  OverViewUi;
        UiMap["CreateRuleUi"] = CreateRuleUi;
        UiMap["RulesListUi"] = RulesListUi;
        UiMap["ModifyRuleUi"] = ModifyRuleUi;
        UiMap["CreateCardRulesUi"] = CreateCardRuleUi;
        UiMap["CardRulesListUi"] = CardRulesListUi;
        UiMap["ModifyCardRuleUi"] = ModifyCardRuleUi;
        UiMap["playFirstCardFromPhase"] = playFirstCardFromPhase;
        UiMap["playAnyCardFromPhase"] = playAnyCardFromPhase;
        UiMap["evaluateCardDuel"] = evaluateCardDuel;
        UiMap["determineWinner"] = determineWinner;
        UiMap["drawNCard"] = drawNCard;
        dispatch("LoginUi", "LoginController", new Dictionary<string, string>());
        //dispatch("NewProjUi", "NewProjController", new List<string>());

    }

    // Update is called once per frame
    void Update () {
	
	}

   
   

    public void dispatch(string UiName, string UiScript, Dictionary<string,string> param)
    {
     /*   Dictionary<string, string[]> l = new Dictionary<string, string[]>();
        string[] d = new string[7];
        d[0] = "prolo";
        d[1] = "smic";
        l.Add("khey", d);
        
        print();*/
        if (ActiveUi)
        {
                Destroy(ActiveUi);
        }
      
            ActiveUi = Instantiate(UiMap[UiName]) as GameObject;
            ActiveUi.tag = "active";
            ActiveUi.transform.SetParent(gameObject.transform, false);
        
        
        BasicController UiController = ActiveUi.GetComponent(UiScript) as BasicController;
        UiController.setParams(param);
        UiController.apply();
        ActiveUi.transform.SetParent(gameObject.transform, false);
    }
}
