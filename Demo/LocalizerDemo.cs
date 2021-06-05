using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizerDemo : MonoBehaviour
{

    public Image[] languageOptionsButtons;

    private void Start() {
        UpdateButtons();
    }

    private void UpdateButtons() {
        int selectedLanguageIndex = LocalizationHelper.Instance.GetSelectedLanguageIndex();
        for ( int i = 0; i < languageOptionsButtons.Length; i ++ ) {
            if ( selectedLanguageIndex != i )
                languageOptionsButtons[i].color = Color.white;
            else 
                languageOptionsButtons[i].color = Color.yellow;
        }
    }

    public void ChangeLanguage( int index ) {
        LocalizationHelper.Instance.SelectLanguage(index);
        UpdateButtons();
    }
}
