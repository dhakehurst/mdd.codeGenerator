/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.collections
{
    using framework.basicTypes;
    using System.Linq;

    /// <summary>
    /// An implementation of the Sequence type from
    /// the OMG OCL standard
    /// </summary>
    public interface OrderedSet<T> : Sequence<T>, Set<T>
    {

    }
}
