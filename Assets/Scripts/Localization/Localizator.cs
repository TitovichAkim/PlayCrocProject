using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public static class Localizator
{
    public static void LocalizedText (Text targetTextObject, string key, int num = 0)
    {
        if(PlayerPrefs.HasKey("GameLanguage"))
        {
            string GameLanguage = PlayerPrefs.GetString("GameLanguage");  //  RU/EN
            targetTextObject.text = localized_text(key, GameLanguage, num);
        }
        else
        {  // если язык не выбран то русский по умолчанию
            targetTextObject.text = localized_text(key, "RU", num);
        }
    }
    private static string localized_text (string key, string lang, int num)
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
                    return cuted_row[1].Replace("_", "\n").Split("\n")[num];
                }
                else if(lang == "EN")
                {
                    return cuted_row[2].Replace("_", "\n").Split("\n")[num];
                }
                break;
            }
        }
        return "translation not found";  // если перевод не найден в таблице
    }
}