using System;
using UnityEngine;
using Zenject;

public class AnimalMoveSystem : GameSystem
{
    [Inject] private readonly MovingConfigData _movingConfigData;

    private Camera _camera;

    public override void OnAwake()
    {
        _camera = Camera.main;
    }

    public override void OnUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_game.CurrentAnimal == null) return;

        _game.CurrentAnimal.transform.position = Vector3.MoveTowards(_game.CurrentAnimal.transform.position, GetOffsetWorldPointPosition(), _movingConfigData.MoveSpeed * Time.deltaTime);
    }

    private Vector3 GetOffsetWorldPointPosition()
    {
        Vector3 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.x = Math.Clamp(worldPos.x, _movingConfigData.MovingPositionClampMinimumX, _movingConfigData.MovingPositionClampMaximumX);
        worldPos.y = _movingConfigData.MovingOffset.y;
        worldPos.z = _movingConfigData.MovingOffset.z;

        return worldPos;
    }
}
