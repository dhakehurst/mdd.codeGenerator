/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.collections.test
{
    using framework.basicTypes;
    using framework.collections;
    using NUnit.Framework;

    [TestFixture]
    class StructConvert_Test
    {
        [SetUp]
        public void setUp() {
            this.os = new global::framework.os.dotNet_4_0.OsImpl();
        }

        [TearDown]
        public void tearDown() {
            global::framework.os.OsRef.actualOs = null;
        }


        global::framework.os.dotNet_4_0.OsImpl os;


        struct TestStruct
        {
            public System.Boolean b;
            public System.Int32 i;
            public System.Single f;
        }

        #region SequenceOfBitString8
        [Test]
        public void SequenceOfBitString8()
        {
            TestStruct structure = new TestStruct();
            structure.b = true;
            structure.i = 0x11223344;
            structure.f = 3.141f;

            Sequence<BitString8> seq = os.SequenceOfBitString8(structure);

            Assert.AreEqual(12, seq.size.to_Int32());
        }
        #endregion

        #region Structure
        [Test]
        public void Structure() {
            Sequence<BitString8> seq = os.Sequence( new BitString8[] {
                0x01, 0x00, 0x00, 0x00,
                0x11, 0x22, 0x33, 0x44,
                0xaa, 0xbb, 0xcc, 0xdd
            });

            TestStruct structure = os.Structure<TestStruct>(seq);

            Assert.AreEqual(12, seq.size.to_Int32());
        }
        #endregion
    }
}
