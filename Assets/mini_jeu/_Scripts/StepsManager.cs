using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsManager : MonoBehaviour
{
    public List<Step> steps = new List<Step>();
    public Color activeColor = Color.green;  // Color when step is active
    public Color inactiveColor = Color.gray;  // Color when step is inactive
    private Manager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.Instance;
        manager.OnSameCards += UpdateStepsColor;
        
        foreach (Step step in steps)
        {
            step.SetColor(inactiveColor);
        }
    }

    //Method to update the color of the steps
    public void UpdateStepsColor(CardModel cardModel)
    {
        for (int i = 0; i < steps.Count; i++)
        {
            if (i <= manager.NumberPairsFound)
            {
                steps[i].SetColor(activeColor);
                Debug.Log("Step " + i + " is active");
            }
            else
            {
                steps[i].SetColor(inactiveColor);
            }
        }
    }
    
}
