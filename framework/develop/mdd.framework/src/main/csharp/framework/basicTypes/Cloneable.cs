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
    /* indicates a 'trust me there is a deepClone method'
     * don't want to put the method signature here as
     * C# does not allow covariant return types, and thus
     * subtypes cannot have a deepClone operation that
     * returns the correct type.
     */
    public interface Cloneable {
    }
}
