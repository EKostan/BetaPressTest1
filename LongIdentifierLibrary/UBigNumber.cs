using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LongIdentifierLibrary
{
    public class UBigNumber
    {
        private const int PowBase = 32;
        private const int SymbolCount = 9;

        private uint[] _bits;
        private string _stringValue;

        //public UBigNumber(UBigNumber value)
        //    : this(value._bits)
        //{
        //    _stringValue = GetStringValue();
        //}

        protected UBigNumber(uint[] bits)
        {
            _bits = bits;
            _stringValue = GetStringValue();
        }

        public static UBigNumber operator +(UBigNumber n1, UBigNumber n2)
        {
            if (IsLessThan(n1._bits, n2._bits))
                return AddLessRightNumber(n1._bits, n2._bits);
            else
                return AddLessRightNumber(n2._bits, n1._bits);
        }

        /// <summary>
        /// Разбор строки, на выходе получаем UBigNumber
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static UBigNumber Parse(string stringValue)
        {
            var memberCount = stringValue.Length / SymbolCount + (stringValue.Length % SymbolCount > 0 ? 1 : 0);
            var bits = new uint[memberCount];

            for (int i = memberCount - 1; i >= 0; i--)
            {
                var startIndex = i * SymbolCount;
                var symbolsResidue = stringValue.Length - startIndex;
                var memberValueLength = Math.Min(symbolsResidue, SymbolCount);

                var memberValueString = stringValue.Substring(startIndex, memberValueLength);
                var memberValue = uint.Parse(memberValueString);
                var memberIndex = memberCount - 1 - i;
                bits[memberIndex] = memberValue;
            }

            return new UBigNumber(bits);
        }

        private static bool IsLessThan(uint[] leftMembers, uint[] rightMembers)
        {
            return leftMembers.Length > rightMembers.Length
                   || (leftMembers.Length == rightMembers.Length && CompareLastMembers(leftMembers, rightMembers));
        }

        private static bool CompareLastMembers(uint[] leftMembers, uint[] rightMembers)
        {
            return leftMembers[leftMembers.Length - 1] > rightMembers[rightMembers.Length - 1];
        }

        private static UBigNumber AddLessRightNumber(uint[] leftMembers, uint[] rightMembers)
        {
            var bitsList = new List<uint>();
            var maxMembersCount = leftMembers.Length;
            uint carry = 0;
            for (int i = 0; i < maxMembersCount; i++)
            {
                var leftMember = leftMembers[i];

                if (i > rightMembers.Length)
                {
                    bitsList.Add(SumMembers(leftMember, 0, ref carry));
                    continue;
                }

                var rightMember = rightMembers[i];

                bitsList.Add(SumMembers(leftMember, rightMember, ref carry));
            }

            return new UBigNumber(bitsList.ToArray());
        }

        private static uint SumMembers(uint leftMembers, uint rightMembers, ref uint carry)
        {
            var sumLong = (ulong)leftMembers + rightMembers + carry;
            carry = (uint)(sumLong >> PowBase);
            return (uint)sumLong;
        }

        private string GetStringValue()
        {
            var sb = new StringBuilder();
            
            for (int i = _bits.Length - 1; i >= 0; i--)
            {
                var memberValueString = _bits[i].ToString();
                if (memberValueString.Length < SymbolCount && i != 0)
                {
                    sb.Append(new string('0', SymbolCount - memberValueString.Length));
                }

                sb.Append(_bits[i]);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return _stringValue;
        }
    }
}
