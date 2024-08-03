namespace SlimeScience.Effects
{
    public class EffectRenderData
    {
        public EffectModifiers Effect { get; private set; }
        public float Value { get; private set; }
        public bool IsBuff { get; private set; }

        public EffectRenderData(EffectModifiers effect, float value, bool isBuff)
        {
            Effect = effect;
            Value = value;
            IsBuff = isBuff;
        }
    }
}
