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
	public class Integer : Real
	{
        public static implicit operator Integer(System.Int64 value)  // implicit conversion operator
        {
            return new Integer(value);
        }

        #region Variables
        protected new System.Int64 value;
        #endregion

        #region Constructors
        public Integer(System.Int64 value) : base(value) { this.value = value; }
        public Integer(Integer value) : base(value) { this.value = value.value; }
        #endregion

        #region Operations
        public new Integer negate() { return new Integer(this.value * -1); }
        public new PositiveInteger absolute() { return new PositiveInteger(System.Convert.ToUInt32(System.Math.Abs(this.value))); }
        public Integer plus(Integer other) { return new Integer(this.value + other.value); }
        public Integer minus(Integer other) { return new Integer(this.value - other.value); }
        public Integer multiply(Integer other) { return new Integer(this.value * other.value); }
        public Integer divide(Integer other) { return new Integer(this.value / other.value); }
        public Integer modulo(Integer other) { return new Integer(this.value % other.value); }
        public Integer max(Integer other) { if (other.value > this.value) { return other; } else { return this; } }
        public Integer min(Integer other) { if (other.value < this.value) { return other; } else { return this; } }

        public override Real plus(Real other)           { return base.plus(other); }
        public override Real minus(Real other)          { return base.minus(other); }
        public override Real multiply(Real other)       { return base.multiply(other); }
        public override Real divide(Real other)         { return base.divide(other); }
        public override Real modulo(Real denominator)   { return base.modulo(denominator); }
        public override Real max(Real other)            { return base.max(other); }
        public override Real min(Real other)            { return base.min(other); }
        #endregion

        #region Cloneable
        protected override Real _deepClone() {
            return new Integer(this.value);
        }
        public new Integer deepClone() {
            return this._deepClone() as Integer;
        }
        #endregion

        #region Comparison
        public Boolean equalTo(Integer other) { if (this.value == other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean notEqualTo(Integer other) { if (this.value != other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean greaterThan(Integer other) { if (this.value > other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean greaterThanOrEqualTo(Integer other) { if (this.value >= other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean lessThan(Integer other) { if (this.value < other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean lessThanOrEqualTo(Integer other) { if (this.value <= other.value) { return new Boolean(true); } else { return new Boolean(false); } }

        public override bool Equals(object obj) {
            if (obj is Integer)
                return ((Integer)obj).value.Equals(this.value);
            else
                return false;
        }
        public override int GetHashCode() {
            return this.value.GetHashCode();
        }
        #endregion

        #region Framework Converters
        public override String asString() { return new String(value.ToString()); }
        public virtual BitString8 asBitString8() { return new BitString8((byte)this.value); }
        public virtual BitString16 asBitString16() { return new BitString16((ushort)this.value); }
        public override BitString32 asBitString32() { return new BitString32((uint)this.value); }
        public override BitString64 asBitString64() { return new BitString64((uint)this.value); }
        public override Boolean asBoolean() { return new Boolean(0 < this.value); }
        public virtual Real asReal() { return new Real(this.value); }
        public override Integer asInteger() { return new Integer(this.value); }
        public override PositiveInteger asPositiveInteger() { return new PositiveInteger((uint)this.value); }
        #endregion

        #region Base Language operators
        public static Boolean operator >(Integer left, Integer right) {
            return left.greaterThan(right);
        }
        public static Boolean operator <(Integer left, Integer right) {
            return left.lessThan(right);
        }
        public static Boolean operator <=(Integer left, Integer right) {
            return left.lessThanOrEqualTo(right);
        }
        public static Boolean operator >=(Integer left, Integer right) {
            return left.greaterThanOrEqualTo(right);
        }
        public static Integer operator +(Integer left, Integer right) {
            return left.plus(right);
        }
        public static Integer operator -(Integer left, Integer right) {
            return left.minus(right);
        }
        public static Integer operator *(Integer left, Integer right) {
            return left.multiply(right);
        }
        public static Real operator /(Integer left, Integer right) {
            return left.divide(right);
        }
        #endregion

        #region Base Language converters
        public System.Int64  to_Int64()  { return (System.Int64)  System.Convert.ToInt64(value); }
        public System.Int32  to_Int32()  { return (System.Int32)  System.Convert.ToInt32(value); }
        public System.Int16  to_Int16()  { return (System.Int16)  System.Convert.ToInt16(value); }
        public System.UInt64 to_UInt64() { return (System.UInt64) System.Convert.ToUInt64(value); }
        public System.UInt32 to_UInt32() { return (System.UInt32) System.Convert.ToUInt32(value); }
        public System.UInt16 to_UInt16() { return (System.UInt16) System.Convert.ToUInt16(value); }
        public System.Byte   to_Byte()   { return (System.Byte)   System.Convert.ToByte(value); }
        #endregion

        public override System.String ToString() {
            return value.ToString();
        }
    }

}
