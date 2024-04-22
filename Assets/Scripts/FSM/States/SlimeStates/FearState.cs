using UnityEngine;

public class FearState : IBehaviour
{
    private Slime _slime;

    public FearState(Slime slime)
    {
        _slime = slime;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public bool IsReady()
    {
        return false;
    }

    public void Update()
    {
    }
}
