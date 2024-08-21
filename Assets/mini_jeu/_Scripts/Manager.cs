using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public List<CardId> CardIds = new List<CardId>();

    public List<CardId> CardsReturned = new List<CardId>();

    public Action OnSameCards;
    public Action OnDifferentCards;

    private void Awake()
    {
        // Init Singleton
        Instance = this;


        OnSameCards += RemovePairCards;
        OnSameCards += DebugSameCards;
    }

    #region List Cards
    public void AddCard(CardId _cardiId)
    {
        if (!CardIds.Contains(_cardiId))
        {
            CardIds.Add(_cardiId);
        }
    }

    public void RemoveCard(CardId _cardiId)
    {
        if (CardIds.Contains(_cardiId))
        { 
            CardIds.Remove(_cardiId);
        }
    }
    #endregion

    public void ReturnCard(CardId _cardiId)
    {
        if (CardsReturned.Contains(_cardiId))
            return;

        CardsReturned.Add(_cardiId);

        if (CardsReturned.Count >= 2)
        {
            // verif id card
            if (CardsReturned[0].Cardid == CardsReturned[1].Cardid)
            {
                // same cards
                OnSameCards?.Invoke();
            }
            else
            {
                // different cards
                OnDifferentCards?.Invoke();
            }
        }
    }

    public void RemovePairCards()
    {
        foreach (var _card in CardsReturned)
        {
            RemoveCard(_card);
        }
    }


    public void VictoryCondition()
    {
        if (CardIds.Count <= 0)
        {
            // Victory
        }
    }

    private void DebugSameCards()
    {
        Debug.Log("Same cards");
    }

    private void DebugDiffCards()
    {
        Debug.Log("Different cards");
    }
}
