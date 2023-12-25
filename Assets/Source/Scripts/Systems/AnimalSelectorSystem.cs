using DG.Tweening;
using UnityEngine;

public class AnimalSelectorSystem : GameSystem
{
    public override void OnAwake()
    {
        _game.OnAnimalSpawnedSingal.AddListener(Select);
    }

    public override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_game.CurrentAnimal == null) return;
            if (_game.GameOver) return;

            DeSelect();
        }
    }

    private void Select()
    {
        _game.CurrentAnimal = _game.PreviewAnimals.Dequeue();
    }

    private void DeSelect()
    {
        _game.CurrentAnimal.Rigidbody.isKinematic = false;
        _game.CurrentAnimal.Collider.isTrigger = false;

        _game.CurrentAnimal.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).OnComplete(() => 
        {
            Select();
            _game.OnAnimalDeselectedSingal.Dispatch();
        });

        _game.CurrentAnimal = null;
    }
}
