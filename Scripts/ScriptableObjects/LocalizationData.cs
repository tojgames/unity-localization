using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationDataAsset", menuName = "Project Localization/LocalizationData", order = 0)]
public class LocalizationData : ScriptableObject
{
    public List<Language> languages = new List<Language>();
    public List<Phrase> phrases = new List<Phrase>();


    [System.Serializable]
    public class Phrase {

        [HideInInspector]
        public string firstElementTitle;
        public List<LocalizedPhrase> localizations = new List<LocalizedPhrase>();

        [System.Serializable]
        public class LocalizedPhrase {
            
            [HideInInspector]
            public string firstElementTitle;

            public string phrase;
        }
    }


    [System.Serializable]
    public class Language {
        public string languageTitle;
        public string languageTitleLocalized;
        public int languageID;
    }

    public int GetLanguageIndexByID( int langID ) {
        for ( int i = 0; i < languages.Count; i ++ ) {
            if ( langID == languages[i].languageID ) {
                return i;
            }
        }
        return 0;
    }

}
