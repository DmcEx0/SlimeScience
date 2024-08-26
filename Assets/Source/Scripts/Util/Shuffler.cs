using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Util
{
    public class Shuffler
    {
        public static void Shuffle<T>(ref List<T> collection)
        {
            for (int i = collection.Count - 1; i >= 1; i--)
            {
                int rnd = Random.Range(0, i);

                var tmp = collection[i];

                collection[i] = collection[rnd];
                collection[rnd] = tmp;
            }
        }
    }
}