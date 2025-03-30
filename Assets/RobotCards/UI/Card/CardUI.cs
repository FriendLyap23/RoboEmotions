using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text SituationText;

    [SerializeField] private TMP_Text[] ChoicesText;

    [SerializeField] private Image ImageCard;

    public void DisplayCard(Card card) 
    {
        SituationText.text = card.SituationText;
        ImageCard.sprite = card.ImageCard;

        for (int i = 0; i < ChoicesText.Length; i++)
            ChoicesText[i].text = card.Choices[i].ChoiceText;
    }
}
