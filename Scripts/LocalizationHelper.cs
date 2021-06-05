using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationHelper : MonoBehaviour
{

    private const string USER_LANGUAGE = "LOCALIZATION_MANAGER_USER_SELECTED_LANGUAGE_ID";

    public LocalizationData localizationData;

    public Action<int> languageSelected;

    public int selectedLanguageIndex = 0;


    private static LocalizationHelper instance;
    public static LocalizationHelper Instance {
        get { return instance; }
    }

    private void Awake() {
        
        if ( instance != null )
            Destroy(gameObject);
        instance = this;

        if ( localizationData == null ) {
            localizationData = (LocalizationData) Resources.Load("LocalizationData");
        }

        languageSelected = null;
        
        selectedLanguageIndex = PlayerPrefs.GetInt(USER_LANGUAGE, 0);
        if ( languageSelected != null )
            languageSelected(selectedLanguageIndex);

    }

    public int GetSelectedLanguageIndex() {
        return PlayerPrefs.GetInt(USER_LANGUAGE, 0);
    }

    public List<LocalizationData.Language> GetLanguages() {
        return localizationData.languages;
    }

    public void SelectLanguage(int index)
    {
        selectedLanguageIndex = index;
        PlayerPrefs.SetInt(USER_LANGUAGE, selectedLanguageIndex);

        if ( languageSelected != null )
            languageSelected(selectedLanguageIndex);
    }


}
