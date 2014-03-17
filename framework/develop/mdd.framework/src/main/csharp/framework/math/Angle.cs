/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.math {
    using framework.basicTypes;

    public static class AngleExtensionToReal {
        public static Angle arcsin(this Real value) {
            return new AngleRadians(System.Math.Asin(value.to_Double()));
        }
        public static Angle arccos(this Real value) {
            return new AngleRadians(System.Math.Acos(value.to_Double()));
        }
        public static Angle arctan(this Real value) {
            return new AngleRadians(System.Math.Atan(value.to_Double()));
        }
    }

	//Primitive Type
	public abstract class Angle
	: global::framework.basicTypes.Real
	{
	
	  // --- Constructors ---
	    public Angle(double value) : base(value) {}
        public Angle(global::framework.basicTypes.Real value) : base(value) { }
	
        public abstract AngleRadians radians { get; }
        public abstract AngleDegrees degrees { get; }

        #region Operations
        public Real sin() {
            double v = System.Math.Sin( this.radians.to_Double() );
            return new Real(v);
        }
        public Real cos() {
            double v = System.Math.Cos(this.radians.to_Double());
            return new Real(v);
        }
        public Real tan() {
            double v = System.Math.Tan(this.radians.to_Double());
            return new Real(v);
        }
        #endregion

    }

}
