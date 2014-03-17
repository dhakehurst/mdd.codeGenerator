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
    class String_TCtx
    {
        #region Constructors
        [Test]
        public void construct_empty()
        {
            String sut = new String("");
            Assert.IsNotNull(sut);

            Assert.AreEqual("", sut.to_string());
        }

        [Test]
        public void construct_hello()
        {
            String sut = new String("hello");

            Assert.AreEqual("hello", sut.to_string());
        }

        [Test]
        public void construct_With_a_String()
        {
            String a = new String("cheese");
            String b = new String(a);

            Assert.AreEqual("cheese", b.to_string());
        }
        #endregion



        #region length
        [Test]
        public void length_zero()
        {
            String a = new String("");
            PositiveInteger b = a.length();

            Assert.AreEqual(0, b.to_Int32());
        }

        [Test]
        public void length_twenty_nine()
        {
            String a = new String("I am a random string monster!");
            PositiveInteger b = a.length();

            Assert.AreEqual(29, b.to_Int32());
        }
        #endregion

        #region concat

        [Test]
        public void SequenceString_concat() {
           System.Collections.Generic.List<String> sut = new System.Collections.Generic.List<String>() {
               "a", "b", "c", "d"
           };

           String res = sut.concat();

           Assert.AreEqual("abcd", res.to_string());
        }

        #endregion

        #region replaceAll
        [Test]
        public void replaceAll() {
            String sut = "aa.bb.cc.dd.ee";

            String res = sut.replaceAll(".","/");

            Assert.AreEqual("aa/bb/cc/dd/ee", res.to_string());
        }
        #endregion

        [Test]
        public void replaceFirst() {
            String sut = "aa.bb.cc.dd.ee";

            String res = sut.replaceFirst(".", "/");

            Assert.AreEqual("aa/bb.cc.dd.ee", res.to_string());
        }


        [Test]
        public void replaceLast() {
            String sut = "aa.bb.cc.dd.ee";

            String res = sut.replaceLast(".", "/@");

            Assert.AreEqual("aa.bb.cc.dd/@ee", res.to_string());
        }

        #region subString
        [Test]
        public void subString__1_3() {
            String sut = "aa.bb.cc.dd.ee";

            String res = sut.subString(1, 3);

            Assert.AreEqual("aa.", res.to_string());
        }
        [Test]
        public void subString__0_3() {
            String sut = "aa.bb.cc.dd.ee";

            String res = sut.subString(0, 3);

            Assert.AreEqual("aa.", res.to_string());
        }
                [Test]
        public void subString__3_0() {
            String sut = "aa.bb.cc.dd.ee";

            String res = sut.subString(3, 0);

            Assert.AreEqual(".", res.to_string());
        }
        #endregion

        #region indexOf
        [Test]
        public void indexOf() {
            String sut = "aa.bb.cc.dd.ee";
            PositiveInteger i = sut.indexOf("bb");
            Assert.AreEqual(4, i.to_UInt32());
        }
        #endregion

        #region Cloneable
        [Test]
        public void deepClone() {
            String sut = "hello";
            String clone = sut.deepClone();

            Assert.AreEqual(sut, clone);
            Assert.True(sut != clone);
            Assert.True(sut.Equals(clone));
            Assert.True(sut.equalTo(clone));
        }
        #endregion
    }
}
