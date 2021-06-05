using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationHelper))]
public class LocalizationHelperEditorInScene : Editor
{

    int customLanguageIndex = 0;
    LocalizationHelper localizationHelper;

    public override void OnInspectorGUI() {
        DrawDefaultInspector ();

        if ( localizationHelper == null )
            localizationHelper = (LocalizationHelper) target;

        GUILayout.Space(20);

        customLanguageIndex = EditorGUILayout.IntField( "Language index", customLanguageIndex );
        if ( GUILayout.Button("Update Language") )
        {
            LocalizationHelperEditor.phrasesLanguageIndex = customLanguageIndex;
            localizationHelper.SelectLanguage(customLanguageIndex);
        }


    }
}
