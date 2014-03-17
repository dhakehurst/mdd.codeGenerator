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
    using framework.math;
    using NUnit.Framework;

    [TestFixture]
    class AngleDegrees_TCtx
    {
        #region Constructors
        [Test]
        public void construct_0p0() 
        {
            AngleDegrees sut = new AngleDegrees(0.0);

            Assert.AreEqual(0.0, sut.to_Double());
        }

        [Test]
        public void construct_Positive()
        {
            AngleDegrees a = new AngleDegrees(583.284);

            Assert.AreEqual(583.284, a.to_Double());
        }

        [Test]
        public void construct_Negative()
        {
            AngleDegrees a = new AngleDegrees(-726.148);

            Assert.AreEqual(-726.148, a.to_Double());
        }

        [Test]
        public void construct_With_Another_Real()
        {
            Real a = new Real(1122.3344);
            AngleDegrees b = new AngleDegrees(a);

            Assert.AreEqual(1122.3344, b.to_Double());
        }

        [Test]
        public void construct_With_Integer()
        {
            Integer a = new Integer(1234);
            AngleDegrees b = new AngleDegrees(a);

            Assert.AreEqual(1234d, b.to_Double());
        }
        #endregion

        #region arcsin
        [Test]
        public void arcsin_0p1() {
            Real sut = new Real(0.5);

            Angle res = sut.arcsin();
            Integer v = res.degrees.truncate();

            Assert.AreEqual(30, v.to_Int32());
        }
        #endregion

    }
}
