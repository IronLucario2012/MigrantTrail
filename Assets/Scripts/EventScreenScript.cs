using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class EventScreenScript : MonoBehaviour {

    public Text eventText;
    public Button button1;
    public Button button2;
    public Button button3;
    public GameObject eventPanelObject;

    private static EventScreenScript eventScreenScript;

    public static EventScreenScript Instance()
    {
        if (!eventScreenScript)
        {
            eventScreenScript = FindObjectOfType(typeof(EventScreenScript)) as EventScreenScript;
            if (!eventScreenScript)
            {
                Debug.LogError("Can't find this script on any objects.");
            }
        }

        return eventScreenScript;
    }

    public void StartEvent(GameEvent gameEvent)
    {
        if (gameEvent.ending) SceneManager.LoadScene("GameOverScene");

        GlobalVars._speedMultiplier = 0;
        GlobalVars._isInEvent = true;

        eventPanelObject.SetActive(true);
        eventText.text = gameEvent.text;

        button1.gameObject.SetActive(true);
        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(() => Resources.SetAllResources(gameEvent.option1Results));
        button1.GetComponentInChildren<Text>().text = gameEvent.option1Text;
        button1.onClick.AddListener(EndEvent);
        if(gameEvent.eventsToTrigger[0] != -1)
            button1.onClick.AddListener(() => StartEvent(GlobalVars.GetEventByID(gameEvent.eventsToTrigger[0])));

        if (!string.IsNullOrEmpty(gameEvent.option2Text))
        {
            button2.gameObject.SetActive(true);
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(() => Resources.SetAllResources(gameEvent.option2Results));
            button2.GetComponentInChildren<Text>().text = gameEvent.option2Text;
            button2.onClick.AddListener(EndEvent);
            if (gameEvent.eventsToTrigger[1] != -1)
                button2.onClick.AddListener(() => StartEvent(GlobalVars.GetEventByID(gameEvent.eventsToTrigger[1])));
        }
        else
        {
            button2.gameObject.SetActive(false);
        }
        if (!string.IsNullOrEmpty(gameEvent.option3Text))
        {
            button3.gameObject.SetActive(true);
            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(() => Resources.SetAllResources(gameEvent.option3Results));
            button3.GetComponentInChildren<Text>().text = gameEvent.option3Text;
            button3.onClick.AddListener(EndEvent);
            if (gameEvent.eventsToTrigger[2] != -1)
                button3.onClick.AddListener(() => StartEvent(GlobalVars.GetEventByID(gameEvent.eventsToTrigger[2])));
        }
        else
        {
            button3.gameObject.SetActive(false);
        }
    }

    void EndEvent()
    {
        eventPanelObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);

        GlobalVars._speedMultiplier = 1;
        GlobalVars._isInEvent = false;
    }
}


