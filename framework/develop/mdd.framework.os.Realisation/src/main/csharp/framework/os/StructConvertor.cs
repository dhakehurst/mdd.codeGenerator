/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.os {
    using framework.basicTypes;
    using framework.collections;
    using System.Runtime.InteropServices;

	static class StructConvertor
	{
        public static Sequence<BitString8> asSequenceOfBitString8<T>(this T self) where T : struct {
            int size = Marshal.SizeOf(self);
            BitString8[] array = new BitString8[size];

            GCHandle arrayHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            
            System.IntPtr arraytPtr = arrayHandle.AddrOfPinnedObject();

            Marshal.StructureToPtr(self,arraytPtr,true);

            return new os.collections.SequenceOnArray<BitString8>(array);
        }

        public static T asStructure<T>(this Sequence<BitString8> self) where T : struct {
            int size = Marshal.SizeOf(typeof(T));

            GCHandle arrayHandle = GCHandle.Alloc(self.array, GCHandleType.Pinned);

            System.IntPtr arraytPtr = arrayHandle.AddrOfPinnedObject();

            T structure = (T) Marshal.PtrToStructure(arraytPtr, typeof(T));

            return structure;
        }
	}
}
