using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{

    public void LevelLoad(string name)
    {
        Debug.Log("Level load requested for: " + name);
       
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        print(name);
    }

    public void LoadnextLevel()
    {
        Scene si = SceneManager.GetActiveScene();
       

        SceneManager.LoadScene(si.buildIndex + 1);

    }

 

    public void Done()
    {
        Application.Quit();
    }
}
