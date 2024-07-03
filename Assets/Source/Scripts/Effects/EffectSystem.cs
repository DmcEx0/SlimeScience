using SlimeScience.Saves;
using System;
using System.Collections;
using UnityEngine;

namespace SlimeScience.Effects
{
    public class EffectSystem
    {
        private GameVariables _gameVariables;
        private MonoBehaviour _owner;
        private Coroutine _coroutine;

        public event Action EffectApplied;
        public event Action EffectEnded;

        public EffectSystem(MonoBehaviour owner, GameVariables gameVariables)
        {
            _owner = owner;
            _gameVariables = gameVariables;
        }

        public void ApplyEffect(EffectModifiers effect, float value, float duration)
        {
            _gameVariables.AddModifier(effect, value);

            if (_coroutine != null)
            {
                _owner.StopCoroutine(_coroutine);
            }

            EffectApplied?.Invoke();
            _coroutine = _owner.StartCoroutine(RemoveEffect(effect, value, duration));
        }

        private IEnumerator RemoveEffect(EffectModifiers effect, float value, float delay)
        {
            yield return new WaitForSeconds(delay);

            _gameVariables.RemoveModifier(effect, value);
            EffectEnded?.Invoke();
        }
    }
}
