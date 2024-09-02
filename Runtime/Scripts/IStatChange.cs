namespace Software.Contraband.Data
{
    public interface IStatChange<in T>
    {
        float GetStatChange(T stat);
        float GetStatMultiplier(T stat);

        void ResetModifier();
    }
}