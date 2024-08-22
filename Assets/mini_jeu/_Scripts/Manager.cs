using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    [FormerlySerializedAs("CardIds")] public List<CardModel> ListCards = new List<CardModel>();

    public List<CardModel> CardsReturned = new List<CardModel>();

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
    public void AddCard(CardModel cardiModel)
    {
        if (!ListCards.Contains(cardiModel))
        {
            ListCards.Add(cardiModel);
        }
    }

    public void RemoveCard(CardModel cardiModel)
    {
        if (ListCards.Contains(cardiModel))
        { 
            ListCards.Remove(cardiModel);
        }
    }
    #endregion

    public void ReturnCard(CardModel cardiModel)
    {
        if (CardsReturned.Contains(cardiModel))
            return;

        CardsReturned.Add(cardiModel);

        if (CardsReturned.Count >= 2)
        {
            IsUsingCards = true;
            
            if (CardsReturned[0].Type == CardsReturned[1].Type)
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

    public void ResetCard(CardModel cardModel)
    {
        if (CardsReturned.Contains(cardModel))
        {
            CardsReturned.Remove(cardModel);
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
        if (ListCards.Count <= 0)
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
