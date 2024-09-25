using System;
using System.Collections.Generic;
using System.Linq;

using Software.Contraband.Data.System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Software.Contraband.Data
{
    public abstract class ModifiableValues<T, V> : MonoBehaviour 
        where T : struct, Enum
        where V : unmanaged // int, byte, float, etc. Only, no reference types, not even strings
    {
        // Public config
        [FormerlySerializedAs("baseStats")] [SerializeField] private GenericStatsAsset<T, V> baseGenericStats;
        
        // State
        public List<IStatChange<T>> ActiveModifiers { get; private set; } = new();
        
        #region Unity Callbacks
        // private void Update()
        // {
        //     print("Mods: " + ActiveModifiers.Count);
        // }
        #endregion
        
        #region Public API
        /// <summary>
        /// Returns all the info about a stat, including base value, icon, name, description, etc.
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public GenericStatsAsset<T, V>.StatValue GetBaseStatInfo(T stat)
        {
            var s = baseGenericStats.GetStat(stat);
#if UNITY_EDITOR
            if (s is null)
                throw new InvalidOperationException("This stat has no base value recorded in the stats asset!");
#endif
            return s;
        }
        
        /// <summary>
        /// Returns a stat value, with the additive upgrades applied first, then the multiplicative ones.
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public V GetStat(T stat)
        {
            V baseStat = baseGenericStats.GetStat(stat).value;
#if UNITY_EDITOR
            if (ActiveModifiers.Count > 0)
                if (typeof(V).IsNumeric())
                    throw new ArithmeticException("Attempted to use value modifiers on non-number types.");
#endif
            // ActiveModifiers.ForEach(f => baseStat += PollChange(f, stat));
            // ActiveModifiers.ForEach(f => baseStat *= PollMultiplier(f, stat));
            ExpressionEvaluator.Evaluate(baseStat.ToString(), out V res);
            return res;
        }

        /// <summary>
        /// Overwrite the active firmwares.
        /// </summary>
        /// <param name="modifiers"></param>
        public void SetModifiers(IEnumerable<IStatChange<T>> modifiers)
        {
            ActiveModifiers = modifiers.ToList();
        }

        /// <summary>
        /// Checks if a firmware upgrade is present.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool ModifierPresent<T>()
        {
            return ActiveModifiers.Any(f => f.GetType() == typeof(T));
        }
        
        public bool ModifierPresent(Type t)
        {
            return ActiveModifiers.Any(f => f.GetType() == t);
        }
        #endregion

        #region GET_UPGRADE_VALUE
        private static float PollChange(IStatChange<T> statChange, T stat)
        {
            try
            {
                float query = statChange.GetStatChange(stat);
                if (float.IsNaN(query)) return 0;
                return query;
            }
            catch (NoMoreStatsException)
            {
                return 0;
            }
        }

        private static float PollMultiplier(IStatChange<T> statChange, T stat)
        {
            try
            {
                float query = statChange.GetStatMultiplier(stat);
                if (float.IsNaN(query)) return 1;
                return query;
            }
            catch (NoMoreStatsException)
            {
                return 1;
            }
        }
        #endregion
    }
}