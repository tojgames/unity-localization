using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(TextLocalizer))]
public class TextLocalizerEditor : Editor
{

    TextLocalizer localizer;
    string[] options;

    public override void OnInspectorGUI() {
        
        localizer = (TextLocalizer) target;
        Init();
        
        localizer.nonLocalizable = EditorGUILayout.Toggle("Non localizable text", localizer.nonLocalizable);

        if ( !localizer.nonLocalizable )
        {
            EditorGUILayout.HelpBox("Selected phrase index: " + localizer.phraseIndex, MessageType.Info);
    
            localizer.capitalization = (TextLocalizer.Capitalization) EditorGUILayout.EnumPopup( "Capitalization type", localizer.capitalization);

            options = LocalizationHelperEditor.GetPhrases();

            if ( options == null )
                LocalizationHelperEditor.OpenWindow();
            
            localizer.phraseIndex = EditorGUILayout.Popup("Select phrase", localizer.phraseIndex, options );
            
            string phrase = options[localizer.phraseIndex];

            if ( localizer.capitalization == TextLocalizer.Capitalization.AllCapitalLetters )
                phrase = phrase.ToUpper();
            else if ( localizer.capitalization == TextLocalizer.Capitalization.AllNonCapitalLetters )
                phrase = phrase.ToLower();
            else if ( localizer.capitalization == TextLocalizer.Capitalization.OnlyFirstLetterCapital )
                phrase = new StringBuilder( phrase[0].ToString().ToUpper() ).Append(phrase.Substring(1)).ToString();

            localizer.text.text = phrase;
            EditorUtility.SetDirty(localizer.text);
            
        }

    }

    private void Init()
    {
        if ( localizer.text == null ) {
            localizer.text = localizer.gameObject.GetComponent<Text>();
        }
    }

}
