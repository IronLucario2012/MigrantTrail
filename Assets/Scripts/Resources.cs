using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour {

    public EventScreenScript eventScript; //Instance of game event handling script

    private static int food = 0; //Amount of food in units of how much Eva and Cassie eat over a certain distance
    private static int medicine = 0;
    private static int valuables = 0;

    private static string[] healthStates = {"Dead","Very unwell","Unwell","Healthy"};
    private static int motherHealth = 3;
    private static int childHealth = 3;

    public static Text foodText;
    public static Text medicineText;
    public static Text valuablesText;
    public static Text mHealthText;
    public static Text cHealthText;

    static readonly string foodLabel = "Food: ";
    static readonly string medicineLabel = "Medicine: ";
    static readonly string valuablesLabel = "Valuables: ";
    static readonly string mHealthLabel = "Eva's Health: ";
    static readonly string cHealthLabel = "Cassie's Health: ";

    // Use this for initialization
    void Start () {
        foodText = GameObject.Find("FoodText").GetComponent<Text>();
        medicineText = GameObject.Find("MedicineText").GetComponent<Text>();
        valuablesText = GameObject.Find("ValuablesText").GetComponent<Text>();
        mHealthText = GameObject.Find("MotherHealthText").GetComponent<Text>();
        cHealthText = GameObject.Find("ChildHealthText").GetComponent<Text>();
        eventScript = EventScreenScript.Instance();
    }
	
	// Update is called once per frame
	void Update () {
        if (childHealth <= 0) CharacterIsDead(false);
        if (motherHealth <= 0) CharacterIsDead(true);
    }

    #region Getters & Setters

    public static int GetFood() { return food; }
    public static void SetFood(int amount)
    {
        if (amount + food >= 0)
        {
            food += amount;
        }
        else
        {
            food = 0;
            if (Random.Range(0, 4) > 2)
                SetCHealth(-1);
            else
                SetMHealth(-1);
        }
        foodText.text = foodLabel + food;
    }

    public static int GetMedicine() { return medicine; }
    public static void SetMedicine(int amount)
    {
        if (amount + medicine >= 0)
        {
            medicine += amount;
        }
        else
        {
            medicine = 0;
            if (Random.Range(0, 4) > 2)
                SetCHealth(-1);
            else
                SetMHealth(-1);
        }
        medicineText.text = medicineLabel + medicine;
    }

    public static int GetValuables() { return valuables; }
    public static void SetValuables(int amount)
    {
        if (amount + valuables >= 0)
        {
            valuables += amount;
        }
        else
        {
            valuables = 0;
            SetFood(-2);
            foodText.text = foodLabel + food;
        }
        valuablesText.text = valuablesLabel + valuables;
    }

    public static string GetMHealth()
    {
        if (motherHealth >= 0 && motherHealth < healthStates.Length)
            return healthStates[motherHealth];
        else
            return healthStates[0];
    }
    public static void SetMHealth(int amount)
    {
        if (amount + motherHealth >= 0)
        {
            if (amount + motherHealth < healthStates.Length)
            {
                motherHealth += amount;
                mHealthText.text = mHealthLabel + GetMHealth();
            }
            else
            {
                motherHealth = healthStates.Length - 1;
                mHealthText.text = mHealthLabel + GetMHealth();
            }
        }
        else
        {
            motherHealth = 0;
            mHealthText.text = mHealthLabel + GetMHealth();
        }
    }

    public static string GetCHealth()
    {
        if (childHealth >= 0 && childHealth < healthStates.Length)
            return healthStates[childHealth];
        else
            return healthStates[0];
    }
    public static void SetCHealth(int amount)
    {
        if (amount + childHealth >= 0)
        {
            if (amount + childHealth < healthStates.Length)
            {
                childHealth += amount;
                cHealthText.text = cHealthLabel + GetMHealth();
            }
            else
            {
                childHealth = healthStates.Length - 1;
                cHealthText.text = cHealthLabel + GetMHealth();
            }
        }
        else
        {
            childHealth = 0;
            cHealthText.text = cHealthLabel + GetMHealth();
        }
    }

    public static void SetAllResources(int newFood, int newMedicine, int newValuables, int newMHealth, int newCHealth)
    {
        SetFood(newFood);
        SetMedicine(newMedicine);
        SetValuables(newValuables);
        SetMHealth(newMHealth);
        SetCHealth(newCHealth);
    }
    public static void SetAllResources(int[] amounts)
    {
        SetFood(amounts[0]);
        SetMedicine(amounts[1]);
        SetValuables(amounts[2]);
        SetMHealth(amounts[3]);
        SetCHealth(amounts[4]);
    }
    #endregion

    //3 mother, 4 daughter

    public void CharacterIsDead(bool isMother)
    {
        eventScript.StartEvent(GlobalVars.GetEventByID(isMother ? 3 : 4));
    }
}
