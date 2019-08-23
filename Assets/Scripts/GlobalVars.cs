using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GlobalVars : MonoBehaviour {

    public static readonly int _distance = 2197; //Distance in km from start to destination
    public static float _distanceInWorld; //Sum of the distances between nodes in the world map
    public static float _distanceRatio; //How many World units per km there are
    public static int _currentDistance; //Current travelled distance in km
    public static float _currentDistanceInWorld; //Current travelled distance in world units
    public static bool _isInEvent; //Whether or not there is currently an event triggered
    public static int _speedMultiplier; //Amount for speed to be multiplied by; 0 for during events, 1 during normal gameplay
    public static GameEventList _gameEvents; //Container with an array of the in-game events that can happen
    public static bool _isGoodEnding; //Which ending did the player get

    public TextAsset gameEventsFile; //File containing the list of events
    public EventScreenScript eventScript; //Instance of game event handling script
    
    public Text kmText;
    public string kmWords = "Distance Travelled (km): ";
    
    void Awake ()
    {
        _currentDistance = 0;
        _currentDistanceInWorld = 0f;
        _distanceInWorld = GetWorldDistance();
        _distanceRatio = _distanceInWorld / _distance;
        _isInEvent = false;
        _speedMultiplier = 0;
        _isGoodEnding = false;
    }

    void Start()
    {
        _gameEvents = JsonUtility.FromJson<GameEventList>(gameEventsFile.text);
        eventScript = EventScreenScript.Instance();
        eventScript.StartEvent(_gameEvents.events[0]); //Starts introductory event
    }

    float GetWorldDistance()
    {
        int numberOfNodes = 5;
        float result = 0f;
        
        for (int i=1;i<numberOfNodes;i++)
        {
            Transform pos1 = GameObject.Find("Checkpoint " + (i-1)).GetComponent<Transform>();
            Transform pos2 = GameObject.Find("Checkpoint " + i).GetComponent<Transform>();
            result += Vector3.Distance(pos1.position, pos2.position);
        }

        return result;
    }

    private void Update()
    {
        if(_currentDistance < _distance)
        {
            kmText.text = kmWords + _currentDistance + "/" + _distance;
            int checkDistance = (int)(_currentDistanceInWorld / _distanceRatio);
            if(_currentDistance != checkDistance)
            {
                _currentDistance = checkDistance;
                DistanceUpdate();
            }
        }
    }

    public void DistanceUpdate()
    {
        foreach(GameEvent ge in _gameEvents.events)
        {
            if (ge.triggerDistance == _currentDistance)
                eventScript.StartEvent(ge);
        }
    }

}

[Serializable]
public class GameEvent
{
    public readonly int id;
    public bool triggered = false; //True if the event needs to be triggered, false if it can happen randomly
    public int triggerDistance = -1; //Distance in KM at which the event is triggered, if it is a distance-triggered event. -1 if it's not
    public string text = "";
    public int[] eventsToTrigger = new int[3]; //Event(s) to trigger on completion, if any. -1 means none.

    public string option1Text = "";
    public int[] option1Results = new int[5];
    public string option2Text = "";
    public int[] option2Results = new int[5];
    public string option3Text = "";
    public int[] option3Results = new int[5];
}

[Serializable]
public class GameEventList
{
    public GameEvent[] events;
}