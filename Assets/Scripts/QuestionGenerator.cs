using UnityEngine;

public class QuestionGenerator
{
    public static (string question, string answer) GenerateQuestion()
    {
        string question, answer;
        int difficulty = GameManager.Instance.Difficulty;

        // Difficulté 1
        if (difficulty == 1)
        {
            (question, answer) = GenQ1();
        }
        // Difficulté 2
        else if (difficulty == 2)
        {
            (question, answer) = GenQ2();
        }
        // Difficulté 3
        else if (difficulty == 3)
        {
            (question, answer) = GenQ3();
        }
        // Difficulté 4
        else //if (difficulty == 4)
        {
            (question, answer) = GenQ4();
        }
        // Réinitialiser le champ de réponse
        Debug.Log($"Nouvelle question : {question} (Réponse : {answer})");

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
            // Exemple : "Quel est le résultat de 8 ÷ (2 + 2) ?"
            int divResult = num1 + num2; // Le diviseur est une somme
            answer = (num1 / divResult).ToString();
            question = $"Quel est le résultat de {num1} ÷ ({num2} + {num3}) ?";
        }

        return (question, answer);
    }


    private static (string question, string answer) GenQ3()
    {
        int num1, num2, num3, operation;
        string question, answer;

        num1 = Random.Range(1, 10);  // Premier terme
        num2 = Random.Range(1, 10);  // Deuxième terme
        num3 = Random.Range(1, 10);  // Troisième terme
        operation = Random.Range(1, 5); // 1: équation linéaire simple, 2: équation avec multiplication, 3: système d'équations, 4: équation quadratique

        if (operation == 1)
        {
            // Exemple : 3x + 5 = 20
            int coeff = Random.Range(1, 5);  // Coefficient pour x
            int constant = Random.Range(1, 10);  // Terme constant
            int resultat = Random.Range(10, 30); // La constante de droite (ex: 20)
            answer = ((resultat - constant) / coeff).ToString();  // Résolution de l'équation
            question = $"Résoudre {coeff}x + {constant} = {resultat}. Quelle est la valeur de x ?";
        }
        else if (operation == 2)
        {
            // Exemple : 4x - 2 = 3(x + 5)
            int coeff1 = Random.Range(1, 5);  // Coefficient de x à gauche
            int coeff2 = Random.Range(1, 5);  // Coefficient de x à droite
            int constant1 = Random.Range(1, 10);  // Terme constant à gauche
            int constant2 = Random.Range(1, 10);  // Terme constant à droite
                                                  // Résolution de l'équation : 4x - 2 = 3(x + 5)
            int leftSide = coeff1 * num1 - constant1;
            int rightSide = coeff2 * (num1 + constant2);
            answer = ((constant2 - constant1) / (coeff1 - coeff2)).ToString();  // Résolution de l'équation
            question = $"Résoudre {coeff1}x - {constant1} = {coeff2}(x + {constant2}). Quelle est la valeur de x ?";
        }
        else if (operation == 3)
        {
            // Exemple : Résoudre un système d'équations
            int constant1 = Random.Range(1, 10); // Terme constant pour la première équation
            int constant2 = Random.Range(1, 10); // Terme constant pour la seconde équation

            // Calcul de x et y de manière à ce que les réponses soient entières
            int x = Random.Range(1, 10); // x entre 1 et 10
            int y = Random.Range(1, 10); // y entre 1 et 10

            // Créer un système d'équations linéaires
            int equation1Left = x + y;  // Première équation : x + y = ? (ex: 7)
            int equation2Left = 2 * x - y;  // Deuxième équation : 2x - y = ? (ex: 10)

            // La somme des deux équations donnera la bonne réponse
            answer = equation1Left.ToString();  // On utilise le résultat de la première équation

            question = $"Résoudre le système d'équations : x + y = {equation1Left}, 2x - y = {equation2Left}. Quelle est la valeur de x + y ?";
        }
        else //if (operation == 4)
        {
            // Exemple : x² + 2x - 3 = 0
            int a = Random.Range(1, 5);  // Coefficient de x²
            int b = Random.Range(1, 5);  // Coefficient de x
            int c = Random.Range(-10, 10);  // Terme constant
                                            // Résoudre l'équation quadratique : ax² + bx + c = 0
            int discriminant = b * b - 4 * a * c; // Discriminant pour vérifier si il existe une solution réelle
            if (discriminant >= 0)
            {
                // Si le discriminant est positif ou nul, nous avons des solutions réelles
                int sqrtDiscriminant = Mathf.FloorToInt(Mathf.Sqrt(discriminant));
                int x1 = (-b + sqrtDiscriminant) / (2 * a);
                int x2 = (-b - sqrtDiscriminant) / (2 * a);
                question = $"Résoudre l'équation : {a}x² + {b}x + {c} = 0. Quelle est la valeur de x ?";
                answer = x1.ToString(); // Choisir x1 comme solution
            }
            else
            {
                // Si le discriminant est négatif, il n'y a pas de solutions réelles, vous pouvez gérer cela différemment
                question = $"L'équation {a}x² + {b}x + {c} = 0 n'a pas de solution réelle.";
                answer = int.MinValue.ToString();  // Indiquer qu'il n'y a pas de solution réelle
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
        operation = Random.Range(1, 4); // 1: intégrale, 2: dérivée, 3: systèmes d'équations

        if (operation == 1)
        {
            // Exemple de calcul intégral (simplifié pour l'exemple)
            result = num1 * Mathf.Pow(num2, 2) / 2f;
            answer = Mathf.RoundToInt(result).ToString();
            question = $"Intégrale de {num1}x² dx entre 0 et {num2}. Quel est le résultat ?";
        }
        else if (operation == 2)
        {
            // Exemple de dérivée (simplifiée)
            result = 2 * num1 * num2;
            answer = Mathf.RoundToInt(result).ToString();
            question = $"Quelle est la dérivée de {num1}x² ?";
        }
        else //if (operation == 3)
        {
            // Exemple de système d'équations : x + y = 10, 2x - y = 4
            answer = (num1 + num2).ToString();
            question = $"Résoudre le système d'équations : x + y = 10, 2x - y = 4. Quelle est la valeur de x + y ?";
        }

        return (question, answer);
    }
}
