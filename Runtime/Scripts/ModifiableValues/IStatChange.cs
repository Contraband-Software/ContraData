using System;

namespace Software.Contraband.Data
{
    // [Serializable]
    public interface IStatChange<in T>
    {
        public float GetStatChange(T stat);
        public float GetStatMultiplier(T stat);

        public void ResetModifier();
    }
}