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
    //Primitive Type
	public class Real : Cloneable
	{
        public static implicit operator Real(System.Double value)  // implicit conversion operator
        {
            return new Real(value);
        }

        #region Variables
        protected System.Double value;
        #endregion

        #region Constructors
        public Real(System.Double value) { this.value = value; }
        public Real(Real value) { this.value = value.value; }
        #endregion

        #region Operations
        public Real squared() { return power(new Real(2.0)); }
        public Real power(Real exponent) { return new Real(System.Math.Pow(this.value, exponent.value)); }
        public Real absolute() { return new Real(System.Convert.ToDouble(System.Math.Abs(this.value))); }
        public Real exponential() { return new Real(System.Math.Exp(this.value)); }
        public Real log() { return new Real(System.Math.Log(this.value)); }
        public Real log(Real toTheBase) { return new Real(System.Math.Log(this.value, toTheBase.value)); }
        //public Real arccos() { return new Real(System.Math.Acos(this.value)); }
        //public Real arcsin() { return new Real(System.Math.Asin(this.value)); }
        //public Real arctan() { return new Real(System.Math.Atan(this.value)); }
        public Integer truncate() { return new Integer((int)System.Math.Truncate(value)); }
        public Integer floor() { return new Integer((int)System.Math.Floor(value)); }
        public Integer ceiling() { return new Integer((int)System.Math.Ceiling(value)); }
        public Real negate() { return new Real(this.value * -1); }

        public virtual Real modulo(Real denominator) { return new Real(this.value % denominator.value); }
        public virtual Real plus(Real other) { return new Real(this.value + other.value); }
        public virtual Real minus(Real other) { return new Real(this.value - other.value); }
        public virtual Real multiply(Real other) { return new Real(this.value * other.value); }
        public virtual Real divide(Real other) { return new Real(this.value / other.value); }
        public virtual Real max(Real other) { if (other.value > this.value) { return other; } else { return this; } }
        public virtual Real min(Real other) { if (other.value < this.value) { return other; } else { return this; } }
        #endregion

        #region Cloneable
        protected virtual Real _deepClone() {
            return new Real(this.value);
        }
        public Real deepClone() {
            return this._deepClone();
        }
        #endregion

        #region Comparison
        public Boolean equalTo(Real other) { if (this.value == other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean notEqualTo(Real other) { if (this.value != other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean greaterThan(Real other) { if (this.value > other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean greaterThanOrEqualTo(Real other) { if (this.value >= other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean lessThan(Real other) { if (this.value < other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean lessThanOrEqualTo(Real other) { if (this.value <= other.value) { return new Boolean(true); } else { return new Boolean(false); } }

        public override bool Equals(object obj) {
            if (obj is Real)
                return ((Real)obj).value.Equals(this.value);
            else
                return false;
        }
        public override int GetHashCode() {
            return this.value.GetHashCode();
        }
        #endregion

        #region Framework Converters
        public virtual String asString() { return new String(value.ToString()); }
        public virtual BitString64 asBitString64() {
            long l = System.BitConverter.DoubleToInt64Bits(this.value);
            return new BitString64((ulong)l);
        }
        public virtual BitString32 asBitString32() {
            float f = (float)this.value;
            byte[] bytes = System.BitConverter.GetBytes(f);
            int i = System.BitConverter.ToInt32(bytes,0);
            return new BitString32((uint)i);
        }
        public virtual BitStringN asBitStringMostSignificantSignBit(PositiveInteger length, PositiveInteger digitsAfterRadixPoint) {
            if (length < 2) {
                throw new Exception("asBitStringMostSignificantSignBit must have length >= 2");
            }
            BitStringN bs = new BitStringN(0, length);

            Real s = new Integer(2).power(digitsAfterRadixPoint);
            Real v1 = this * s;
            System.Double v2 = v1.absolute().to_Double();
            System.UInt64 v3 = (System.UInt64)v2;
            BitStringN n = new BitStringN(v3, (length-1).asPositiveInteger());
            bs = (BitStringN)bs.setRange(1, n);
            if (this.value < 0) {
                bs = (BitStringN)bs.set(length, true);
            }
            /*
            
            //set sign bit
            if (this.value < 0) {
                bs = (BitStringN)bs.set(length, true);
            }
            PositiveInteger wholeLength = (length - 1 - digitsAfterRadixPoint).asPositiveInteger();
            BitStringN whole = this.absolute().floor().asBitString64().subBitString(1, wholeLength);
            bs = (BitStringN)bs.setRange((digitsAfterRadixPoint+1).asPositiveInteger(), whole);

            Real part = this.absolute() - this.absolute().floor();
            Real s = new Integer(2).power(digitsAfterRadixPoint);
            BitStringN p = part.multiply(s).asBitString64().subBitString(1, digitsAfterRadixPoint);
            
            */
            return bs;
        }
        public virtual BitStringN asBitStringNoSignBit(PositiveInteger length, PositiveInteger digitsAfterRadixPoint) {
            if (length < 1) {
                throw new Exception("asBitStringMostSignificantSignBit must have length >= 1");
            }
            if (this >= 0) {
                double s = new Integer(2).power(digitsAfterRadixPoint).to_Double();
                System.Double v1 = this.value * s;
                System.UInt64 v2 = (System.UInt64)v1;
                BitStringN bs = new BitStringN(v2, length); 
                /*
                BitStringN bs = new BitStringN(0, length);
                PositiveInteger wholeLength = (length - digitsAfterRadixPoint).asPositiveInteger();
                BitStringN whole = this.absolute().floor().asBitString64().subBitString(1, wholeLength);
                bs = (BitStringN)bs.setRange((digitsAfterRadixPoint + 1).asPositiveInteger(), whole);

                Real part = this.absolute() - this.absolute().floor();
                
                BitStringN p = part.multiply(s).asBitString64().subBitString(1, digitsAfterRadixPoint);
                bs = (BitStringN)bs.setRange(1, p);
                */
                return bs;
            } else {
                //can't convert negative real value without a sign bit
                throw new Exception("Cannot convert negative value to BitString without a sign bit: " + this);
            }
        }
        public virtual Boolean asBoolean() { return new Boolean(0 < this.value); }
        public virtual PositiveInteger asPositiveInteger() { return new PositiveInteger((uint)this.value); }
        public virtual Integer asInteger() { return new Integer((int)this.value); }
        #endregion

        #region Base Language operators
        public static Boolean operator >(Real left, Real right) {
            return left.greaterThan(right);
        }
        public static Boolean operator <(Real left, Real right) {
            return left.lessThan(right);
        }
        public static Boolean operator <=(Real left, Real right) {
            return left.lessThanOrEqualTo(right);
        }
        public static Boolean operator >=(Real left, Real right) {
            return left.greaterThanOrEqualTo(right);
        }

        public static Real operator +(Real left, Real right) {
            return left.plus(right);
        }
        public static Real operator -(Real left, Real right) {
            return left.minus(right);
        }
        public static Real operator *(Real left, Real right) {
            return left.multiply(right);
        }
        public static Real operator /(Real left, Real right) {
            return left.divide(right);
        }
        #endregion

        #region Base Language Converters
        public System.Double to_Double() { return (System.Double)value; }
        public System.Single to_Single() { return (System.Single)value; }
        #endregion

        public override System.String ToString() {
            return value.ToString();
        }
    }

}
