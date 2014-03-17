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

	//Primitive Type
	public class AngleDegrees
	: global::framework.math.Angle
	{
	
	  // --- Constructors ---
	    public AngleDegrees(double value) : base(value) {}
        public AngleDegrees(global::framework.basicTypes.Real value) : base(value) { }
	
	  // --- Operations ---


        public override AngleRadians radians {
            get { return new AngleRadians(this.value * System.Math.PI / 180); }
        }

        public override AngleDegrees degrees {
            get { return this; }
        }
    }

}
