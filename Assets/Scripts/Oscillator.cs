using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startPos;
    Vector3 endPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;

    float movementFactor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + movementVector;
    }

    // Update is called once per frame
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);

        transform.position = Vector3.Lerp(startPos, endPos, movementFactor );
        
    }
}
