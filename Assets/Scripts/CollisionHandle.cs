using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandle : MonoBehaviour
{
    int currentScene;
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    private bool isControllable = false; // Flag to prevent repeated collision handling
    bool isCollidable = true;
    AudioSource audioSource;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;


    bool controllable = true;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        GetComponent<Movement>().enabled = true; // Enable movement
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }
    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextScene();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            Debug.Log($"colliding :{isCollidable}");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isControllable || !isCollidable) return; // Prevent handling collisions if a crash sequence has started

        string tag = other.gameObject.tag;

        // Only handle collisions if movement is enabled
        if (!controllable) return;
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

    private void HandleFinishCollision()
    {
        if (isControllable) return; // Prevent multiple executions
        isControllable = true;

        controllable = false;
        audioSource.Stop();
        Debug.Log("Finish line reached. Loading next scene...");
        GetComponent<Movement>().enabled = false; // Disable movement
        audioSource.PlayOneShot(successSound);
        successParticle.Play();
        Invoke(nameof(LoadNextScene), delay);
    }

    private void HandleCrashCollision()
    {
        if (isControllable) return; // Prevent multiple executions
        isControllable = true;

        controllable = false;
        audioSource.Stop();
        Debug.Log("Crash occurred. Reloading scene...");
        GetComponent<Movement>().enabled = false; // Disable movement
        audioSource.PlayOneShot(crashSound);
        crashParticle.Play();
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
