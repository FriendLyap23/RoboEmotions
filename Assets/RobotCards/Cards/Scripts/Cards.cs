using System;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    [SerializeField] private List<Card> _allCards = new List<Card>();
    [SerializeField] private List<Card> _remainderCards;
    [SerializeField] private List<Card> _finishCards = new List<Card>();

    private Card _currentCard;

    private void Awake()
    {
        _remainderCards = new List<Card>(_allCards);
    }

    public Card GetRandomCard()
    {
        if (_remainderCards.Count <= 0 && _finishCards.Count > 0)
        {
            _currentCard = _finishCards[0];
            return _currentCard;
        }

        int currenCardIndex = UnityEngine.Random.Range(0, _remainderCards.Count);
        _currentCard = _remainderCards[currenCardIndex];
        _remainderCards.RemoveAt(currenCardIndex);

        return _currentCard;
    }
}
