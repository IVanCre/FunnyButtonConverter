
using System;

namespace Ninja
{
    class Crypter
    {


        public static string Encode(string s, bool side)//обычное смещение на 1символ  вперед, side=true = +1, иначе -1. 
        {
            char[] charMas = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                Int16 hex = Convert.ToInt16(charMas[i]);
                if (side == true)
                    hex = (short)(hex + 1);
                else
                    hex = (short)(hex - 1);
                charMas[i] = Convert.ToChar(hex);
            }
            string f = new string(charMas);
            return f;
        }
    }
}
