using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard" ,  menuName = "Cards/Card")]
public class Card : ScriptableObject
{
    [TextArea(1, 10)]
    public string SituationText;
    public Sprite ImageCard;

    public List<Card> NewCardsToUnlock = new List<Card>();
    public Choice[] Choices;

    public bool IsFinal;
    public int IndexFinal;
}

[Serializable]
public class Choice
{
    public string ChoiceText;
    public Emotions emotions;
    public Card NextCard;
}

[Serializable]
public class Emotions 
{
    public int FearChange;
    public int JoyChange;
    public int SadnessChange;
    public int AngerChange;
}
