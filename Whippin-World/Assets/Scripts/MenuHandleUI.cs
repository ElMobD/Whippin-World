using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandleUI : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managerObject = GameObject.Find("GameManager");
        if (managerObject != null)
        {
            gameManager = managerObject.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*if (!gameManager.IsGameOver)
        {
        }*/
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
