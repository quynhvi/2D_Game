using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Playables;

public class Numpad : MonoBehaviour
{
    [SerializeField] private GameObject NumpadScreen;
    [SerializeField] private PlayableDirector playableDirector;

    string code = "1234";
    string number = null;
    int numberIndex = 0;
    string alpha;
    public TMP_Text UIText = null;

    public void CodeFunction(string numbers)
    {
        numberIndex++;
        number = number + numbers;
        UIText.text = number;
    }

    public void Enter()
    {
        if (number == code)
        {
            Time.timeScale = 1f;
            UIText.text = "Correct";
            NumpadScreen.SetActive(false);

            playableDirector.Play();
            
            SceneManager.LoadScene(0);
        }
        else
        {
            UIText.text = "Wrong Passcode";
        }
    }

    public void Delete()
    {
        numberIndex++;
        number = null;
        UIText.text = number;
    }
}
