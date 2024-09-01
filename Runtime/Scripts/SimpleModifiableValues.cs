using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Software.Contraband.Data
{
    public sealed class SimpleModifiableValues : MonoBehaviour
    {
        // Public config
        [SerializeField] private Stats baseStats;
        
        // State
        public List<IStatChange> ActiveModifiers { get; private set; } = new();
        
        #region Public API
        /// <summary>
        /// Returns all the info about a stat, including base value, icon, name, description, etc.
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public StatValue GetBaseStatInfo(Stat stat)
        {
            var s = baseStats.GetStat(stat);
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
        public float GetStat(Stat stat)
        {
            var baseStat = baseStats.GetStat(stat).value;
            ActiveModifiers.ForEach(f => baseStat += PollChange(f, stat));
            ActiveModifiers.ForEach(f => baseStat *= PollMultiplier(f, stat));
            return baseStat;
        }
        
        /// <summary>
        /// Overwrite the active firmwares.
        /// </summary>
        /// <param name="types"></param>
        public void SetModifiers(IEnumerable<IStatChange> modifiers)
            => ActiveModifiers = modifiers.ToList();

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
        private static float PollChange(IStatChange statChange, Stat stat)
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

        private static float PollMultiplier(IStatChange statChange, Stat stat)
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