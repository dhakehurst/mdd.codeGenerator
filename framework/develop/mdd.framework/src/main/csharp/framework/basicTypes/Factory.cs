/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.basicTypes
{
    public interface Factory
    {
        String String(System.String value);
        Boolean Boolean(System.Boolean value);
        Real Real(System.Double value);
        Integer Integer(System.Int64 value);
        PositiveInteger PositiveInteger(System.UInt64 value);
    }
}
