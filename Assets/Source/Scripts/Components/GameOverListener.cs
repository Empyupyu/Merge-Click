using System;
using UnityEngine;

public class GameOverListener : MonoBehaviour
{
    public event Action<Transform> OnTriggerEnterEvent, OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other.transform);
    }
}
