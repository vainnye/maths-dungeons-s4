using System;
using System.Text;
using UnityEngine;

public class QuestionGenerator
{
    private static System.Random rand = new System.Random();

    public static (string question, string answer) GenerateQuestion()
    {
        string question, answer;
        int difficulty = GameManager.Instance.Difficulty;

        // Difficult� 1
        if (difficulty == 1)
        {
            (question, answer) = GenQ1();
        }
        // Difficult� 2
        else if (difficulty == 2)
        {
            (question, answer) = GenQ2();
        }
        // Difficult� 3
        else if (difficulty == 3)
        {
            (question, answer) = GenQ3();
        }
        // Difficult� 4
        else //if (difficulty == 4)
        {
            (question, answer) = GenQ4();
        }
        // R�initialiser le champ de r�ponse
        Debug.Log($"Nouvelle question : {question} (R�ponse : {answer})");

        return (question, answer);
    }


    private static (string question, string answer) GenQ1()
    {
        int num1, num2, operation;
        string answer, question;
        // Niveau 1 et 2 : Addition, soustraction, multiplication simples
        num1 = UnityEngine.Random.Range(1, 10);
        num2 = UnityEngine.Random.Range(1, 10);
        operation = UnityEngine.Random.Range(1, 4);

        if (operation == 1)
        {
            answer = (num1 + num2).ToString();
            question = $"Combien fait :\n{num1} + {num2} ?";
        }
        else if (operation == 2)
        {
            answer = (num1 - num2).ToString();
            question = $"Combien fait :\n{num1} - {num2} ?";
        }
        else
        {
            answer = (num1 * num2).ToString();
            question = $"Combien fait :\n{num1} x {num2} ?";
        }

        return (question, answer);
    }


    private static (string question, string answer) GenQ2()
    {
        int num1, num2, num3, operation;
        string answer, question;
        num1 = UnityEngine.Random.Range(1, 10);
        num2 = UnityEngine.Random.Range(1, 10);
        num3 = UnityEngine.Random.Range(1, 10);
        operation = UnityEngine.Random.Range(1, 3); // 1: multiplication, 2: division

        if (operation == 1)
        {
            // Exemple : "Combien fait (5 + 3) x 2 ?"
            answer = ((num1 + num2) * num3).ToString();
            question = $"Combien fait :\n({num1} + {num2}) x {num3} ?";
        }
        else //if (operation == 2)
        {
            // Exemple : "Quel est le r�sultat de 8 � (2 + 2) ?"
            int divResult = num1 + num2; // Le diviseur est une somme
            answer = (num1 / divResult).ToString();
            question = $"Quel est le r�sultat de :\n{num1} � ({num2} + {num3}) ?";
        }

        return (question, answer);
    }


    private static (string question, string answer) GenQ3()
    {
        int[] coeffs = GenererPolynome(3); // Degr� 3 max
        string polynome = AfficherPolynome(coeffs);
        int[] derivee = Deriver(coeffs);
        string polynomeDerive = AfficherPolynome(derivee);

        return ("Quelle est la d�riv�e de :\n" + polynome, polynomeDerive);
    }

    private static (string question, string answer) GenQ4()
    {
        // On choisit les solutions (x, y) d'abord
        int x = rand.Next(-10, 11);
        int y = rand.Next(-10, 11);

        // On choisit les coefficients de la 1re �quation
        int a1 = rand.Next(1, 11);
        int b1 = rand.Next(1, 11);
        if (rand.Next(2) == 0) a1 *= -1;
        if (rand.Next(2) == 0) b1 *= -1;

        int c1 = a1 * x + b1 * y;

        // On choisit les coefficients de la 2e �quation
        int a2 = rand.Next(1, 11);
        int b2 = rand.Next(1, 11);
        if (rand.Next(2) == 0) a2 *= -1;
        if (rand.Next(2) == 0) b2 *= -1;

        int c2 = a2 * x + b2 * y;

        string question = $"{a1}x + {b1}y = {c1}\n{a2}x + {b2}y = {c2} (r�pondre avec \"x,y\")";
        string answer = $"{x},{y}";

        return (question, answer);
    }


    static int[] GenererPolynome(int degreMax)
    {
        int[] coeffs = new int[degreMax + 1];
        for (int i = 0; i <= degreMax; i++)
        {
            coeffs[i] = rand.Next(-5, 6); // Coefficients entre -5 et 5
        }
        coeffs[degreMax] = rand.Next(1, 6); // S'assurer que le terme de plus haut degr� est non nul
        return coeffs;
    }

    static string AfficherPolynome(int[] coeffs)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = coeffs.Length - 1; i >= 0; i--)
        {
            int c = coeffs[i];
            if (c == 0) continue;

            string signe = (sb.Length > 0 && c > 0) ? " + " : (c < 0 ? " - " : "");
            string coef = Math.Abs(c) != 1 || i == 0 ? Math.Abs(c).ToString() : "";
            string variable = i == 0 ? "" : (i == 1 ? "x" : $"x^{i}");

            sb.Append($"{signe}{coef}{variable}");
        }

        return sb.Length > 0 ? sb.ToString().Trim() : "0";
    }


    static int[] Deriver(int[] coeffs)
    {
        int[] derivee = new int[coeffs.Length - 1];
        for (int i = 1; i < coeffs.Length; i++)
        {
            derivee[i - 1] = coeffs[i] * i;
        }
        return derivee;
    }
}
