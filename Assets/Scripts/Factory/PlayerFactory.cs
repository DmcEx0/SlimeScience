using System;
using System.Collections.Generic;
using static UnityEditor.VersionControl.Asset;

public abstract class PlayerFactory : GameObjectFactory
{
    public Player Get()
    {
        var config = GetConfig();
        Player instance = CreateInstance(config.Prefab);

        IInputRouter inputRouter = new PlayerInputRouter();

        instance.Init(CreateStateMachine(instance), inputRouter, config);

        return instance;
    }

    protected abstract PlayerConfig GetConfig();

    private StateMachine CreateStateMachine(Player instance)
    {
        Dictionary<Type, IBehaviour> states = new Dictionary<Type, IBehaviour>()
        {
            [typeof(IdleState)] = new IdleState(instance),
            [typeof(MovementState)] = new MovementState(instance)
        };

        IBehaviour startBehaviour = new IdleState(instance);
        return new StateMachine(startBehaviour, states);
    }
}