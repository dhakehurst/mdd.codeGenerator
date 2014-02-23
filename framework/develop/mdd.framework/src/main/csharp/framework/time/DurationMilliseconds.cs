/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.time {

	//Primitive Type
	public class DurationMilliseconds
	: global::framework.time.Duration
	{
	
	  // --- Constructors ---
	    public DurationMilliseconds(double value) : base(value) {}
	    public DurationMilliseconds(global::framework.time.Duration value) : base(value) {}
	
	  // --- Operations ---


        public override DurationSeconds asSeconds {
            get { return new DurationSeconds(this.value/1000); }
        }
        public override DurationMilliseconds asMilliseconds {
            get { return this; }
        }
    }

}
