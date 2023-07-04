using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OptionsSetup : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> optionButtons = new();
    private List<GameObject> shuffledOptionButtons = new();

    private List<int> usedNumbers = new();
    private void OnEnable()
    {
        usedNumbers.Add(GameManager.Instance.instantiatedNumber.GetComponent<NumberInfo>().numberInt);
        int randomNumberCorrect = Random.Range(0, optionButtons.Count - 1);
        
        shuffledOptionButtons = optionButtons.OrderBy( x => Random.value ).ToList( );
        
        for (int i = 0; i < shuffledOptionButtons.Count - 1; i++)
        {
            if (i == randomNumberCorrect)
            {
                shuffledOptionButtons[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.instantiatedNumber.GetComponent<NumberInfo>().numberString;
                shuffledOptionButtons[i].gameObject.GetComponent<Button>().onClick.AddListener(WinnerButton);
            }
            else
            {
                int randomNumberWrong = RandomIntWithException.randomIntExceptMultiples(1, 10, usedNumbers);
                shuffledOptionButtons[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = NumberToText(randomNumberWrong);
                shuffledOptionButtons[i].gameObject.GetComponent<Button>().onClick.AddListener(LoserButton);
                usedNumbers.Add(randomNumberWrong);
            }
        }
    }

    private void OnDisable()
    {
        usedNumbers.Clear();
    }

    public void WinnerButton()
    {
        Debug.Log("WinnerButton");
        // GOOD JOB
        GameManager.State.SetState(GameState.EGameState.Postgame);
    }
    
    public void LoserButton()
    {
        // TRY AGAIN :/
        Debug.Log("LoserButton");
    }

    private string NumberToText(int number)
    {
        switch (number)
        {
           case 1:
               return "One";
           case 2:
               return "Two";
           case 3:
               return "Three";
           case 4:
               return "Four";
           case 5:
               return "Five";
           case 6:
               return "Six";
           case 7:
               return "Seven";
           case 8:
               return "Eight";
           case 9:
               return "Nine";
           case 10:
               return "Ten";
        }
        return "Error";
    }
}
