using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentScene;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a friendly Game Object");
                break;
            case "Finish":
                LoadNextScene();
                break;
            case "Fuel":
                Debug.Log("You gained extra points");
                break;
            default:
                ReloadScene(currentScene);
                break;
        }
    }
    private void ReloadScene(int sceneindex)
    {
        SceneManager.LoadScene(sceneindex);
    }
    private void LoadNextScene()
    {
        currentScene++;
        if (currentScene == 3)
        {
            currentScene = 0;
        }
        ReloadScene(currentScene);
    }
}
