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

        public int MaxItems { get; private set; }

        public int Value => _items.Count;

        public bool IsFull => _items.Count >= MaxItems;


        public void Add(T item)
        {
            if (_items.Count >= MaxItems)
            {
                return;
            }

            _items.Add(item);
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
