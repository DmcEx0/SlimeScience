using System;
using System.Collections.Generic;

public class Slime : MobileObject
{
    private float _distanceForFear;

    private void Update()
    {
        UpdateStateMachine();
    }

    protected override void Init(MobileObjectConfig config)
    {
        if (config is not SlimeConfig)
            return;

        SlimeConfig slimeConfig = config as SlimeConfig;

        _distanceForFear = slimeConfig.DistanceFofFear;
    }

    protected override Dictionary<StatesType, IBehaviour> GetBehaviours()
    {
        //return new Dictionary<Type, IBehaviour>()
        //{
        //    [typeof(FearState)] = new FearState(this),
        //    [typeof(PatrollState)] = new PatrollState(this),
        //};
        return new Dictionary<StatesType, IBehaviour>()
        {
            //[StatesType.Fear] = new FearState(this),
            //[StatesType.Patroll] = new PatrollState(this),
        };
    }

    protected override IBehaviour GetStartState()
    {
        return new PatrollState(this);
    }
}