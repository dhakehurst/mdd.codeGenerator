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
    using System.Collections.Generic;
    using System.Linq;
    using framework.collections;
    
    public static class EnumExtensions
    {
        public static Sequence<BitString8> asSequenceOfBitString8(this System.Enum self) {
            return new String(self.ToString()).asSequenceOfBitString8();
        }
        public static Sequence<BitString16> asSequenceOfBitString16(this System.Enum self) {
            return new String(self.ToString()).asSequenceOfBitString16();
        }
        public static Sequence<BitString32> asSequenceOfBitString32(this System.Enum self) {
            return new String(self.ToString()).asSequenceOfBitString32();
        }
        public static Sequence<BitString64> asSequenceOfBitString64(this System.Enum self) {
            return new String(self.ToString()).asSequenceOfBitString64();
        }


        public static E? asEnum<E>(this Sequence<BitString8> self) where E : struct {
            return self.asString().asEnum<E>();
        }
        public static E? asEnum<E>(this Sequence<BitString16> self) where E : struct {
            return self.asString().asEnum<E>();
        }
        public static E? asEnum<E>(this Sequence<BitString32> self) where E : struct {
            return self.asString().asEnum<E>();
        }
        public static E? asEnum<E>(this Sequence<BitString64> self) where E : struct {
            return self.asString().asEnum<E>();
        }
    }
}
