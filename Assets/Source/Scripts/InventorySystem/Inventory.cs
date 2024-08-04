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

        public Inventory(float maxItems)
        {
            MaxItems = (int)maxItems;
        }

        public event Action Filled;

        public event Action Changed;
        
        public event Action Released;
        
        public event Action Expanded;

        public int MaxItems { get; private set; }

        public int Amount => _items.Count;
        
        public bool IsFull => _items.Count >= MaxItems;
        
        public int AvailableSpace => MaxItems - Amount;
        public T GetTypeInInventory => _items.Count !=0 ? _items[0] : null;
        
        public void Add(T item)
        {
            if (_items.Count >= MaxItems)
            {
                return;
            }

            _items.Add(item);
            Changed?.Invoke();

            if (_items.Count >= MaxItems)
            {
                Filled?.Invoke();
            }
        }

        public List<T> Free()
        {
            var items = new List<T>(_items);
            _items.Clear();

            Released?.Invoke();

            return items;
        }

        public void Expand(int amount)
        {
            if (amount < 0)
            {
                return;
            }

            MaxItems += amount;
            Expanded?.Invoke();
        }

        public void Fill(int count)
        {
            if (count < 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                _items.Add(null);
            }

            Changed?.Invoke();

            if (_items.Count >= MaxItems)
            {
                Filled?.Invoke();
            }
        }
        
        public T GetItem(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                return null;
            }

            var item = _items[index];
            
            _items.RemoveAt(index);
            Changed?.Invoke();

            return item;
        }
    }
}
