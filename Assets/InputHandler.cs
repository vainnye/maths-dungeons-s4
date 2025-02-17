using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;


    public void ValidateInput()
    {
        string input = inputField.text;

        if(input.Length >= 4)
        {
            resultText.text = "Valeur correcte";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "Valeur incorrecte";
            resultText.color = Color.red;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
