/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.os.interprocess {

    using global::framework.basicTypes;
    using framework.collections;
    using global::framework.os.interprocess;
    using System.Linq;

    public class NamedMemoryImpl : global::framework.os.interprocess.NamedMemory {

        public NamedMemoryImpl(NamedItemIdentifier identity, PositiveInteger numberOfElements, PositiveInteger elementSize) {
            this.identity = identity;
            this._numberOfElements = numberOfElements;
            this._elementSize = elementSize;
            long numBytes = (numberOfElements * elementSize).to_Int64();
            this.mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateOrOpen(
                identity.to_string(),
                numBytes,
                System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite
            );
            this.mem = mmf.CreateViewAccessor();
            this.maxIndex = (numberOfElements * (elementSize / 8)).asInteger().to_Int32();
        }
        int maxIndex;
        NamedItemIdentifier identity;
        PositiveInteger _numberOfElements;
        public PositiveInteger numberOfElements { get { return _numberOfElements; }  }

        PositiveInteger _elementSize;
        public PositiveInteger elementSize { get { return _elementSize; } }

        System.IO.MemoryMappedFiles.MemoryMappedFile mmf;
        System.IO.MemoryMappedFiles.MemoryMappedViewAccessor mem;

        public T get<T>(PositiveInteger index) where T : struct {
            T t;
            this.mem.Read(index.to_Int32(), out t);
            return t;
        }

        public global::framework.collections.Sequence<T> get<T>(PositiveInteger index, PositiveInteger length)
            where T : struct, BitString
        {
            if (this.maxIndex < index.to_Int32() + (length * (this.elementSize / 8))) {
                throw new InterprocessException("NamedMemory.get: Number of elements to get must == " + this.numberOfElements);
            }
            T[] data = new T[length.to_Int32()];
            this.mem.ReadArray(index.to_Int32(), data, 0, data.Length);
            return new framework.os.collections.SequenceOnArray<T>(data);
        }

        public void set<T>(PositiveInteger index, T data) where T : struct {
            int dataSize = System.Runtime.InteropServices.Marshal.SizeOf(data);
            if (this.maxIndex < dataSize) {
                throw new InterprocessException("NamedMemory.set: Number of bytes to set must <= " + this.maxIndex);
            } else {
                this.mem.Write(index.to_Int32(), ref data);
            }
        }

        public void set<T>(PositiveInteger index, Sequence<T> bytes) where T : struct, BitString {
            if (this.maxIndex < index.to_Int32() + (bytes.size*(this.elementSize/8))) {
                throw new InterprocessException("NamedMemory.set: Number of bytes to set must <= " + this.maxIndex);
            } else {
                this.mem.WriteArray(index.to_Int32(), bytes.array, 0, bytes.size.to_Int32());
            }
        }

    }
}
