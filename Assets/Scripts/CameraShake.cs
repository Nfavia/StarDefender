using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float ShakeDuration = 1f;
    [SerializeField] float shakeMagnitude = .5f;

    Vector3 initalPos;

    void Start()
    {
        initalPos = transform.position;

    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0;
        while (elapsedTime < ShakeDuration)
        {
            transform.position = initalPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initalPos;
    }
}
