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
    class PositiveInteger_TCtx
    {
        #region Constructor
        [Test]
        public void construct_0() 
        {
            PositiveInteger sut = new PositiveInteger(0);

            Assert.AreEqual(0, sut.to_UInt32());
        }

        [Test]
        public void construct_Positive()
        {
            PositiveInteger a = new PositiveInteger(50000);

            Assert.AreEqual(50000, a.to_Int32());
        }

        [Test]
        public void construct_With_A_PositiveInteger()
        {
            PositiveInteger a = new PositiveInteger(12);
            PositiveInteger b = new PositiveInteger(a);

            Assert.AreEqual(12, a.to_Int32());
        }
        #endregion

        #region Cloneable
        [Test]
        public void deepClone() {
            PositiveInteger sut = 0;
            PositiveInteger clone = sut.deepClone();

            Assert.AreEqual(sut, clone);
            Assert.True(sut != clone);
            Assert.True(sut.Equals(clone));
            Assert.True(sut.equalTo(clone));
        }
        #endregion
    }
}
