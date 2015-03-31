using System.Collections.Generic;
using System.Linq;

namespace TestApplication.Helper
{

    internal class TranslitLetter
    {
        public string Rus { get; private set; }
        public string Eng { get; private set; }

        public TranslitLetter(string rus, string eng)
        {
            Rus = rus;
            Eng = eng;
        }
    }

    /// <summary>
    /// extension для транслитерации строк 
    /// </summary>
    public static class TranslitHelper
    {
        public static string Translit(this string str)
        {
            var list = new List<TranslitLetter>
            {
                new TranslitLetter("а", "a"),
                new TranslitLetter("б", "b"),
                new TranslitLetter("в", "v"),
                new TranslitLetter("г", "g"),
                new TranslitLetter("д", "d"),
                new TranslitLetter("е", "e"),
                new TranslitLetter("ё", "yo"),
                new TranslitLetter("ж", "zh"),
                new TranslitLetter("з", "z"),
                new TranslitLetter("и", "i"),
                new TranslitLetter("й", "j"),
                new TranslitLetter("к", "k"),
                new TranslitLetter("л", "l"),
                new TranslitLetter("м", "m"),
                new TranslitLetter("н", "n"),
                new TranslitLetter("о", "o"),
                new TranslitLetter("п", "p"),
                new TranslitLetter("р", "r"),
                new TranslitLetter("с", "s"),
                new TranslitLetter("т", "t"),
                new TranslitLetter("у", "u"),
                new TranslitLetter("ф", "f"),
                new TranslitLetter("х", "h"),
                new TranslitLetter("ц", "c"),
                new TranslitLetter("ч", "ch"),
                new TranslitLetter("ш", "sh"),
                new TranslitLetter("щ", "sch"),
                new TranslitLetter("ъ", "j"),
                new TranslitLetter("ы", "i"),
                new TranslitLetter("ь", "j"),
                new TranslitLetter("э", "e"),
                new TranslitLetter("ю", "yu"),
                new TranslitLetter("я", "ya"),
                new TranslitLetter("А", "A"),
                new TranslitLetter("Б", "B"),
                new TranslitLetter("В", "V"),
                new TranslitLetter("Г", "G"),
                new TranslitLetter("Д", "D"),
                new TranslitLetter("Е", "E"),
                new TranslitLetter("Ё", "Yo"),
                new TranslitLetter("Ж", "Zh"),
                new TranslitLetter("З", "Z"),
                new TranslitLetter("И", "I"),
                new TranslitLetter("Й", "J"),
                new TranslitLetter("К", "K"),
                new TranslitLetter("Л", "L"),
                new TranslitLetter("М", "M"),
                new TranslitLetter("Н", "N"),
                new TranslitLetter("О", "O"),
                new TranslitLetter("П", "P"),
                new TranslitLetter("Р", "R"),
                new TranslitLetter("С", "S"),
                new TranslitLetter("Т", "T"),
                new TranslitLetter("У", "U"),
                new TranslitLetter("Ф", "F"),
                new TranslitLetter("Х", "H"),
                new TranslitLetter("Ц", "C"),
                new TranslitLetter("Ч", "CH"),
                new TranslitLetter("Ш", "SH"),
                new TranslitLetter("Щ", "SCH"),
                new TranslitLetter("Ъ", "J"),
                new TranslitLetter("Ы", "I"),
                new TranslitLetter("Ь", "J"),
                new TranslitLetter("Э", "E"),
                new TranslitLetter("Ю", "YU"),
                new TranslitLetter("Я", "YA")
            };
            return list.Aggregate(str, (current, letter) => current.Replace(letter.Rus, letter.Eng));
        }
    }
}
