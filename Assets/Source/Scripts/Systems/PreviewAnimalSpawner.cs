using UnityEngine;
using Zenject;

public class AnimalSelecter : GameSystem
{
    public override void OnAwake()
    {
       
    }

    private void Select()
    {
        _game.CurrentAnimal = _game.PreviewAnimals.Dequeue();
    }

    private void DeSelect()
    {
        _game.CurrentAnimal = null;
    }
}

public class PreviewAnimalSpawner : GameSystem
{
    [Inject] private readonly AnimalConfigData _animalConfigData;

    public override void OnAwake()
    {
        PreLoad();
    }


    private void PreLoad()
    {
        for (int i = 0; i < _animalConfigData.AnimalsInPreview; i++)
        {
            Spawn();
        }


    }

    private void Spawn()
    {
        var animals = _animalConfigData.Animals;
        var animalPrefab = animals[Random.Range(0, animals.Count)];

        var newAnimal = Instantiate(animalPrefab, new Vector3(2 + (_game.PreviewAnimals.Count * .2f), 2f, 0), Quaternion.identity);
        newAnimal.transform.localScale = _animalConfigData.AnimalsInPreviewScale;

        _game.PreviewAnimals.Enqueue(newAnimal);
    }
}
