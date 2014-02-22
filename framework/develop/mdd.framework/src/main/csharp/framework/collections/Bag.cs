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
    /// An implementation of the Bag type from
    /// the OMG OCL standard
    /// </summary>
    public interface Bag<T> : System.Collections.Generic.IEnumerable<T>, Cloneable
    {
        PositiveInteger size { get; }
        Boolean isEmpty{get;}

        Boolean includes(T other);
        Boolean includesAll(Bag<T> other);
        Boolean excludes(T other);
        Boolean excludesAll(Bag<T> other);

        Boolean forAll(global::System.Func<T, Boolean> expr);
        void forEvery(global::System.Action<T> expr);
        R iterate<R>(R initialValue, global::System.Func<T, R, R> expr);
        Bag<T2> collect<T2>(global::System.Func<T, T2> expr);
        Bag<T> select(global::System.Func<T, Boolean> expr);
        Bag<T> reject(global::System.Func<T, Boolean> expr);


        Set<T> asSet();
        Sequence<T> asSequence();
        OrderedSet<T> asOrderedSet();
    }
}
