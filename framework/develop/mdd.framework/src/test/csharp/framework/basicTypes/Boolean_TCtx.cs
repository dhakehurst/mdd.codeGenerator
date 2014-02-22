/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.basicTypeTests
{
    using framework.basicTypes;
    using NUnit.Framework;

    [TestFixture]
    class Boolean_TCtx
    {
        #region Constructors
        [Test]
        public void construct_false() 
        {
            Boolean s = new Boolean(false);

            Assert.AreEqual(false, s.to_Boolean());
        }

        [Test]
        public void construct_true()
        {
            Boolean a = new Boolean(true);

            Assert.AreEqual(true, a.to_Boolean());
        }

        [Test]
        public void construct_SystemBoolean_false()
        {
            System.Boolean a = false;
            Boolean b = new Boolean(a);

            Assert.AreEqual(false, b.to_Boolean());
        }

        [Test]
        public void construct_SystemBoolean_true()
        {
            System.Boolean a = true;
            Boolean b = new Boolean(a);

            Assert.AreEqual(true, b.to_Boolean());
        }
        #endregion

        #region Operations

        #region Not
        [Test]
        public void not_True()
        {
            Boolean a = new Boolean(true);
            Boolean b = a.not();

            Assert.AreEqual(false, b.to_Boolean());
        }

        [Test]
        public void not_False()
        {
            Boolean a = new Boolean(false);
            Boolean b = a.not();

            Assert.AreEqual(true, b.to_Boolean());
        }
        #endregion

        #region Or
        [Test]
        public void or_False_and_False()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(false);
            Boolean c = a.or(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void or_Fasle_and_True()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(true);
            Boolean c = a.or(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void or_True_and_False()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(false);
            Boolean c = a.or(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void or_True_and_True()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(true);
            Boolean c = a.or(b);

            Assert.AreEqual(true, c.to_Boolean());
        }
        #endregion

        #region XOr
        [Test]
        public void xor_False_and_False()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(false);
            Boolean c = a.xor(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void xor_Fasle_and_True()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(true);
            Boolean c = a.xor(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void xor_True_and_False()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(false);
            Boolean c = a.xor(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void xor_True_and_True()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(true);
            Boolean c = a.xor(b);

            Assert.AreEqual(false, c.to_Boolean());
        }
        #endregion

        #region And
        [Test]
        public void and_False_and_False()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(false);
            Boolean c = a.and(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void and_Fasle_and_True()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(true);
            Boolean c = a.and(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void and_True_and_False()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(false);
            Boolean c = a.and(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void and_True_and_True()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(true);
            Boolean c = a.and(b);

            Assert.AreEqual(true, c.to_Boolean());
        }
        #endregion

        #region Implies
        [Test]
        public void implies_False_and_False()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(false);
            Boolean c = a.implies(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void implies_Fasle_and_True()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(true);
            Boolean c = a.implies(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void implies_True_and_False()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(false);
            Boolean c = a.implies(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void implies_True_and_True()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(true);
            Boolean c = a.implies(b);

            Assert.AreEqual(true, c.to_Boolean());
        }
        #endregion

        #endregion

        #region Comparion

        #region equalTo
        [Test]
        public void equalTo_False_and_False()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(false);
            Boolean c = a.equalTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void equalTo_Fasle_and_True()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(true);
            Boolean c = a.equalTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void equalTo_True_and_False()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(false);
            Boolean c = a.equalTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void equalTo_True_and_True()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(true);
            Boolean c = a.equalTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }
        #endregion

        #region notEqualTo
        [Test]
        public void notEqualTo_False_and_False()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(false);
            Boolean c = a.notEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void notEqualTo_Fasle_and_True()
        {
            Boolean a = new Boolean(false);
            Boolean b = new Boolean(true);
            Boolean c = a.notEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void notEqualTo_True_and_False()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(false);
            Boolean c = a.notEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void notEqualTo_True_and_True()
        {
            Boolean a = new Boolean(true);
            Boolean b = new Boolean(true);
            Boolean c = a.notEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }
        #endregion

        #endregion

        #region Cloneable
        [Test]
        public void deepClone() {
            Boolean sut = true;
            Boolean clone = sut.deepClone();

            Assert.AreEqual(sut, clone);
            Assert.True(sut != clone);
            Assert.True(sut.Equals(clone));
            Assert.True(sut.equalTo(clone));
        }
        #endregion

        #region Framework Converters

        #region asString
        [Test]
        public void asString_true()
        {
            Boolean a = new Boolean(true);
            String b = a.asString();

            Assert.AreEqual("True", b.to_string());
        }

        [Test]
        public void asString_false()
        {
            Boolean a = new Boolean(false);
            String b = a.asString();

            Assert.AreEqual("False", b.to_string());
        }
        #endregion

        #region asInteger
        [Test]
        public void toInteger_true()
        {
            Boolean a = new Boolean(true);
            Integer b = a.asInteger();

            Assert.AreEqual(1, b.to_Int32());
        }

        [Test]
        public void toInteger_false()
        {
            Boolean a = new Boolean(false);
            Integer b = a.asInteger();

            Assert.AreEqual(0, b.to_Int32());
        }
        #endregion

        #region toPositiveInteger
        [Test]
        public void toPositiveInteger_true()
        {
            Boolean a = new Boolean(true);
            PositiveInteger b = a.asPositiveInteger();

            Assert.AreEqual(1, b.to_Int32());
        }

        [Test]
        public void toPositiveInteger_false()
        {
            Boolean a = new Boolean(false);
            PositiveInteger b = a.asPositiveInteger();

            Assert.AreEqual(0, b.to_Int32());
        }
        #endregion

        #endregion

        #region Base Language Converters

        #region to_Boolean
        [Test]
        public void to_Boolean_true()
        {
            Boolean a = new Boolean(true);
            System.Boolean b = a.to_Boolean();

            Assert.AreEqual(true, b);
        }

        [Test]
        public void to_Boolean_false()
        {
            Boolean a = new Boolean(false);
            System.Boolean b = a.to_Boolean();

            Assert.AreEqual(false, b);
        }
        #endregion

        #region to_Byte
        [Test]
        public void to_Byte_true()
        {
            Boolean a = new Boolean(true);
            System.Byte b = a.to_Byte();

            Assert.AreEqual(1, b);
        }

        [Test]
        public void to_Byte_false()
        {
            Boolean a = new Boolean(false);
            System.Byte b = a.to_Byte();

            Assert.AreEqual(0, b);
        }
        #endregion

        #region to_Int32
        [Test]
        public void to_Int32_true()
        {
            Boolean a = new Boolean(true);
            System.Int32 b = a.to_Int32();

            Assert.AreEqual(1, b);
        }

        [Test]
        public void to_Int32_false()
        {
            Boolean a = new Boolean(false);
            System.Int32 b = a.to_Int32();

            Assert.AreEqual(0, b);
        }
        #endregion

        #endregion
    }
}
