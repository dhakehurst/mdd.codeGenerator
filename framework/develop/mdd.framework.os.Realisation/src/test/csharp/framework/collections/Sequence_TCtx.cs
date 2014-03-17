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
    class Sequence_Test
    {
        [SetUp]
        public void MyTestInitialize() {
            this.os = new global::framework.os.dotNet_4_0.OsImpl();
        }

        [TearDown]
        public void MyTestCleanup() {
            global::framework.os.OsRef.actualOs = null;
        }


        global::framework.os.dotNet_4_0.OsImpl os;


        #region Constructors
        [Test]
        public void construct_empty()
        {
            Sequence<Boolean> sut = os.Sequence<Boolean>();

            Assert.True(0 == sut.size.to_UInt32());
        }

        [Test]
        public void construct_array_1()
        {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { true });

            Assert.True(1 == sut.size.to_UInt32());
        }

        [Test]
        public void construct_enumerable_1()
        {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new System.Collections.Generic.List<Boolean>() { true });

            Assert.True(1 == sut.size.to_UInt32());
        }
        #endregion

        #region size
        [Test]
        public void size__0()
        {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { true });

            PositiveInteger size = sut.size;

            Assert.True(1 == size.to_UInt32());
        }
        #endregion

        #region deepClone
        [Test]
        public void deepClone__Boolean() {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { true });

            Sequence<Boolean> res = sut.deepClone();

            Assert.True(1 == res.size.to_UInt32());
        }

        [Test]
        public void deepClone__BitString() {
            Sequence<BitString> sut = os.Sequence<BitString>(new BitString[] { new BitString16(0) }).cast<BitString>();

            Sequence<BitString> res = sut.deepClone();

            Assert.True(1 == res.size.to_UInt32());
        }

        [Test]
        public void deepClone__BitString16() {
            Sequence<BitString16> sut = os.Sequence<BitString16>(new BitString16[] { new BitString16(0) });

            Sequence<BitString16> res = sut.deepClone();

            Assert.True(1 == res.size.to_UInt32());
        }

        #endregion

        #region subSequence

        [Test]
        public void subSequence__1_2() {
            Sequence<Integer> sut = os.Sequence<Integer>(new Integer[] { 1,2,3,4,5,6,7,8,9 });

            Sequence<Integer> res = sut.subSequence(1, 2);

            Assert.True(2 == res.size.to_Int32());
            Assert.True(1 == res.at(1).to_Int32());
            Assert.True(2 == res.at(2).to_Int32());
        }


        [Test]
        public void subSequence__5_8() {
            Sequence<Integer> sut = os.Sequence<Integer>(new Integer[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            Sequence<Integer> res = sut.subSequence(5, 8);

            Assert.True(4 == res.size.to_Int32());
            Assert.True(5 == res.at(1).to_Int32());
            Assert.True(6 == res.at(2).to_Int32());
            Assert.True(7 == res.at(3).to_Int32());
            Assert.True(8 == res.at(4).to_Int32());
        }

        [Test]
        public void subSequence__7_9() {
            Sequence<Integer> sut = os.Sequence<Integer>(new Integer[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            Sequence<Integer> res = sut.subSequence(7, 9);

            Assert.True(3 == res.size.to_Int32());
            Assert.True(7 == res.at(1).to_Int32());
            Assert.True(8 == res.at(2).to_Int32());
            Assert.True(9 == res.at(3).to_Int32());
        }

        #endregion


        #region forAll

        [Test]
        public void forAll__empty() {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { });

            Boolean res = sut.forAll(e => e);

            Assert.True(true == res.to_Boolean());

        }

        [Test]
        public void forAll__1_true() {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { true });

            Boolean res = sut.forAll(e => e);

            Assert.True(true == res.to_Boolean());

        }

        [Test]
        public void forAll__1_false() {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { false });

            Boolean res = sut.forAll(e => e);

            Assert.True(false == res.to_Boolean());

        }

        [Test]
        public void forAll__5_true() {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { true, true, true, true, true });

            Boolean res = sut.forAll(e => e);

            Assert.True(true == res.to_Boolean());

        }

        [Test]
        public void forAll__5_false() {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { false, false, false, false, false });

            Boolean res = sut.forAll(e => e);

            Assert.True(false == res.to_Boolean());

        }

        [Test]
        public void forAll__1_false_4_true() {
            Sequence<Boolean> sut = os.Sequence<Boolean>(new Boolean[] { true, true, true, false, true });

            Boolean res = sut.forAll(e => e);

            Assert.True(false == res.to_Boolean());

        }

        #endregion

        #region transitiveClosure
        class Node {
            framework.os.OsRef os;
            public Node(Node parent, String name) {
                this.os = new framework.os.OsRef();
                this.name = name;
                this._parent = parent;
                this._child = os.Sequence<Node>();
            }
            String name;

            Node _parent;
            public Node parent {
                get { return _parent; }
            }

            Sequence<Node> _child;
            public Sequence<Node> child {
                get { return _child; }
                set { this._child = value; }
            }

            public override string ToString() {
                return name.to_string();
            }
        }
        [Test]
        public void transitiveClosure_empty() {
            Sequence<Node> sut = os.Sequence<Node>(new Node[] { });

            Sequence<Node> decendents = sut.transitiveClosure(e => e.child);

            Assert.True(true == decendents.isEmpty);
        }

        [Test]
        public void transitiveClosure_0levels() {
            Node root = new Node(null, "root");

            Sequence<Node> sut = os.Sequence<Node>(new Node[] { root });

            Sequence<Node> decendents = sut.transitiveClosure(e => e.child);

            Assert.True(1 == decendents.size.to_Int32());
            Assert.True(root == decendents.first());
        }

        [Test]
        public void transitiveClosure_1level() {
            Node root = new Node(null, "root");
            root.child = root.child.append(new Node(root, "l1.1"));
            root.child = root.child.append(new Node(root, "l1.2"));

            Sequence<Node> sut = os.Sequence<Node>(new Node[] { root });

            Sequence<Node> decendents = sut.transitiveClosure(e => e.child);

            Assert.True(3 == decendents.size.to_Int32());
            Assert.True(root == decendents.first());
        }
        #endregion

        #region setRange
        [Test]
        public void setRange() {
            Sequence<Integer> sut = os.Sequence<Integer>(new Integer[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            Sequence<Integer> seq = os.Sequence<Integer>(new Integer[] { 11, 12, 13 });

            sut.setRange(2, seq);

            Assert.AreEqual(seq.at(1), sut.at(2));
            Assert.AreEqual(seq.at(2), sut.at(3));
            Assert.AreEqual(seq.at(3), sut.at(4));
        }
        #endregion
    }
}
