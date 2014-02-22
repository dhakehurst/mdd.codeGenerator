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
    public class Boolean : Cloneable
    {
        public static implicit operator Boolean(bool value)  // implicit conversion operator
        {
            return new Boolean(value);
        }

        public static implicit operator bool(Boolean value)
        {
            return value.value;
        }

        #region Variables
        System.Boolean value;
        #endregion

        #region Constructors
        public Boolean(System.Boolean value) { this.value = value; }
        public Boolean(Boolean value) { this.value = value.value; }
        #endregion

        #region Operations
        public Boolean not() { return new Boolean(!this.value); }
        public Boolean or(Boolean other) { return new Boolean( this.value | other.value); }
        public Boolean xor(Boolean other) { return new Boolean(this.value ^ other.value); }
        public Boolean and(Boolean other) { return new Boolean(this.value & other.value); }
        public Boolean implies(Boolean other) { return new Boolean( (!this.value | other.value) ); }
        #endregion

        #region Cloneable
        protected virtual object _deepClone() {
            return new Boolean(this.value);
        }
        public Boolean deepClone() {
            return this._deepClone() as Boolean;
        }
        #endregion

        #region Comparison
        public Boolean equalTo(Boolean other) { if (this.value == other.value) { return new Boolean(true); } else { return new Boolean(false); } }
        public Boolean notEqualTo(Boolean other) { if (this.value != other.value) { return new Boolean(true); } else { return new Boolean(false); } }


        public override bool Equals(object obj) 
        {
            if (obj is Boolean)
                return ((Boolean)obj).value.Equals(this.value);
            else
                return false;
        }
        public override int GetHashCode() 
        {
            return this.value.GetHashCode();
        }
        #endregion
        
        #region Framework Converters
        public BitString8 asBitString8() { return new BitString8(System.Convert.ToByte(this.value)); }
        public BitString16 asBitString16() { return new BitString16(System.Convert.ToUInt16(this.value)); }
        public BitString32 asBitString32() { return new BitString32(System.Convert.ToUInt32(this.value)); }
        public BitString64 asBitString64() { return new BitString64(System.Convert.ToUInt64(this.value)); }
        public String asString() { if (this.value) { return new String("True"); } else { return new String("False"); } }
        public Integer asInteger() { return new Integer(this.value?1:0); }
        public PositiveInteger asPositiveInteger() { return new PositiveInteger((uint)(this.value ? 1 : 0)); }
        #endregion

        #region Base Language operators
        public static bool operator true(Boolean self) {
            return true == self.value;
        }
        public static bool operator false(Boolean self) {
            return false == self.value;
        }
        #endregion

        #region Base Language Converters
        public System.Boolean to_Boolean() { return value; }
        public System.Byte to_Byte() { return value ? (byte)1 : (byte)0; }
        public System.Int32 to_Int32() { return value ? 1 : 0; }
        #endregion

        public override System.String ToString() {
            return value.ToString();
        }

    }
}
