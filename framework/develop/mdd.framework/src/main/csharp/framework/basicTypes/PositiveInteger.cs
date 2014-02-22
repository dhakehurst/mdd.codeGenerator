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
	public class PositiveInteger : Integer
	{
        public static implicit operator PositiveInteger(System.UInt64 value)  // implicit conversion operator
        {
            return new PositiveInteger(value);
        }
        
        public PositiveInteger(System.UInt64 value)
            : base((System.Int64)value)
        {
            if (value < 0)
                throw new System.ArgumentOutOfRangeException("value", value, "Range: >= 0");
        }
        public PositiveInteger(PositiveInteger value) : base(value) {
        }

        #region Cloneable
        protected override Real _deepClone() {
            return new PositiveInteger((ulong)this.value);
        }
        public new PositiveInteger deepClone() {
            return this._deepClone() as PositiveInteger;
        }
        #endregion

        public override System.String ToString() {
            return value.ToString();
        }
    }

}
