using System;
using System.Linq;

namespace LongIdentifierLibrary
{
    /// <summary>
    /// Контроллер для работы с глобальным уникальным идентификатором
    /// </summary>
    public class IdController
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public static string Identifier => string.Concat(_baseAlphaNumericIdentifier, _currentIdentifierNumber);

        private static string _currentIdentifierNumber = string.Empty;
        private static string _baseAlphaNumericIdentifier;
        private static readonly object _lockObject = new Object();

        /// <summary>
        /// Проинициализировать новый буквенно-числовой идентификатор. 
        /// Прошлый идентификатор с его инкрементированием будет потерян!
        /// </summary>
        /// <param name="alphaNumericIdentifier"></param>
        public static void InitAlphaNumericIdentifier(string alphaNumericIdentifier)
        {
            CheckAlphaNumericIdentifier(alphaNumericIdentifier);

            lock (_lockObject)
            {
                _baseAlphaNumericIdentifier = alphaNumericIdentifier;
                _currentIdentifierNumber = string.Empty;
            }
        }

        private static void CheckAlphaNumericIdentifier(string alphaNumericIdentifier)
        {
            if (!alphaNumericIdentifier.All(char.IsLetterOrDigit))
            {
                throw new IdentifierException(alphaNumericIdentifier);
            }
        }

        /// <summary>
        /// Увеличивает уникальный идентификатор Identifier на значение inrementNumber с поддержкой отмены.
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="inrementNumber">Число на которое нужно увеличить уникальный идентификатор, 
        /// строка должна содержать только цифры и может быть любой длинны</param>
        /// <param name="isCancelled">Функция для возможности прирывания увеличения уникального идентификатора</param>
        /// <returns></returns>
        public static void Increment(string inrementNumber, Func<bool> isCancelled)
        {
            lock (_lockObject)
            {
                var currentStringNumber = StringNumber.Parse(_currentIdentifierNumber);
                var inrementStringNumber = StringNumber.Parse(inrementNumber);
                _currentIdentifierNumber = currentStringNumber.Increment(inrementStringNumber, isCancelled).Value;
            }
        }

        /// <summary>
        /// Увеличивает уникальный идентификатор Identifier на значение inrementNumber
        /// </summary>
        /// <param name="inrementNumber"></param>
        public static void Increment(string inrementNumber)
        {
            Increment(inrementNumber, () => false);
        }
    }
}