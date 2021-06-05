using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;

public class LocalizationHelperEditor : EditorWindow
{

    //Helps with localization

    [MenuItem("Localization/Open Window",false, 1)]
    public static void OpenWindow()
    {
        try {
            GetWindow<LocalizationHelperEditor>("Localization");
        }
        catch {}
    }

    public static int phrasesLanguageIndex = 0;
    public static LocalizationData localizationData;
    public LocalizationHelper localizationHelper;

    public int totalTextsFound = 0;

    private void OnGUI() {

        if ( localizationData == null ) {
            localizationData = (LocalizationData) Resources.Load("LocalizationData");
        }
        
        if ( localizationHelper == null ) {
            localizationHelper = GetLocalizationHelperEditorInScene();
            if ( localizationHelper == null )
                EditorGUILayout.HelpBox("Could not find any LocalizationHelper in the scene.", MessageType.Error);

        }
        

        localizationHelper = EditorGUILayout.ObjectField("In scene script",localizationHelper,typeof(LocalizationHelper), true) as LocalizationHelper;
        localizationData = EditorGUILayout.ObjectField("Data asset",localizationData,typeof(LocalizationData), true) as LocalizationData;

        GUILayout.Space(20);

        if ( GUILayout.Button("SETUP TEXTS ON THE SCENE") ) {
            SetupTextsInScene();
        }
        
        if ( totalTextsFound > 0 ) {
            EditorGUILayout.HelpBox("Totally " + totalTextsFound + " texts found in scene.", MessageType.Info);
        }
        else {
            EditorGUILayout.HelpBox("Could not find any texts in the scene. Click button above to search", MessageType.Error);
        }

        GUILayout.Space(20);
        
        ShowPhrases();

    }


    private void SetupTextsInScene()
    {

        List<Text> texts = GetAllTextsInScene();
        totalTextsFound = texts.Count;

        for ( int i = 0; i < texts.Count; i ++ )
        {
            if ( texts[i].GetComponent<TextLocalizer>() == null ) {
                texts[i].gameObject.AddComponent<TextLocalizer>();
            }
        }
    }

    private void ShowPhrases()
    {
        string[] selectedPhrases = GetPhrases();
        for ( int i = 0; i < selectedPhrases.Length; i ++ )
            EditorGUILayout.TextField(selectedPhrases[i]);
    }

    public static string[] GetPhrases()
    {
        if ( localizationData != null ) {
            string[] phrases = new string[localizationData.phrases.Count];
            for ( int i = 0; i < phrases.Length; i ++ ) {
                phrases[i] = localizationData.phrases[i].localizations[phrasesLanguageIndex].phrase;
            }
            return phrases;
        }
        return null;
    }

    List<Text> GetAllTextsInScene()
    {
        List<Text> objectsInScene = new List<Text>();

        foreach (Text go in Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[])
        {
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                objectsInScene.Add(go);
        }

        return objectsInScene;
    }

    LocalizationHelper GetLocalizationHelperEditorInScene() {
        foreach (LocalizationHelper go in Resources.FindObjectsOfTypeAll(typeof(LocalizationHelper)) as LocalizationHelper[])
        {
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                return go;
        }
        return null;
    }

    private static LocalizationHelperEditor instance;
    public static LocalizationHelperEditor Instance {
        get { return instance; }
    }

    private void Awake() {
        if ( instance != null ) {
            Destroy(this);
        }
        instance = this;
    }


}
