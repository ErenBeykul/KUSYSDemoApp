using System.Text;

namespace KUSYSDemoApp.Domain.Extensions
{
    public static class SecurityExtensions
    {        
        /// <summary>
        /// Verilen Değerin ...
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string OEncode(this string text)
        {
            return BitConverter.ToString(Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.ASCII.GetBytes(text)))).Replace("-", "");
        }


        /// <summary>
        /// Verilen Değerin ...
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ODecode(this string text)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(Encoding.ASCII.GetString(text.StringToByteArray())));
        }


        /// <summary>
        /// Verilen Değeri ...
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] StringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}