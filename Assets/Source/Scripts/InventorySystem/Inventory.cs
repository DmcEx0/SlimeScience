using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.InventorySystem
{
    public class Inventory<T>
        where T : MonoBehaviour
    {
        private List<T> _items = new List<T>();

        public Inventory(int maxItems)
        {
            MaxItems = maxItems;
        }

        public event Action Filled;

        public int MaxItems { get; private set; }

        public int Amount => _items.Count;

        public bool IsFull => _items.Count >= MaxItems;


        public void Add(T item)
        {
            if (_items.Count >= MaxItems)
            {
                return;
            }

            _items.Add(item);

            if (_items.Count >= MaxItems)
            {
                Filled?.Invoke();
            }
        }

        public List<T> Free()
        {
            var items = new List<T>(_items);
            _items.Clear();

            return items;
        }

        public void Expand(int amount)
        {
            if (amount < 0)
            {
                return;
            }

            MaxItems += amount;
        }
    }
}
