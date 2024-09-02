using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Software.Contraband.Data
{
    public abstract class GenericStatsAsset<T> : ScriptableObject where T : struct, IConvertible
    {
        [Serializable]
        public class StatValue
        {
            public T stat;
            public Sprite icon;
            public string friendlyName = "";
            public float value;
        }
        
        [SerializeField] List<StatValue> values = new();
    
        public StatValue GetStat(T stat)
        {
            StatValue query = values.FirstOrDefault(s => s.stat.Equals(stat));
            return query;
        }
    }
}