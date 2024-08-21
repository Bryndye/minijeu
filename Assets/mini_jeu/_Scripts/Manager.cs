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

    public bool IsUsingCards {  get; private set; }

    [SerializeField] private GameObject VictoryArea;

    private void Awake()
    {
        // Init Singleton
        Instance = this;


        OnSameCards += RemovePairCards;
        OnSameCards += DebugSameCards;
        OnDifferentCards += DebugDiffCards;
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
            IsUsingCards = true;
            
            if (CardsReturned[0].Cardid == CardsReturned[1].Cardid)
            {
                // same cards
                OnSameCards?.Invoke();
                foreach (var item in CardsReturned)
                {
                    item.MyEventCard.SetCardDiscovered(Event_card.CardDiscovered.Discovered);
                }
                IsUsingCards = false;
                CardsReturned.Clear();
            }
            else
            {
                // different cards
                OnDifferentCards?.Invoke();
                Invoke(nameof(ResetCardBack), 1.5f);
            }

            // End interaction -> reset init pos OR rm same cards
        }

        VictoryCondition();
    }

    public void ResetCard(CardId _cardId)
    {
        if (CardsReturned.Contains(_cardId))
        {
            CardsReturned.Remove(_cardId);
        }
    }

    private void ResetCardBack()
    {
        foreach (var card in CardsReturned)
        {
            card.MyEventCard.SetCardState(Event_card.CardState.FaceDown);
        }
        CardsReturned.Clear();
        IsUsingCards = false;
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
            VictoryArea.SetActive(true);
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
