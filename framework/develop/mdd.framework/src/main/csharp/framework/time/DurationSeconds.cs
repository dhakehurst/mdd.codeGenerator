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
	public class DurationSeconds
	: global::framework.time.Duration
	{
	
	  // --- Constructors ---
	    public DurationSeconds(double value) : base(value) {}
        public DurationSeconds(global::framework.time.Duration value) : base(value.asSeconds) { }
	
	  // --- Operations ---

        public override DurationSeconds asSeconds {
            get { return this; }
        }
        public override DurationMilliseconds asMilliseconds {
            get { return new DurationMilliseconds(this.value * 1000); }
        }
    }

}
