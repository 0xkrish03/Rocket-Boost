using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a friendly Game Object");
                break;
            case "Finish":
                Debug.Log("Congrats you finished the game");
                break;
            case "Fuel":
                Debug.Log("You gained extra points");
                break;
            default:
                Debug.Log("You Exploded");

                break;
        }
    }
}
