using DG.Tweening;
using UnityEngine;

public class AnimalSelectorSystem : GameSystem
{
    private float _delayDeSelect = .15f;
    private float _animalScaleDuration = .5f;
    private float _timerForActivateDeSecelt;

    public override void OnAwake()
    {
        _game.OnAnimalSpawnedSingal.AddListener(Select);
    }

    private void Select()
    {
        _game.CurrentAnimal = _game.PreviewAnimals.Dequeue();
    }

    public override void OnUpdate()
    {
        DeSelectTimer();
        DeSelect();
    }

    private void DeSelectTimer()
    {
        _timerForActivateDeSecelt = _timerForActivateDeSecelt > 0 ? _timerForActivateDeSecelt - Time.deltaTime : 0;
    }

    private void DeSelect()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (_game.CurrentAnimal == null) return;
        if (_game.GameOver) return;
        if (_timerForActivateDeSecelt > 0) return;

        ActiveAnimalPhysic(_game.CurrentAnimal);

        _game.CurrentAnimal.transform.DOScale(Vector3.one, _animalScaleDuration).SetEase(Ease.OutBack).OnComplete(() => 
        {
            _timerForActivateDeSecelt = _delayDeSelect;
            Select();
            _game.OnAnimalDeselectedSingal.Dispatch();
        });

        _game.CurrentAnimal = null;
    }

    private void ActiveAnimalPhysic(Animal animal)
    {
        animal.Rigidbody.isKinematic = false;
        animal.Collider.isTrigger = false;
    }
}
