using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerBulle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private Manager manager;
    
    //Method to update the text of the bubble
    public void UpdateText(CardModel cardModel)
    {
        _textMeshPro.text = cardModel.textBulle.text;
    }

    public void Start()
    {
        manager = Manager.Instance;
        manager.OnSameCards += UpdateText;        
    }
}
