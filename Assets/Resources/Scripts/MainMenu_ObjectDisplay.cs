using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu_ObjectDisplay : MonoBehaviour
{
    public float RPM = 60;
    public GameObject displayObject;
    public GameObject MainMenuContainer;
    public GameObject ScoreBoardContainer;
    // Start is called before the first frame update
    void Start()
    {
        displayObject = GameObject.Find("DisplayObject");
        MainMenuContainer = GameObject.Find("MenuContainer");
        ScoreBoardContainer = GameObject.Find("ScoreScreen");

        ScoreBoardContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        displayObject.transform.Rotate(RPM * Time.deltaTime, RPM * Time.deltaTime, RPM * Time.deltaTime);
    }

    public void quitGame()
    {
        Debug.Log("Game shutting down. Good bye");
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void toggleScoreScreen()
    {
        List<Button> buttonsToChange = new List<Button>();

        buttonsToChange.AddRange(MainMenuContainer.transform.GetComponentsInChildren<Button>());


        if (ScoreBoardContainer.activeSelf)
        {
            foreach (var item in buttonsToChange)
            {
                item.enabled = true;
            }

            ScoreBoardContainer.SetActive(false);
        }
        else
        {
            foreach (var item in buttonsToChange)
            {
                item.enabled = false;
            }

            ScoreBoardContainer.SetActive(true);
        }


    }
}
