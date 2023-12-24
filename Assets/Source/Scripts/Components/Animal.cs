using Supyrb;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public Collider Collider { get; private set; }

    private MergingSingal _mergingSingal;
    private bool _isMerge;

    public void Initialize(int id)
    {
        ID = id;
    }

    public void Merge()
    {
        _isMerge = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isMerge) return;
        if (!collision.collider.TryGetComponent<Animal>(out var animal)) return;
        if (!animal.ID.Equals(ID)) return;

        Merge();
        animal.Merge();

        if (_mergingSingal == null) _mergingSingal = Signals.Get<MergingSingal>();

        _mergingSingal.Dispatch(this, animal);
    }
}
