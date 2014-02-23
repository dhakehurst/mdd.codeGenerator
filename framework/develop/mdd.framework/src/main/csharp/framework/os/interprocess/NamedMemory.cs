/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.os.interprocess {
    using framework.basicTypes;
    using framework.collections;

    public interface NamedMemory {
        PositiveInteger numberOfElements { get; }
        PositiveInteger elementSize { get; }

        T get<T>(PositiveInteger index) where T : struct;
        void set<T>(PositiveInteger index, T data) where T : struct;
        Sequence<T> get<T>(PositiveInteger index, PositiveInteger length) where T : struct, global::framework.basicTypes.BitString;
        void set<T>(PositiveInteger index, Sequence<T> bytes) where T : struct, global::framework.basicTypes.BitString;

    }

}

