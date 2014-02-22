/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.basicTypes
{
    using framework.collections;

    public interface BitString : Cloneable
    {
        PositiveInteger length { get; }

        Boolean isSet(PositiveInteger index);
        Boolean at(PositiveInteger index);
        BitString set(PositiveInteger index, Boolean value);

        Boolean this[PositiveInteger index] { get; }

        /// <summary>
        /// This function sets the range of bits from index to index+value.length to the bits in value
        /// </summary>
        /// <param name="index">index of first bit to set (indexing starts at 1)</param>
        /// <param name="value">the new bit values for the specified range</param>
        BitString setRange(PositiveInteger index, BitString value);
        /*
        BitString shiftLeft(PositiveInteger n);
        BitString shiftRight(PositiveInteger n);
        BitString bitwiseOr(BitString n);
        BitString bitwiseAnd(BitString n);
        BitString bitwiseXor(BitString n);
        BitString bitwiseNot();
        */
        BitStringN subBitString(PositiveInteger firstIndex, PositiveInteger lastIndex);

        #region Comparison
        Boolean equalTo(BitString other);
        Boolean notEqualTo(BitString other);
        bool Equals(object obj);
        int GetHashCode();
        #endregion

        #region Framework Converters

        String asString();
        String asStringBinary();
        String asStringOctal();
        String asStringHex();
        Integer asInteger();
        PositiveInteger asPositiveInteger();
        Real asRealTwosComplementFormat(PositiveInteger digitsAfterRadixPoint);

        System.Collections.Generic.IEnumerable<Boolean> asSequenceOfBoolean();

        #endregion

        #region Base Language converters
        System.Byte to_Byte();
        System.UInt16 to_UInt16();
        System.UInt32 to_UInt32();
        System.UInt64 to_UInt64();
        #endregion



    }


    public interface BitString<T> : BitString where T : struct
    {

    }
}
