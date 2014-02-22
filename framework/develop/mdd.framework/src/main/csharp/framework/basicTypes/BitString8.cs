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
    using System.Collections.Generic;
    using System.Linq;

    public static class BitString8Extensions
    {
        public static String asString(this List<BitString8> self) {
            char[] arr = self.Select(bs => System.Convert.ToChar(bs.to_Byte())).ToArray();
            return new String(new System.String(arr));
        }
    }

    public struct BitString8 : BitString<System.Byte>
    {

        public static implicit operator BitString8(System.Byte value)  // implicit conversion operator
        {
            return new BitString8(value);
        }

        public BitString8(System.String value) {
            this.value = System.Convert.ToByte(value, 2);
        }

        public BitString8(System.Byte value) {
            this.value = value;
        }

        public BitString8(BitString8 value) {
            this.value = value.value;
        }

        System.Byte value;
        public PositiveInteger length {
            get { return 8; }
        }

        public Boolean isSet(PositiveInteger index) {
            return this.at(index);
        }
        public Boolean at(PositiveInteger index) {
            Real p = index.minus(new Integer(1));
            double d = new PositiveInteger(2).power(p).to_Double();
            ulong mask = (ulong)d;
            return 1 == ((this.value & mask) >> (index.to_Int32() - 1));
        }
        public BitString set(PositiveInteger index, Boolean value) {
            Real p = index.minus(new Integer(1));
            double d = new PositiveInteger(2).power(p).to_Double();
            System.Byte mask = (System.Byte)d;
            System.Byte newValue = 0;
            if (value) {
                newValue = (System.Byte)(this.value | mask);
            } else {
                newValue = (System.Byte)(this.value & ~mask);
            }
            return new BitString8(newValue);
        }

        public Boolean this[PositiveInteger index] {
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

        public BitString8 shiftLeft(PositiveInteger n) { return new BitString8((System.Byte)(this.value << n.to_Int32())); }
        public BitString8 shiftRight(PositiveInteger n) { return new BitString8((System.Byte)(this.value >> n.to_Int32())); }
        public BitString8 bitwiseOr(BitString8 n) { return new BitString8((System.Byte)(this.value | n.value)); }
        public BitString8 bitwiseAnd(BitString8 n) { return new BitString8((System.Byte)(this.value & n.value)); }
        public BitString8 bitwiseXor(BitString8 n) { return new BitString8((System.Byte)(this.value ^ n.value)); }
        public BitString8 bitwiseNot() { return new BitString8((System.Byte)~this.value); }

        public BitStringN subBitString(PositiveInteger firstIndex, PositiveInteger lastIndex) {
            int f = firstIndex.to_Int32() - 1;
            int l = lastIndex.to_Int32() - 1;
            int len = this.length.to_Int32();
            if ((l >= f)
                && (0 <= f && f <= len)
                && (0 <= l && l <= len)
                ) {
                int newLength = l - f + 1;
                System.Byte newValue = (System.Byte)(this.value << (7 - l));
                newValue = (System.Byte)(newValue >> (8 - newLength));
                return new BitStringN(newValue, (uint)newLength);
            } else {
                throw new System.IndexOutOfRangeException("subBitString(" + f + ", " + l + ")");
            }

        }

        #region Comparison
        public Boolean equalTo(BitString other) {
            return this.Equals(other);
        }
        public Boolean notEqualTo(BitString other) {
            return !this.Equals(other);
        }
        public override bool Equals(object obj) {
            if (obj is BitString8)
                return ((BitString8)obj).value.Equals(this.value);
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
            string s = System.Convert.ToString(this.value, 2);
            return s.PadLeft(this.length.to_Int32(), '0');
        }
        public String asStringOctal() {
            return System.Convert.ToString(this.value, 8);
        }
        public String asStringHex() {
            int v = this.length.to_Int32() / 4;
            return this.value.ToString("X" + v);
        }
        public Integer asInteger() { return new Integer((int)this.value); }
        public PositiveInteger asPositiveInteger() { return new PositiveInteger((uint)this.value); }
        public Real asRealTwosComplementFormat(PositiveInteger digitsAfterRadixPoint) {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<Boolean> asSequenceOfBoolean() {
            for (int i = 0; i < this.length.to_Int32(); ++i) {
                yield return this.at((uint)(i + 1));
            }
        }

        #endregion

        #region Base Language converters
        public System.Byte to_Byte() {
            return (System.Byte)(value & 0xFF);
        }
        public System.UInt16 to_UInt16() {
            return (System.UInt16)(value & 0xFFFF);
        }
        public System.UInt32 to_UInt32() {
            return (System.UInt32)(value & 0xFFFFFFFF);
        }
        public System.UInt64 to_UInt64() {
            return (System.UInt64)(value & 0xFFFFFFFFFFFFFFFF);
        }
        #endregion

        public BitString deepClone() {
            return new BitString8(this.value);
        }

        public override System.String ToString() {
            return this.asStringHex().ToString();
        }
    }

}

