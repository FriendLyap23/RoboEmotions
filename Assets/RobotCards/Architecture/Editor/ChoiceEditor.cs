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

        if (GUILayout.Button("������� ����� �����"))
        {
            Card newCard = ScriptableObject.CreateInstance<Card>();

            newCard.SituationText = "";
            newCard.Choices = new Choice[0];

            string path = EditorUtility.SaveFilePanel("��������� ����� �����", "Assets/RobotCards/Cards/ScriptableObject", "NewCard.asset", "asset");

            if (!string.IsNullOrEmpty(path))
            {
                path = FileUtil.GetProjectRelativePath(path);
                AssetDatabase.CreateAsset(newCard, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Choice newChoice = new Choice();
                newChoice.ChoiceText = "����� �����";
                newChoice.NextCard = newCard;

                Array.Resize(ref card.Choices, card.Choices.Length + 1);
                card.Choices[card.Choices.Length - 1] = newChoice;
            }
        }
    }
}
