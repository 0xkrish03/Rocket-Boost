using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandle : MonoBehaviour
{
    int currentScene;
    [SerializeField] float delay = 2f;
    private bool hasCrashed = false; // Flag to prevent repeated collision handling

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        GetComponent<Movement>().enabled = true; // Enable movement
    }

    private void OnCollisionEnter(Collision other)
    {
        if (hasCrashed) return; // Prevent handling collisions if a crash sequence has started

        string tag = other.gameObject.tag;

        // Only handle collisions if movement is enabled
        if (GetComponent<Movement>().enabled)
        {
            switch (tag)
            {
                case "Friendly":
                    Debug.Log("Bumped into a friendly Game Object");
                    break;

                case "Finish":
                    HandleFinishCollision();
                    break;

                case "Fuel":
                    Debug.Log("You gained extra points");
                    break;

                default:
                    HandleCrashCollision();
                    break;
            }
        }
    }

    private void HandleFinishCollision()
    {
        if (hasCrashed) return; // Prevent multiple executions
        hasCrashed = true;

        Debug.Log("Finish line reached. Loading next scene...");
        GetComponent<Movement>().enabled = false; // Disable movement
        Invoke(nameof(LoadNextScene), delay);
    }

    private void HandleCrashCollision()
    {
        if (hasCrashed) return; // Prevent multiple executions
        hasCrashed = true;

        Debug.Log("Crash occurred. Reloading scene...");
        GetComponent<Movement>().enabled = false; // Disable movement
        Invoke(nameof(ReloadScene), delay);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    private void LoadNextScene()
    {
        currentScene++;
        if (currentScene >= SceneManager.sceneCountInBuildSettings) // Ensure you loop back to the first scene
        {
            currentScene = 0;
        }
        SceneManager.LoadScene(currentScene);
    }
}
