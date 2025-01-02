using UnityEngine;

public class Oscillation : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    Vector3 startingPosition;
    Vector3 endPosition;
    float movementFactor;
    void Start()
    {
        startingPosition = transform.position;
        endPosition = startingPosition + movementVector;
    }

    // Update is called once per frame
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startingPosition, endPosition, movementFactor);
    }
}
