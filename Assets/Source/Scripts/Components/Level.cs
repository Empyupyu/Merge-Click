using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField] public Transform PreviewPoint { get; private set; }
    [field: SerializeField] public GameOverListener GameOverListener { get; private set; }
}
