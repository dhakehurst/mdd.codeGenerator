/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.basicTypes
{
    using framework.collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class BitString64Extensions
    {
        public static String asString(this List<BitString64> self) {
            char[] arr = self.Select(bs => System.Convert.ToChar(bs.to_UInt64())).ToArray();
            return new String(new System.String(arr));
        }
    }

    public struct BitString64 : BitString<System.UInt64>
    {
        System.UInt64 value;
        public PositiveInteger length {
            get { return 64; }
        }

        public static implicit operator BitString64(System.UInt64 value)  // implicit conversion operator
        {
            return new BitString64(value);
        }

        public BitString64(System.String value) {
            this.value = System.Convert.ToUInt64(value, 2);
        }

        public BitString64(System.UInt64 value) {
            this.value = value;
        }

        public BitString64(BitString64 value) {
            this.value = value.value;
        }

        /*
        public BitString(System.UInt32 value) {
            this._length = 32;
            this.value = value;
        }

        public BitString(System.UInt16 value) {
            this._length = 16;
            this.value = value;
        }

        public BitString(System.Byte value) {
            this._length = 8;
            this.value = value;
        }

        public BitString(PositiveInteger length, System.UInt64 value)
        {
            this._length = length;
            this.value = value;
        }
        public BitString(BitString value)
        {
            this._length = value.length;
            this.value = value.value;
        }
        */

        public Boolean isSet(PositiveInteger index) {
            return this.at(index);
        }
        public Boolean at(PositiveInteger index)
        {
            Real p = index.minus(new Integer(1));
            double d = new PositiveInteger(2).power(p).to_Double();
            System.UInt64 mask = (System.UInt64)d;
            return 1 == ((this.value & mask) >> (index.to_Int32() - 1));
        }
        public BitString set(PositiveInteger index, Boolean value) {
            Real p = index.minus(new Integer(1));
            double d = new PositiveInteger(2).power(p).to_Double();
            System.UInt64 mask = (System.UInt64)d;
            System.UInt64 newValue = 0;
            if (value) {
                newValue = this.value | mask;
            } else {
                newValue = this.value & ~mask;
            }
            return new BitString64(newValue);
        }

        public Boolean this[PositiveInteger index]
        {
            get { return this.at(index); }
        }

        /// <summary>
        /// This function sets the range of bits from index to index+value.length to the bits in value
        /// </summary>
        /// <param name="index">index of first bit to set (indexing starts at 1)</param>
        /// <param name="value">the new bit values for the specified range</param>
        public BitString setRange(PositiveInteger index, BitString value) {
            uint p = index.to_UInt32();
            BitString newBs = this.deepClone();
            foreach (Boolean b in value.asSequenceOfBoolean()) {
                newBs = newBs.set(p, b);
                p++;
            }
            return newBs;
        }

        public BitString64 shiftLeft(PositiveInteger n) { return new BitString64(this.value << n.to_Int32()); }
        public BitString64 shiftRight(PositiveInteger n) { return new BitString64(this.value >> n.to_Int32()); }
        public BitString64 bitwiseOr(BitString n) { return new BitString64(this.value | n.to_UInt64()); }
        public BitString64 bitwiseAnd(BitString n) { return new BitString64(this.value & n.to_UInt64()); }
        public BitString64 bitwiseXor(BitString n) { return new BitString64(this.value ^ n.to_UInt64()); }
        public BitString64 bitwiseNot() { return new BitString64(~this.value); }

        public BitStringN subBitString(PositiveInteger firstIndex, PositiveInteger lastIndex) {
            int f = firstIndex.to_Int32() - 1;
            int l = lastIndex.to_Int32() - 1;
            int len = this.length.to_Int32();
            if (   (l >= f)
                && (0 <= f && f <= len)
                && (0 <= l && l <= len)
                )
            {
                int newLength = l - f +1;
                System.UInt64 newValue = this.value << (63 - l);
                newValue = newValue >> (64 - newLength);
                return new BitStringN(newValue, (uint)newLength);
            } else {
                throw new System.IndexOutOfRangeException("subBitString("+f+", "+l+")");
            }

        }

        #region Comparison
        public Boolean equalTo(BitString other) {
            return this.Equals(other);
        }
        public Boolean notEqualTo(BitString other) {
            return ! this.Equals(other);
        }

        public override bool Equals(object obj) {
            if (obj is BitString)
                return ((BitString)obj).to_UInt64().Equals(this.to_UInt64());
            else
                return false;
        }
        public override int GetHashCode() {
            return this.value.GetHashCode();
        }
        #endregion

        #region Framework Converters
        public String asString() {
            char c = System.Convert.ToChar(this.value);
            return "" + c;
        }
        public String asStringBinary() {
            string s = System.Convert.ToString((long)this.value, 2);
            return s.PadLeft(this.length.to_Int32(), '0');
        }
        public String asStringOctal() {
            return System.Convert.ToString((long)this.value, 8);
        }
        public String asStringHex() {
            int v = this.length.to_Int32() / 4;
            return this.value.ToString("X" + v);
        }
        public Integer asInteger() { return new Integer((long)this.value); }
        public PositiveInteger asPositiveInteger() { return new PositiveInteger((ulong)this.value); }
        public Real asReal() {
            long lv = (long)this.value;
            double d = System.BitConverter.Int64BitsToDouble(lv);
            return new Real(d);
        }
        public Real asRealTwosComplementFormat(PositiveInteger digitsAfterRadixPoint) {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<Boolean> asSequenceOfBoolean() {
            for (int i = 0; i < this.length.to_UInt32(); ++i) {
                yield return this.at((uint)(i+1));
            }
        }
        public Sequence<BitString8> asSequenceOfBitString8() {
            os.OsRef os = new os.OsRef();
            byte[] bytes = System.BitConverter.GetBytes(this.value);
            return os.Sequence(bytes.Select(b => new BitString8(b)));
        }
        #endregion

        #region Base Language converters
        public System.Byte to_Byte() {
            return (System.Byte)(value & 0xFF);
        }
        public System.UInt16 to_UInt16()
        {
            return (System.UInt16)(value & 0xFFFF);
        }
        public System.UInt32 to_UInt32()
        {
            return (System.UInt32)(value & 0xFFFFFFFF);
        }
        public System.UInt64 to_UInt64()
        {
            return (System.UInt64)(value);
        }
        #endregion

        public BitString deepClone() {
            return new BitString64(this.value);
        }

        public override System.String ToString() {
            return this.asStringHex().ToString();
        }
    }

}
