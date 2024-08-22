using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    [FormerlySerializedAs("CardIds")] public List<CardModel> ListCards = new List<CardModel>();
    public float NumberPairs;
    public float NumberPairsFound;
    public float PercentPairs;

    public List<CardModel> CardsReturned = new List<CardModel>();

    public Action<CardModel> OnSameCards;
    public Action OnDifferentCards;

    public bool IsUsingCards {  get; private set; }

    [SerializeField] private GameObject VictoryArea;

    private void Awake()
    {
        // Init Singleton
        Instance = this;

        NumberPairs = ListCards.Count / 2;

        VictoryArea.SetActive(false);
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
                foreach (var _card in CardsReturned)
                {
                    RemoveCard(_card);
                }

                NumberPairsFound = NumberPairs - ListCards.Count / 2;
                PercentPairs = NumberPairsFound / NumberPairs;

                OnSameCards?.Invoke(CardsReturned[0]);

                foreach (var item in CardsReturned)
                {
                    item.MyEventCard.SetCardDiscovered(EventCard.CardDiscovered.Discovered);
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
            card.MyEventCard.SetCardState(EventCard.CardState.FaceDown);
        }
        CardsReturned.Clear();
        IsUsingCards = false;
    }

    public void RemovePairCards(CardModel cardModel)
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
}
