﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;


/*[{id:1,vars:[1,3, '+', '-', 3]},{id:2,vars:[1, '>', 3]}]*/
public class buttonSetRulesList : MonoBehaviour
{
    [SerializeField]
    GameObject inputValue;
    [SerializeField]
    private GameObject rulesSetText;
    [SerializeField]
    private GameObject displayedRulest;
    private List<GameObject> toggleList;
    [SerializeField]
    GameObject objList;
    [SerializeField]
    string[] stringList;
    Dictionary<string, string> rulesSetList;
    [SerializeField]
    GameObject m_toggle;
    [SerializeField]
    GameObject Layout;
    private List<string> toSend;
    private string selectedOne;
    private string originalRules;
    private Dictionary<string, GameObject> selectedInput;
    private Dictionary<string, string> selectedOp;
    private List<string> opKeyList;
    // Start is called before the first frame update
    void Start()
    {
        opKeyList = new List<string>();
        selectedOp = new Dictionary<string, string>();
        selectedInput = new Dictionary<string, GameObject>();
        rulesSetList = new Dictionary<string, string>();
        rulesSetList.Add("playFirstCardFromPhase", "Play the first card from the phase :p1-0");
        rulesSetList.Add("playAnyCardFromPhase", "Play any card from phase number :p1-0");
        rulesSetList.Add("evaluateCardDuel", "for phases :p1-n if player 1 have the strongest ressource  beetween $r1-2 and $r2-3 do to $j1-0  $p1-6 $o1-4 $v1-5 " +
            "if player 2 have the strongest ressource beetween $r1-2 and $r2-3 do to $j2-1  $p3-6 $o1-4 $v1-5 $o2-6" +
            "");
        rulesSetList.Add("determineWinner", "for phases :p1-n if player 1 's ressources is $l1-0 $r1-1 than the player 2, then player 1 win other wise," +
            "the player 2 win ");
        toSend = new List<string>();
        toggleList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<string, GameObject> select in selectedInput)
        {
            if (!select.Value.GetComponent<InputField>().text.Equals(""))
            {
                if (selectedOp.ContainsKey(select.Key))
                {
                    if (!selectedOp[select.Key].Equals(select.Value.GetComponent<InputField>().text))
                    {
                        selectedOp[select.Key] = select.Value.GetComponent<InputField>().text;
                        updateRulesTextForOp();
                        maskParameters();
                    }
                }
                else
                    selectedOp.Add(select.Key, select.Value.GetComponent<InputField>().text);
            }

        }
        maskParameters();
    }

    public string getSelectedRules()
    {
        return (selectedOne);
    }
    public Dictionary<string, string> getSelectedList()
    {
        return (selectedOp);
    }
    public string updateRulesTextForOp()
    {
        string rulesString = originalRules;
        string to_display = originalRules;
        foreach(string key in opKeyList)
        {
            if (!selectedOp.ContainsKey(key))
            {
                to_display = rulesString.Replace(key, "<color=#FFEB03>Value n°" + key[2]+"</color>");
            }
        }
        foreach (KeyValuePair<string, string> op in selectedOp)
        {
            to_display = rulesString.Replace(op.Key, "<color=green>" + op.Value + "</color>");
            rulesString = rulesString.Replace(op.Key, op.Value);
        }
        print(to_display);
        rulesSetText.GetComponent<TextMeshProUGUI>().text = rulesString;
        displayedRulest.GetComponent<TextMeshProUGUI>().text = to_display;

        return (rulesString);
    }

    private void maskParameters()
    {
        string rulesString = rulesSetText.GetComponent<TextMeshProUGUI>().text;
        string toDisplay = displayedRulest.GetComponent<TextMeshProUGUI>().text;
        string key;
        string repl = "";
        int lastIdx = 0;

        while ((lastIdx = rulesString.IndexOf("$", lastIdx)) != -1)
        {
            if (lastIdx >= 0)
            {
                key = rulesString.Substring(lastIdx, 5);
                lastIdx += 5;
                if (key.Contains("l"))
                    repl = "(Logical Operator n°" + key[2] +")";
                if (key.Contains("o"))
                    repl = "(Operator n°" + key[2] + ")";
                if (key.Contains("v"))
                    repl = "(Value n°" + key[2] + ")";
                if (key.Contains("p"))
                    repl = "(player Ressources n°" + key[2] + ")";
                if (key.Contains("j"))
                    repl = "(player n°" + key[2] + ")";
                if (key.Contains("r"))
                    repl = "(ressources n°" + key[2] + ")";
                toDisplay = toDisplay.Replace(key, repl);
            }

        }
        rulesString = toDisplay;
        lastIdx = 0;
        while ((lastIdx = rulesString.IndexOf(":", lastIdx)) != -1)
        {
            if (lastIdx >= 0)
            {
                key = rulesString.Substring(lastIdx, 5);
                lastIdx += 5;
                if (key.Contains("p"))
                    repl = "(Phases n°" + key[2] + ")";
                toDisplay = toDisplay.Replace(key, repl);
            }

        }
        displayedRulest.GetComponent<TextMeshProUGUI>().text = toDisplay;
    }
    private void parseRulesOpText()
    {
        string rulesString = rulesSetText.GetComponent<TextMeshProUGUI>().text;
        string key;
        int lastIdx = 0;

        while ((lastIdx = rulesString.IndexOf("$v", lastIdx)) != -1)
        {
            if (lastIdx >= 0)
            {
                key = rulesString.Substring(lastIdx, 5);
                lastIdx += 5;
                if (!opKeyList.Contains(key))
                    opKeyList.Add(key);
            }

        }
    }

    public void clearContent()
    {
        foreach (Transform c in objList.transform.GetChildren())
        {
            Destroy(c.gameObject);
        }
    }

    public void clearInputList()
    {
        if (selectedInput != null)
        {
            foreach (KeyValuePair<string, GameObject> input in selectedInput)
            {
                Destroy(input.Value);
            }
            selectedInput.Clear();
        }
    }

    public void setSelectedList(List<string> selection)
    {
        toSend = selection;
    }

    private void unCheckAll(string togstring)
    {
        string itemString;
        foreach(GameObject item in toggleList)
        {
                itemString = item.GetComponentInChildren<Text>().text;
            if (itemString.Equals(togstring) == false)
                item.GetComponent<Toggle>().isOn = false;
        }
    }
    public void setSelection()
    {
       
        clearContent();
        toggleList.Clear();
        foreach (string elem in stringList)
        {
            GameObject t = Instantiate(m_toggle) as GameObject;
            Toggle toggle = t.GetComponent<Toggle>();

            if (toSend.Where(x => x.Contains(elem)).FirstOrDefault() == null)
                toggle.isOn = false;
            if (elem.Equals(selectedOne))
                toggle.isOn = true;
            t.GetComponentInChildren<Text>().text = elem;

            toggle.onValueChanged.AddListener(delegate
            {
                string togString = toggle.GetComponentInChildren<Text>().text;
                if (toggle.isOn)
                {
                    if (opKeyList != null)
                        opKeyList.Clear();
                    clearInputList();
                    unCheckAll(togString);      
                    selectedOne = togString;
                    rulesSetText.GetComponent<TextMeshProUGUI>().text = rulesSetList[selectedOne];
                    originalRules = rulesSetList[selectedOne];
                    parseRulesOpText();
                   
                    updateRulesTextForOp();
                    maskParameters();
                    foreach (string str in opKeyList)
                    {
                        GameObject input = Instantiate(inputValue) as GameObject;
                        input.transform.SetParent(Layout.transform, false);
                        selectedInput.Add(str, input);
                       
                    }
                }
                else
                    toSend.RemoveAll(x => x.Contains(togString));
            });
            t.transform.SetParent(objList.transform, false);
            toggleList.Add(t);
        }

    }


}
