using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Software.Contraband.Data
{
    public abstract class GenericStatsAsset<K, V> : ScriptableObject 
        where K : struct, Enum 
        where V : unmanaged
    {
        [Serializable]
        public class StatValue
        {
            public K stat;
            public Sprite icon;
            public string friendlyName = "";
            public V value;
        }
        
        // design this class with the generic instantiation, then force inherit from it (implement getvalue method (generic)?)
        // [Serializable]
        // public abstract class AbstractStatValue
        // {
        //     public K stat;
        //     public Sprite icon;
        //     public string friendlyName = "";
        //     public V value;
        // }
        
        [SerializeField] List<StatValue> values = new();
    
        public StatValue GetStat(K stat)
        {
            StatValue query = values.FirstOrDefault(s => s.stat.Equals(stat));
            return query;
        }
    }
}