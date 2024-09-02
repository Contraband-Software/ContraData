namespace Software.Contraband.Data.System
{
    // ReSharper disable once UnusedType.Global
    public static class TypeUtilities
    {
        /// <summary>
        /// Nullable types will return false.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNumeric(this object o) 
            => 
                o is 
                    byte or sbyte or            // bytes
                    ushort or uint or ulong or  // unsigned ints
                    short or int or long or     // signed ints
                    float or double or decimal; // real numbers
    }
}