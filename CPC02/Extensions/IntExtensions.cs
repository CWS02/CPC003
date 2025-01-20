using System;

namespace prjWastes6.Extensions
{
    public static class IntExtensions
    {
        public static string ToSymbol(this int value)
        {
            switch (value)
            {
                case 0:
                    return "-";
                case 1:
                    return "V";
                case 2:
                    return "X";
                default:
                    return string.Empty; 
            }
        }

        public static string ToSymbol(this short value) 
        {
            return ((int)value).ToSymbol();
        }
    }
}
