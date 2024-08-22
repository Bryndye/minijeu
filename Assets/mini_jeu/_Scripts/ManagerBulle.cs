using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerBulle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private Manager manager;
    [SerializeField] private GameObject _bubble;
    
    //Method to update the text of the bubble
    public void UpdateText(CardModel cardModel)
    {
        _bubble.SetActive(false);
        _bubble.SetActive(true);
        _textMeshPro.text = cardModel.textBulle.text;
    }

    public void Start()
    {
        _bubble.SetActive(false);
        manager = Manager.Instance;
        manager.OnSameCards += UpdateText;        
    }
}
