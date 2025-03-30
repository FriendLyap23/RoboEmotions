using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShowCards : MonoBehaviour
{
    private Cards _cards;
    private CardUI _cardUI;
    private Emotion _emotion;

    private Card _currentCard;
    private Queue<Card> _unlockedCardChains = new Queue<Card>();

    [SerializeField] private FinalPanel _finalPanel;
    [SerializeField] private EmotionIndicators _emotionIndicators;

    [Inject]
    private void Constructor(Cards cards, CardUI cardUI, Emotion emotion)
    {
        _cards = cards;
        _cardUI = cardUI;
        _emotion = emotion;
    }

    private void Start() => StartNewRound();

    public void StartNewRound()
    {
        if (_currentCard != null)
            StartCoroutine(WaitForCardAnimation());
        else
            DisplayNextCard();
    }

    public Card GetCurrentCard()
    {
        return _currentCard;
    }

    private IEnumerator WaitForCardAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        DisplayNextCard();
    }

    private void DisplayNextCard()
    {
        _currentCard ??= GetNextCardInSequence();
        _cardUI.DisplayCard(_currentCard);
    }

    public void OnChoiceSelected(bool isRightSwipe)
    {
        var choice = GetValidatedChoice(isRightSwipe);
        if (choice == null) return;

        ApplyChoiceEffects(choice);
        HandleUnlockedCards(choice);
        MoveToNextCard(choice);

        StartNewRound();
    }

    private Choice GetValidatedChoice(bool isRightSwipe)
    {
        int choiceIndex = isRightSwipe ? 1 : 0;

        if (_currentCard?.Choices == null || choiceIndex >= _currentCard.Choices.Length)
        {
            _currentCard = _cards.GetRandomCard();
            return null;
        }

        return _currentCard.Choices[choiceIndex];
    }

    private void ApplyChoiceEffects(Choice choice) => _emotion.ApplyEffect(choice.emotions);

    private void HandleUnlockedCards(Choice choice)
    {
        if (_currentCard.NewCardsToUnlock?.Count > 0)
        {
            foreach (var card in _currentCard.NewCardsToUnlock)
                _unlockedCardChains.Enqueue(card);
        }
    }

    private void MoveToNextCard(Choice choice)
    {
        if (choice.NextCard != null)
        {
            _currentCard = choice.NextCard;

            if (_currentCard.IsFinal)
            {
                _finalPanel.ShowFinalText(_currentCard.IndexFinal);
                return;
            }
        }
        else
            _currentCard = GetNextCardInSequence();
    }

    private Card GetNextCardInSequence()
    {
        return _unlockedCardChains.Count > 0
            ? _unlockedCardChains.Dequeue()
            : _cards.GetRandomCard();
    }
}