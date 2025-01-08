using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class ChangeScene : MonoBehaviour
{
    public void SceneSwitcher0() 
    {
        
        PlayerPrefs.SetInt("SelectedCharacterIndex", 0);
        Random random = new Random();
        int firstOppositionIndex =  random.Next(1,3);
        int secondOppositionIndex = firstOppositionIndex == 1 ? 2 : 1;
        PlayerPrefs.SetInt("firstOppositionIndex", firstOppositionIndex);
        PlayerPrefs.SetInt("secondOppositionIndex", secondOppositionIndex);
        Console.WriteLine("Button Clicked");
        SceneManager.LoadScene("LobbyOne");
        PlayerPrefs.SetInt("Level", 1);

    }
    public void SceneSwitcher1() 
    {

        PlayerPrefs.SetInt("SelectedCharacterIndex", 1);
        Random random = new Random();
        int firstOppositionIndex;
        int secondOppositionIndex;
        int oppositiontype = random.Next(0,2);
        if (oppositiontype == 1) {

            firstOppositionIndex = 2;
            secondOppositionIndex = 0;

        } else {
            firstOppositionIndex = 0;
            secondOppositionIndex = 2;

        }
        PlayerPrefs.SetInt("firstOppositionIndex", firstOppositionIndex);
        PlayerPrefs.SetInt("secondOppositionIndex", secondOppositionIndex);
        Console.WriteLine("Button Clicked");
        SceneManager.LoadScene("LobbyOne");
        PlayerPrefs.SetInt("Level", 1);

    }
    public void SceneSwitcher2() 
    {

        PlayerPrefs.SetInt("SelectedCharacterIndex", 2);
        Random random = new Random();
        int firstOppositionIndex = random.Next(0,2);
        int secondOppositionIndex = firstOppositionIndex == 0 ? 1 : 0;
        PlayerPrefs.SetInt("firstOppositionIndex", firstOppositionIndex);
        PlayerPrefs.SetInt("secondOppositionIndex", secondOppositionIndex);
        Console.WriteLine("Button Clicked");
        SceneManager.LoadScene("LobbyOne");
        PlayerPrefs.SetInt("Level", 1);

    }


}