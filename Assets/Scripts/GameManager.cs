using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> sceneNames = new List<string>();
    [SerializeField] private int sceneIndex = 0;

    #region Singleton
    static private GameManager instance;
    static public GameManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    public void LoadScene(string sceneName) 
    { 
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single); 
        sceneIndex = sceneNames.IndexOf(sceneName);
    }

    public void LoadNextScene() 
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneNames[sceneIndex], LoadSceneMode.Single);
    }

    public void ResetScene() 
    { 
        SceneManager.LoadScene(sceneNames[sceneIndex], LoadSceneMode.Single); 
    }

}
