using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Card))]
public class ChoiceEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();

        Card card = (Card)target;

        if (GUILayout.Button("Создать новую карту"))
        {
            Card newCard = ScriptableObject.CreateInstance<Card>();

            newCard.SituationText = "";
            newCard.Choices = new Choice[0];

            string path = EditorUtility.SaveFilePanel("Сохранить новую карту", "Assets/RobotCards/Cards/ScriptableObject", "NewCard.asset", "asset");

            if (!string.IsNullOrEmpty(path))
            {
                path = FileUtil.GetProjectRelativePath(path);
                AssetDatabase.CreateAsset(newCard, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Choice newChoice = new Choice();
                newChoice.ChoiceText = "Новый выбор";
                newChoice.NextCard = newCard;

                Array.Resize(ref card.Choices, card.Choices.Length + 1);
                card.Choices[card.Choices.Length - 1] = newChoice;
            }
        }
    }
}
