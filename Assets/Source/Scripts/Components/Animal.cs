using UnityEngine;

public class Animal : MonoBehaviour
{
     [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
     [field: SerializeField] public Collider Collider { get; private set; }
}
