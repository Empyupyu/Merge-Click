using Supyrb;
using System.Collections.Generic;

public class GameData
{
    public Animal CurrentAnimal;
    public Queue<Animal> PreviewAnimals = new Queue<Animal>();

    public OnAnimalSpawnedSingal OnAnimalSpawnedSingal = Signals.Get<OnAnimalSpawnedSingal>(); 
    public OnAnimalDeselectedSingal OnAnimalDeselectedSingal = Signals.Get<OnAnimalDeselectedSingal>();
}
