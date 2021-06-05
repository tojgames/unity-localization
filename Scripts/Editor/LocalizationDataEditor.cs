using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


[CustomEditor(typeof(LocalizationData))]
public class LocalizationDataEditor : Editor
{

    private int activeTabIndex = 0;
    private LocalizationData localizationData;

    private ReorderableList phrasesList;

    public override void OnInspectorGUI() {

        if ( !localizationData ) {
            localizationData = (LocalizationData) target;
        }
        
        GUILayout.Label("LOCALIZATION DATA");
        GUILayout.Space(10);

        string[] tabs = { "Phrases", "Languages" };

        GUILayout.BeginHorizontal();
        activeTabIndex = GUILayout.Toolbar( activeTabIndex, tabs);
        GUILayout.EndHorizontal();


        //
        if ( activeTabIndex == 0 ) 
            DrawPhrasesInspector();

        else if ( activeTabIndex == 1 ) 
            DrawLanguagesInspector();

    }

    private void DrawLanguagesInspector() {
        serializedObject.Update();
		
        GUILayout.BeginVertical();

        GUILayout.Space(10);

        var property = serializedObject.FindProperty("languages");
        EditorGUILayout.PropertyField( property, true);

        GUILayout.EndVertical();
        
		serializedObject.ApplyModifiedProperties();
    }

    private void DrawPhrasesInspector() {
        serializedObject.Update();
        GUILayout.BeginVertical();

        UpdatePhrases();

        var property = serializedObject.FindProperty("phrases");
        EditorGUILayout.PropertyField( property, true);

        GUILayout.EndVertical();
		serializedObject.ApplyModifiedProperties();
    }

    private void UpdatePhrases() {

        for ( int i = 0; i < localizationData.phrases.Count; i ++ ) {

            int phraseCount = localizationData.phrases[i].localizations.Count;
            int languagesCount = localizationData.languages.Count;

            if ( phraseCount < languagesCount ) {
                for ( int j = 0; j < languagesCount - phraseCount; j ++ ) {
                    
                    LocalizationData.Phrase.LocalizedPhrase localizedPhrase = new LocalizationData.Phrase.LocalizedPhrase();
                    localizedPhrase.phrase = "";

                    localizationData.phrases[i].localizations.Add(localizedPhrase);
                }
            }

            if ( phraseCount > 0 )
                localizationData.phrases[i].firstElementTitle = localizationData.phrases[i].localizations[0].phrase;
                
                for ( int j = 0; j < phraseCount; j ++ ) {
                {
                    if ( languagesCount >= phraseCount ) {
                        localizationData.phrases[i].localizations[j].firstElementTitle = localizationData.languages[j].languageTitle;
                    }
                }    
                
            }
        }
    }

}
