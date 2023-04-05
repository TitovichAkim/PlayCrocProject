using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public static class Localizator
{
    /*
    public string key;
    void Awake ()
    {  // если язык выбран...
        if(PlayerPrefs.HasKey("GameLanguage"))
        {
            string GameLanguage = PlayerPrefs.GetString("GameLanguage");  //  RU/EN
            change_text(localized_text(key, GameLanguage));
        }
        else
        {  // если язык не выбран то английский по умолчанию
            change_text(localized_text(key, "EN"));
        }
    }



    private void change_text (string new_text)
    {
        // вставляем текст в текстовое поле объекта на котором висит скрипт
        GetComponent<Text>().text = new_text;
    }
    */
    public static void LocalizedText (Text targetTextObject, string key)
    {
        if(PlayerPrefs.HasKey("GameLanguage"))
        {
            string GameLanguage = PlayerPrefs.GetString("GameLanguage");  //  RU/EN
            targetTextObject.text = localized_text(key, GameLanguage);
        }
        else
        {  // если язык не выбран то английский по умолчанию
            targetTextObject.text = localized_text(key, "EN");
        }
    }
    private static string localized_text (string key, string lang)
    {
        string[] fileKey = key.Split('.');
       // вытаскиваем из таблицы значение
       // читаем из Resources/Localization/{Название файла, соответствующее началу ключа}.csv
        TextAsset mytxtData=(TextAsset)Resources.Load($"Localization/{fileKey[0]}");
        string loc_txt=mytxtData.text;
        string[] rows = loc_txt.Split('\n');
        for(int i = 1; i < rows.Length; i++)
        {
            string[] cuted_row = Regex.Split(rows[i], "	");
            if(key == cuted_row[0])
            {
                if(lang == "RU")
                {   
                    return cuted_row[1];
                }
                else if(lang == "EN")
                {
                    return cuted_row[2];
                }
                break;
            }
        }
        return "translation not found";  // если перевод не найден в таблице
    }
}