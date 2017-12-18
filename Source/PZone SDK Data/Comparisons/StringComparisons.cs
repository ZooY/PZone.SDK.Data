using System;


namespace PZone.Data.Comparisons
{
    /// <summary>
    /// Сравнение строк.
    /// </summary>
    public class StringComparisons
    {

        /* The Winkler modification will not be applied unless the 
         * percent match was at or above the mWeightThreshold percent 
         * without the modification. 
         * Winkler's paper used a default value of 0.7
         */
        private const double JARO_WINKLER_WEIGHT_THRESHOLD = 0.7;

        /* Size of the prefix to be concidered by the Winkler modification. 
         * Winkler's paper used a default value of 4
         */
        private const int JARO_WINKLER_NUM_CHARS = 4;


        /// <summary>
        /// Расстояние Джаро между двумя последовательностями символов. Чем меньше 
        /// расстояние, тем больше сходства имеют эти строки друг с другом: 0 означает отсутствие
        /// сходства, а 1 — точное совпадение. 
        /// </summary>
        /// <param name="string1">Первая строка.</param>
        /// <param name="string2">Вторая строка.</param>
        /// <returns>
        /// Метод возвращает значение от 0 до 1.
        /// </returns>
        public static double JaroDistance(string string1, string string2)
        {
            var len1 = string1.Length;
            var len2 = string2.Length;
            if (len1 == 0)
                return len2 == 0 ? 1.0 : 0.0;

            var searchRange = Math.Max(0, Math.Max(len1, len2) / 2 - 1);

            var matched1 = new bool[len1];
            var matched2 = new bool[len2];

            var numCommon = 0;
            for (var i = 0; i < len1; ++i)
            {
                var start = Math.Max(0, i - searchRange);
                var end = Math.Min(i + searchRange + 1, len2);
                for (var j = start; j < end; ++j)
                {
                    if (matched2[j]) continue;
                    if (string1[i] != string2[j])
                        continue;
                    matched1[i] = true;
                    matched2[j] = true;
                    ++numCommon;
                    break;
                }
            }
            if (numCommon == 0)
                return 0.0;

            var numHalfTransposed = 0;
            var k = 0;
            for (var i = 0; i < len1; ++i)
            {
                if (!matched1[i]) continue;
                while (!matched2[k]) ++k;
                if (string1[i] != string2[k])
                    ++numHalfTransposed;
                ++k;
            }
            var numTransposed = numHalfTransposed / 2;
            double numCommonD = numCommon;
            return (numCommonD / len1 + numCommonD / len2 + (numCommon - numTransposed) / numCommonD) / 3.0;
        }


        /// <summary>
        /// Расстояние Джаро—Винклера между двумя последовательностями символов. Чем меньше 
        /// расстояние, тем больше сходства имеют эти строки друг с другом: 0 означает отсутствие
        /// сходства, а 1 — точное совпадение. 
        /// </summary>
        /// <param name="string1">Первая строка.</param>
        /// <param name="string2">Вторая строка.</param>
        /// <returns>
        /// Метод возвращает значение от 0 до 1.
        /// </returns>
        public static double JaroWinklerDistance(string string1, string string2)
        {
            var jaroDistance = JaroDistance(string1, string2);
            if (jaroDistance <= JARO_WINKLER_WEIGHT_THRESHOLD)
                return jaroDistance;
            var max = Math.Min(JARO_WINKLER_NUM_CHARS, Math.Min(string1.Length, string2.Length));
            var pos = 0;
            while (pos < max && string1[pos] == string2[pos])
                ++pos;
            if (pos == 0)
                return jaroDistance;
            return jaroDistance + 0.1 * pos * (1.0 - jaroDistance);
        }
    }
}