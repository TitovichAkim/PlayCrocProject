using UnityEngine;
using UnityEngine.UI;

public static class NumberFormatter
{
    static string[] names = { "", "тыс", "млн", "млрд", "трлн", "квдрлн", "квнтлн", "секстлн", "септлн", "октлн", "нонлн", "децилн", "анд-децилн"};
    public static void FormatAndRedraw (float inputNumber, Text floatText, Text stringText = null)
    {
        int n = 0;
        while(n + 1 < names.Length && inputNumber >= 1000f)
        {
            inputNumber /= 1000f;
            n++;
        }
        if (stringText != null)
        {
            floatText.text = inputNumber.ToString("F3");
            stringText.text = names[n];
        } 
        else
        {
            floatText.text = $"{inputNumber.ToString("F3")} {names[n]}";
        }

    }
}
