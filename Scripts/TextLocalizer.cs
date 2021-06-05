using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextLocalizer : MonoBehaviour
{

    public bool nonLocalizable = true;

    public enum Capitalization { AllCapitalLetters, OnlyFirstLetterCapital, AllNonCapitalLetters };
    public Capitalization capitalization = Capitalization.AllCapitalLetters;
    public int phraseIndex = 0;

    [HideInInspector]
    public Text text;

    private void Awake() {
        if ( !nonLocalizable )
        {
            Init();
            LocalizationHelper.Instance.languageSelected += SetupPhrases;
            SetupPhrases( LocalizationHelper.Instance.selectedLanguageIndex );
        }
    }

    void SetupPhrases(int languageIndex)
    {   
        string phrase = LocalizationHelper.Instance.localizationData.phrases[phraseIndex].localizations[languageIndex].phrase;
        
        if ( capitalization == Capitalization.AllCapitalLetters )
            phrase = phrase.ToUpper();
        else if ( capitalization == Capitalization.AllNonCapitalLetters )
            phrase = phrase.ToLower();
        else if ( capitalization == Capitalization.OnlyFirstLetterCapital )
            phrase = new StringBuilder( phrase[0].ToString().ToUpper() ).Append(phrase.Substring(1)).ToString();
        
        text.text = phrase;
    }

    public void Init()
    {
        if ( text == null )
            text = GetComponent<Text>();
    }

}
