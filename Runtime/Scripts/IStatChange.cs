

namespace Software.Contraband.Data
{
    public interface IStatChange
    {
        float GetStatChange(Stat stat);
        float GetStatMultiplier(Stat stat);

        void ResetModifier();
    }
}