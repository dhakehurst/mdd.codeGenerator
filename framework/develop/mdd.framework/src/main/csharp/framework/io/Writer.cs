/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.io {

    using global::framework.basicTypes;
    using global::framework.collections;

	public interface Writer {

        void write(global::framework.basicTypes.String txt);
        void writeLine(global::framework.basicTypes.String txt);
        void write(Sequence<BitString> bits);

    }

} //namespace framework.io
