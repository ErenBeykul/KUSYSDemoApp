namespace KUSYSDemoApp.Domain.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Belli Bir Enum Değerini Küçük Harfli Dizgi (String) Eşdeğerine Çevirir
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLowerString(this Enum value)
        {
            return value.ToString().ToLower();
        }
    }
}