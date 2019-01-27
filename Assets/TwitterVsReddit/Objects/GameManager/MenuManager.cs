using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(UnityEngine.Random.Range(1, SceneManager.sceneCount));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
