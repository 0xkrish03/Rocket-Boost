using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentScene;
    [SerializeField] float delay = 2f;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        if (GetComponent<Movement>().enabled)
        {

            switch (tag)
            {
                case "Friendly":
                    Debug.Log("Bumped into a friendly Game Object");
                    break;
                case "Finish":
                    SceneDelayManager();
                    break;
                case "Fuel":
                    Debug.Log("You gained extra points");
                    break;
                default:
                    StartCrashSquence();
                    break;
            }
        }
    }
    void SceneDelayManager()
    {
        Invoke("LoadNextScene", delay);
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }
    void LoadNextScene()
    {
        currentScene++;
        if (currentScene == 3)
        {
            currentScene = 0;
        }
        GetComponent<Movement>().enabled = false;
        ReloadScene();
    }
    void StartCrashSquence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);
    }
}
