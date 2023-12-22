using UnityEngine;
using Zenject;

public class AnimalMoveSystem : GameSystem
{
    [Inject] private MovingConfigData _movingConfigData;

    public override void OnUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_game.CurrentAnimal == null) return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.y = _movingConfigData.MovingOffset.y;
        worldPos.z = _movingConfigData.MovingOffset.z;
        _game.CurrentAnimal.transform.position = Vector3.MoveTowards(_game.CurrentAnimal.transform.position, worldPos, _movingConfigData.MoveSpeed * Time.deltaTime);
    }
}
