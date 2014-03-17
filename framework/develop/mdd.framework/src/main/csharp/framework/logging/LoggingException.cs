/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.logging {

	using global::framework.basicTypes;

	// Class
	public class LoggingException
	: global::framework.basicTypes.Exception
	
	{
	
	  // --- Constructors ---
        public LoggingException(String message)
            : base(message) {

	    }

	
	}

} //namespace framework.configuration
