LOCALIZATION 1.0 (by TOJGAMES)

This is a very simple (but maybe hard and complex way) script to help you with your game localization.

Steps required to localize game.


SETUP
_______________

1. Add LocalizationHelper prefab located in Localization/Prefabs/LocalizationHelper.prefab to your scene;

2. Click Localization/Open Window button in Unity's menu bar, this open localization helper Window

3. Click "SETUP TEXTS ON THE SCENE" button. 
   This will add TextLocalizer component to all texts in the scene. 
   By default text localizer is set to non localizable, uncheck it and select proper phrase for this text.

4. Click LocalizationData asset located in Localization/Resources.
   Using this asset you can add your own phrases and localization languages




SCRIPTING
_______________

(!! Finish all the setup steps before)

   -  To get languages Call LocalizationHelper.Instance.GetLanguages() function. 
      It will return the list of available languages in Language class type.
      A Language class contains of Language Title, Language Title Localized and Language ID;

   -  To change the languages in runtime: 
      Call the LocalizationHelper.Instance.SelectLanguage( (int) languageIndex ).
      It will notify all texts in the scene about update.
