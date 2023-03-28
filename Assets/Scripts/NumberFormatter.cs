using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public static class NumberFormatter
{
    static string[] names = { "", "", "", "����", "����", "������", "������", "�������", "������", "�����", "�����", "������", "���-������"};
    public static void FormatAndRedraw (float inputNumber, Text floatText, Text stringText = null)
    {
        int n = 0;

        if (inputNumber >= 1000000000f)
        {
            while(n + 1 < names.Length && inputNumber >= 1000f)
            {
                inputNumber /= 1000f;
                n++;
            }
        }
        if (n < 3)
        {
            if(stringText != null)
            {
                floatText.text = inputNumber.ToString("N2", CultureInfo.InvariantCulture);
                stringText.text = names[n];
            }
            else
            {
                floatText.text = inputNumber.ToString("N2", CultureInfo.InvariantCulture);
            }
        }   
        else
        {
            if(stringText != null)
            {
                floatText.text = inputNumber.ToString("N3", CultureInfo.InvariantCulture);
                stringText.text = names[n];
            }
            else
            {
                floatText.text = inputNumber.ToString("N3", CultureInfo.InvariantCulture);
            }
        }

    }
}
