using DG.Tweening;
using System.Linq;
using UnityEngine;
using Zenject;
public class GameOverChecker : GameSystem
{
    public override void OnAwake()
    {
        _game.OnAnimalDeselectedSingal.AddListener(CheckAnimalPosition);   
    }

    private void CheckAnimalPosition()
    {

    }
}
public class PreviewAnimalSpawner : GameSystem
{
    [Inject] private readonly AnimalConfigData _animalConfigData;

    public override void OnAwake()
    {
        _game.OnAnimalDeselectedSingal.AddListener(Sorting);

        PreLoad();
    }

    private void PreLoad()
    {
        for (int i = 0; i < _animalConfigData.AnimalsInPreview; i++)
        {
            Spawn();
        }

        _game.OnAnimalSpawnedSingal.Dispatch();

        Sorting();
    }

    private void Sorting()
    {
        for (int i = 0; i < _game.PreviewAnimals.Count; i++)
        {
            var animal = _game.PreviewAnimals.ElementAt(i);

            var previewPosition = _animalConfigData.AnimalsInPreviewPosition;
            animal.transform.DOMove(new Vector3(previewPosition.x + (i * _animalConfigData.OffsetPerAnimalInRow), previewPosition.y, previewPosition.z), _animalConfigData.SortingDuration).SetEase(Ease.OutBack);
        }

        Spawn();
    }

    private void Spawn()
    {
        var animals = _animalConfigData.Animals;
        var animalPrefab = animals[Random.Range(0, animals.Count)];

        var previewPosition = _animalConfigData.AnimalsInPreviewPosition;
        var newAnimal = Instantiate(animalPrefab, new Vector3(previewPosition.x + (_game.PreviewAnimals.Count * _animalConfigData.OffsetPerAnimalInRow), previewPosition.y, previewPosition.z), Quaternion.Euler(0, 180, 0));
        newAnimal.transform.localScale = _animalConfigData.AnimalsInPreviewScale;

        _game.PreviewAnimals.Enqueue(newAnimal);
    }
}
