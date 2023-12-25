using DG.Tweening;
using Lean.Pool;
using System.Linq;
using UnityEngine;
using Zenject;
public class PreviewAnimalSpawnerSystem : GameSystem
{
    [Inject] private readonly AnimalConfigData _animalConfigData;

    public override void OnAwake()
    {
        _game.OnAnimalDeselectedSingal.AddListener(Sorting);

        PreLoad();
    }

    private void PreLoad()
    {
        for (int i = 0; i < _animalConfigData.AnimalsInPreview; i++) Spawn();

        _game.OnAnimalSpawnedSingal.Dispatch();

        Sorting();
    }

    private void Sorting()
    {
        for (int i = 0; i < _game.PreviewAnimals.Count; i++)
        {
            var animal = _game.PreviewAnimals.ElementAt(i);

            var positionByIndex = GetPositionOffsetByIndex(i);
            animal.transform.DOMove(positionByIndex, _animalConfigData.SortingDuration).SetEase(Ease.OutBack);
        }

        Spawn();
    }

    private void Spawn()
    {
        var evolutions = _animalConfigData.EvolutionStage;
        var animalPrefab = evolutions[Random.Range(0, _animalConfigData.MaximumEvolutionStageLevelInPreview)].Animal;

        var positionByIndex = GetPositionOffsetByIndex(_game.PreviewAnimals.Count);
        var newAnimal = LeanPool.Spawn(animalPrefab, positionByIndex, Quaternion.Euler(0, 180, 0));
        newAnimal.ResetToOrigin();

        newAnimal.transform.DOScale(_animalConfigData.AnimalsInPreviewScale.x, _animalConfigData.SortingDuration).From(Vector3.zero);

        _game.PreviewAnimals.Enqueue(newAnimal);
    }

    private Vector3 GetPositionOffsetByIndex(int index)
    {
        var previewPosition = _animalConfigData.AnimalsInPreviewPosition;
        return new Vector3(previewPosition.x + (index * _animalConfigData.OffsetPerAnimalInRow), previewPosition.y, previewPosition.z);
    }
}
