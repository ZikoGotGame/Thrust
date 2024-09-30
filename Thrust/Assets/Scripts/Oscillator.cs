using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // Continually calculating the total cycles.
        const float tau = Mathf.PI * 2; // tau constant, one full oscilliation.
        float rawSinWave = Mathf.Sin(cycles * tau); // Finding the Sin value of the cycles.

        movementFactor = (rawSinWave + 1f) / 2f; // Changing the movement factor (adding 1 and dividing by 2 to get range 0 to 1 instead of -1 to 1).

        Vector3 offset = movementVector * movementFactor; // The offset to add to the current position.
        transform.position = startingPosition + offset; // Update the position of the oscillator.
    }
}
