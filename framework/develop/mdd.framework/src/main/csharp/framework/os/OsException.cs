/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.os {

	using global::framework.basicTypes;

	// Exception
	public class OsException : global::framework.basicTypes.Exception
	{
	
	  // --- Constructors ---
	    public OsException(global::framework.basicTypes.String message) : base(message) {
	    }
		
	}

} //namespace framework.os
