using SlimeScience.Saves;
using System;
using System.Collections;
using UnityEngine;

namespace SlimeScience.Effects
{
    public class EffectSystem
    {
        private Queue _buffs = new Queue();
        private Queue _debuffs = new Queue();

        private GameVariables _gameVariables;
        private MonoBehaviour _owner;
        private Coroutine _coroutine;

        public event Action EffectApplied;
        public event Action EffectEnded;

        public EffectSystem(
            MonoBehaviour owner, 
            GameVariables gameVariables)
        {
            _owner = owner;
            _gameVariables = gameVariables;
        }

        public bool DebuffActive => _debuffs.Count > 0;

        public bool BuffActive => _buffs.Count > 0;


        public void ApplyEffect(EffectModifiers effect, float value, float duration)
        {
            _gameVariables.AddModifier(effect, value);

            bool isBuff = IsBuff(value);

            if (isBuff)
            {
                _buffs.Enqueue(effect);
            }
            else
            {
                _debuffs.Enqueue(effect);
            }

            EffectApplied?.Invoke();
            _coroutine = _owner.StartCoroutine(RemoveEffect(effect, isBuff, value, duration));
        }

        private IEnumerator RemoveEffect(
            EffectModifiers effect,
            bool isBuff,
            float value,
            float delay)
        {
            yield return new WaitForSeconds(delay);

            _gameVariables.RemoveModifier(effect, value);

            if (isBuff)
            {
                _buffs.Dequeue();
            }
            else
            {
                _debuffs.Dequeue();
            }

            EffectEnded?.Invoke();
        }

        private bool IsBuff(float value)
        {
            return value > 0;
        }
    }
}
