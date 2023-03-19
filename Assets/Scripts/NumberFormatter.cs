using UnityEngine;
using UnityEngine.UI;

public static class NumberFormatter
{
    static string[] names = { "", "", "���", "����", "����", "������", "������", "�������", "������", "�����", "�����", "������", "���-������"};
    public static void FormatAndRedraw (float inputNumber, Text floatText, Text stringText = null)
    {
        int n = 0;
        if (inputNumber >= 1000000f)
        {
            while(n + 1 < names.Length && inputNumber >= 1000f)
            {
                inputNumber /= 1000f;
                n++;
            }
        }

        if(stringText != null)
        {
            floatText.text = inputNumber.ToString("0.###");
            stringText.text = names[n];
        }
        else
        {
            floatText.text = $"{inputNumber.ToString("0.###")} {names[n]}";
        }
    }
}
