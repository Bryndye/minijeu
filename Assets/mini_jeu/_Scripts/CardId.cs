using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardId : MonoBehaviour
{
    [SerializeField] private int _cardId;
    public int Cardid { get { return _cardId; } }

}