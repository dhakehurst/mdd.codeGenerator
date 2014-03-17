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
    class DateTime_TCtx
    {
        #region Constructors
        [Test]
        public void construct_2000_1_1() 
        {
            DateTime sut = new DateTime(new System.DateTime(2000,1,1));

            Assert.AreEqual(new System.DateTime(2000, 1, 1), sut.to_Date_Time());
        }

        [Test]
        public void construct_With_A_DateTime()
        {
            DateTime a = new DateTime(new System.DateTime(2012, 5, 13));
            DateTime b = new DateTime(a);

            Assert.AreEqual(new System.DateTime(2012, 5, 13), a.to_Date_Time());
        }
        #endregion

        #region Operations

        #region Now
        [Test]
        public void now()
        {
            DateTime a = new DateTime(new System.DateTime(2012, 5, 13));
            System.DateTime b = System.DateTime.Now;

            Assert.AreEqual(System.DateTime.Now, a.now().to_Date_Time());
        }
        #endregion

        #endregion

        #region Base Language Converters

        #region to_Date_Time
        [Test]
        public void to_Date_Time()
        {
            DateTime a = new DateTime(new System.DateTime(2012, 5, 13));
            DateTime b = new DateTime(a);

            Assert.AreEqual(new System.DateTime(2012, 5, 13), a.to_Date_Time());
        }
        #endregion

        #endregion
    }
}
