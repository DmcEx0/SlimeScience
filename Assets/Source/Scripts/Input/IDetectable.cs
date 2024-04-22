using System;

namespace SlimeScience.Input
{
    public interface IDetectable
    {
        public event Action Detected;
    }
}