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
	public abstract class Duration
	: global::framework.basicTypes.Real
	{
	
	  // --- Constructors ---
	    public Duration(double value) : base(value) {}
	    public Duration(global::framework.basicTypes.Real value) : base(value) {}
	
	  // --- Operations ---

        public abstract DurationMilliseconds asMilliseconds { get; }
        public abstract DurationSeconds asSeconds { get; }
    }

}
