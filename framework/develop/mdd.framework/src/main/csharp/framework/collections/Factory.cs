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

    public interface Factory
    {
        Sequence<T> Sequence<T>();
        Sequence<T> Sequence<T>(T[] array);
        Sequence<T> Sequence<T>(System.Collections.Generic.IEnumerable<T> enumerable);

        Sequence<BitString8> SequenceOfBitString8<T>(T structure) where T : struct;
        T Structure<T>(Sequence<BitString8> self) where T : struct;

        Set<T> Set<T>();
        Set<T> Set<T>(System.Collections.Generic.IEnumerable<T> enumerable);
    }
}
