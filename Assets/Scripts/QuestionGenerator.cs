using UnityEngine;

public class QuestionGenerator
{
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
        num1 = Random.Range(1, 10);
        num2 = Random.Range(1, 10);
        operation = Random.Range(1, 4);

        if (operation == 1)
        {
            answer = (num1 + num2).ToString();
            question = $"Combien fait {num1} + {num2} ?";
        }
        else if (operation == 2)
        {
            answer = (num1 - num2).ToString();
            question = $"Combien fait {num1} - {num2} ?";
        }
        else
        {
            answer = (num1 * num2).ToString();
            question = $"Combien fait {num1} x {num2} ?";
        }

        return (question, answer);
    }


    private static (string question, string answer) GenQ2()
    {
        int num1, num2, num3, operation;
        string answer, question;
        num1 = Random.Range(1, 10);
        num2 = Random.Range(1, 10);
        num3 = Random.Range(1, 10);
        operation = Random.Range(1, 3); // 1: multiplication, 2: division

        if (operation == 1)
        {
            // Exemple : "Combien fait (5 + 3) x 2 ?"
            answer = ((num1 + num2) * num3).ToString();
            question = $"Combien fait ({num1} + {num2}) x {num3} ?";
        }
        else //if (operation == 2)
        {
            // Exemple : "Quel est le r�sultat de 8 � (2 + 2) ?"
            int divResult = num1 + num2; // Le diviseur est une somme
            answer = (num1 / divResult).ToString();
            question = $"Quel est le r�sultat de {num1} � ({num2} + {num3}) ?";
        }

        return (question, answer);
    }


    private static (string question, string answer) GenQ3()
    {
        int num1, num2, num3, operation;
        string question, answer;

        num1 = Random.Range(1, 10);  // Premier terme
        num2 = Random.Range(1, 10);  // Deuxi�me terme
        num3 = Random.Range(1, 10);  // Troisi�me terme
        operation = Random.Range(1, 5); // 1: �quation lin�aire simple, 2: �quation avec multiplication, 3: syst�me d'�quations, 4: �quation quadratique

        if (operation == 1)
        {
            // Exemple : 3x + 5 = 20
            int coeff = Random.Range(1, 5);  // Coefficient pour x
            int constant = Random.Range(1, 10);  // Terme constant
            int resultat = Random.Range(10, 30); // La constante de droite (ex: 20)
            answer = ((resultat - constant) / coeff).ToString();  // R�solution de l'�quation
            question = $"R�soudre {coeff}x + {constant} = {resultat}. Quelle est la valeur de x ?";
        }
        else if (operation == 2)
        {
            // Exemple : 4x - 2 = 3(x + 5)
            int coeff1 = Random.Range(1, 5);  // Coefficient de x � gauche
            int coeff2 = Random.Range(1, 5);  // Coefficient de x � droite
            int constant1 = Random.Range(1, 10);  // Terme constant � gauche
            int constant2 = Random.Range(1, 10);  // Terme constant � droite
                                                  // R�solution de l'�quation : 4x - 2 = 3(x + 5)
            int leftSide = coeff1 * num1 - constant1;
            int rightSide = coeff2 * (num1 + constant2);
            answer = ((constant2 - constant1) / (coeff1 - coeff2)).ToString();  // R�solution de l'�quation
            question = $"R�soudre {coeff1}x - {constant1} = {coeff2}(x + {constant2}). Quelle est la valeur de x ?";
        }
        else if (operation == 3)
        {
            // Exemple : R�soudre un syst�me d'�quations
            int constant1 = Random.Range(1, 10); // Terme constant pour la premi�re �quation
            int constant2 = Random.Range(1, 10); // Terme constant pour la seconde �quation

            // Calcul de x et y de mani�re � ce que les r�ponses soient enti�res
            int x = Random.Range(1, 10); // x entre 1 et 10
            int y = Random.Range(1, 10); // y entre 1 et 10

            // Cr�er un syst�me d'�quations lin�aires
            int equation1Left = x + y;  // Premi�re �quation : x + y = ? (ex: 7)
            int equation2Left = 2 * x - y;  // Deuxi�me �quation : 2x - y = ? (ex: 10)

            // La somme des deux �quations donnera la bonne r�ponse
            answer = equation1Left.ToString();  // On utilise le r�sultat de la premi�re �quation

            question = $"R�soudre le syst�me d'�quations : x + y = {equation1Left}, 2x - y = {equation2Left}. Quelle est la valeur de x + y ?";
        }
        else //if (operation == 4)
        {
            // Exemple : x� + 2x - 3 = 0
            int a = Random.Range(1, 5);  // Coefficient de x�
            int b = Random.Range(1, 5);  // Coefficient de x
            int c = Random.Range(-10, 10);  // Terme constant
                                            // R�soudre l'�quation quadratique : ax� + bx + c = 0
            int discriminant = b * b - 4 * a * c; // Discriminant pour v�rifier si il existe une solution r�elle
            if (discriminant >= 0)
            {
                // Si le discriminant est positif ou nul, nous avons des solutions r�elles
                int sqrtDiscriminant = Mathf.FloorToInt(Mathf.Sqrt(discriminant));
                int x1 = (-b + sqrtDiscriminant) / (2 * a);
                int x2 = (-b - sqrtDiscriminant) / (2 * a);
                question = $"R�soudre l'�quation : {a}x� + {b}x + {c} = 0. Quelle est la valeur de x ?";
                answer = x1.ToString(); // Choisir x1 comme solution
            }
            else
            {
                // Si le discriminant est n�gatif, il n'y a pas de solutions r�elles, vous pouvez g�rer cela diff�remment
                question = $"L'�quation {a}x� + {b}x + {c} = 0 n'a pas de solution r�elle.";
                answer = int.MinValue.ToString();  // Indiquer qu'il n'y a pas de solution r�elle
            }
        }

        return (question, answer);
    }

    private static (string question, string answer) GenQ4()
    {
        int num1, num2, num3, operation;
        float result;
        string question, answer;
        num1 = Random.Range(1, 50);
        num2 = Random.Range(1, 50);
        num3 = Random.Range(1, 50);
        operation = Random.Range(1, 4); // 1: int�grale, 2: d�riv�e, 3: syst�mes d'�quations

        if (operation == 1)
        {
            // Exemple de calcul int�gral (simplifi� pour l'exemple)
            result = num1 * Mathf.Pow(num2, 2) / 2f;
            answer = Mathf.RoundToInt(result).ToString();
            question = $"Int�grale de {num1}x� dx entre 0 et {num2}. Quel est le r�sultat ?";
        }
        else if (operation == 2)
        {
            // Exemple de d�riv�e (simplifi�e)
            result = 2 * num1 * num2;
            answer = Mathf.RoundToInt(result).ToString();
            question = $"Quelle est la d�riv�e de {num1}x� ?";
        }
        else //if (operation == 3)
        {
            // Exemple de syst�me d'�quations : x + y = 10, 2x - y = 4
            answer = (num1 + num2).ToString();
            question = $"R�soudre le syst�me d'�quations : x + y = 10, 2x - y = 4. Quelle est la valeur de x + y ?";
        }

        return (question, answer);
    }
}
