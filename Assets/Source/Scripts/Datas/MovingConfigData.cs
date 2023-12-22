using UnityEngine;

[CreateAssetMenu(fileName = "MovingConfigData", menuName = "Configs/MovingConfig")]
public class MovingConfigData : ScriptableObject
{
    [field: SerializeField] public Vector3 MovingOffset { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }

}
