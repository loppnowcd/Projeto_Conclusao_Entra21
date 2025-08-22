namespace System
{
    public static class StringExtension
    {
        /// <summary>
        /// Remove qualquer caracter exceto numeros
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string OnlyNumbers(this string input)
        {
            string newString = "";
            for (int i = 0; i < input.Length; i++)
            {
                newString+= char.IsNumber(input[i]) ? input[i].ToString() : string.Empty;
            }

            return newString;
        }
    }
}
