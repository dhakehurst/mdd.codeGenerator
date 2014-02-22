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

    /// <summary>
    /// An implementation of the Sequence type from
    /// the OMG OCL standard
    /// </summary>
    public interface Sequence<T> : Bag<T>
    {
        T[] array { get; }

        T at(Integer index);
        PositiveInteger indexOf(T element);

        Sequence<T2> cast<T2>();

        Sequence<T> append(T newElement);
        Sequence<T> concatinate(Sequence<T> other);
        Sequence<T> prepend(T newElement);

        Sequence<T> including(T newElement);
        Sequence<T> excluding(T element);
        Sequence<T> subSequence(PositiveInteger firstIndex, PositiveInteger lastIndex);
        T first();
        Sequence<T> tail();
        T last();
        Sequence<T> reverseTail();

        Sequence<T> transitiveClosure(global::System.Func<T, Sequence<T>> expr);


        Sequence<T> deepClone();

        void set(PositiveInteger index, T value);
        void setRange(PositiveInteger index, Sequence<T> value);
    
    }
}
