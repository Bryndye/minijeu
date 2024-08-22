using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepsManager : MonoBehaviour
{
    private Manager manager;
    
    public List<Step> steps = new List<Step>();
    public Color ActiveColor = Color.green;  // Color when step is active
    public Color CurrentColor = Color.cyan;
    public Color InactiveColor = Color.gray;  // Color when step is inactive

    [SerializeField] private Image progressBar;

    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.Instance;
        manager.OnSameCards += UpdateStepsColor;
        
        foreach (Step step in steps)
        {
            step.SetBorder(InactiveColor);
            step.SetColor(InactiveColor);
        }
        steps[0].SetBorder(CurrentColor);
        steps[0].SetColor(ActiveColor);
    }

    //Method to update the color of the steps
    public void UpdateStepsColor(CardModel cardModel)
    {
        for (int i = 0; i < steps.Count; i++)
        {
            Debug.Log(i == manager.NumberPairsFound);
            if (i == manager.NumberPairsFound)
            {
                steps[i].SetBorder(CurrentColor);
                steps[i].SetColor(ActiveColor);
            }
            else if (i < manager.NumberPairsFound)
            {
                steps[i].SetBorder(ActiveColor);
                steps[i].SetColor(ActiveColor);
                Debug.Log("Step " + i + " is active");
            }
            else
            {
                steps[i].SetBorder(InactiveColor);
                steps[i].SetColor(InactiveColor);
            }
        }
        float percent = manager.NumberPairsFound / (manager.NumberPairs - 1);
        progressBar.fillAmount = percent;
    }
    
}