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
    class Integer_TCtx
    {
        #region Constructors
        [Test]
        public void construct_0()
        {
            Integer sut = new Integer(0);

            Assert.AreEqual(0, sut.to_Int32());
        }

        [Test]
        public void construct_Negative()
        {
            Integer i = new Integer(-5);

            Assert.AreEqual(-5, i.to_Int32());
        }

        [Test]
        public void construct_Positive()
        {
            Integer i = new Integer(5000);

            Assert.AreEqual(5000, i.to_Int32());
        }

        [Test]
        public void construct_With_Another_Integer()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(a);

            Assert.AreEqual(5, b.to_Int32());
        }
        #endregion

        #region Operations

        #region Negate
        [Test]
        public void negate_Positive_to_Negative()
        {
            Integer a = new Integer(5);
            Integer b = a.negate();

            Assert.AreEqual(-5, b.to_Int32());
        }

        [Test]
        public void negate_Negative_to_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = a.negate();

            Assert.AreEqual(5, b.to_Int32());
        }
        #endregion

        #region Absolute
        [Test]
        public void absolute_Positive()
        {
            Integer a = new Integer(5);
            PositiveInteger b = a.absolute();

            Assert.AreEqual(5, b.to_Int32());
        }

        [Test]
        public void absolute_Negative()
        {
            Integer a = new Integer(-5);
            PositiveInteger b = a.absolute();

            Assert.AreEqual(5, b.to_Int32());
        }

        [Test]
        public void absolute_Zero()
        {
            Integer a = new Integer(0);
            PositiveInteger b = a.absolute();

            Assert.AreEqual(0, b.to_Int32());
        }
        #endregion

        #region Plus
        [Test]
        public void plus_Positive_to_Negative()
        {
            Integer a = new Integer(15);
            Integer b = new Integer(-30);
            Integer c = a.plus(b);

            Assert.AreEqual(-15, c.to_Int32());
        }

        [Test]
        public void plus_Negative_to_Positive()
        {
            Integer a = new Integer(-15);
            Integer b = new Integer(30);
            Integer c = a.plus(b);

            Assert.AreEqual(15, c.to_Int32());
        }

        [Test]
        public void plus_Positive_add_Positive()
        {
            Integer a = new Integer(15);
            Integer b = new Integer(30);
            Integer c = a.plus(b);

            Assert.AreEqual(45, c.to_Int32());
        }

        [Test]
        public void plus_Negative_add_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(10);
            Integer c = a.plus(b);

            Assert.AreEqual(5, c.to_Int32());
        }

        [Test]
        public void plus_Positive_add_Negative()
        {
            Integer a = new Integer(50);
            Integer b = new Integer(-5);
            Integer c = a.plus(b);

            Assert.AreEqual(45, c.to_Int32());
        }

        [Test]
        public void plus_Negative_add_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-10);
            Integer c = a.plus(b);

            Assert.AreEqual(-15, c.to_Int32());
        }

        [Test]
        public void plus_Integer_plus_Positive_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(10d);
            Real c = a.plus(b);

            Assert.AreEqual(15d, c.to_Double());
        }

        [Test]
        public void plus_Integer_plus_Negative_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(-10d);
            Real c = a.plus(b);

            Assert.AreEqual(-5d, c.to_Double());
        }

        [Test]
        public void plus_Integer_plus_Zero_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(0d);
            Real c = a.plus(b);

            Assert.AreEqual(5d, c.to_Double());
        }
        #endregion

        #region Minus
        [Test]
        public void minus_Negative_to_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-10);
            Integer c = a.minus(b);

            Assert.AreEqual(5, c.to_Int32());
        }

        [Test]
        public void minus_Positive_to_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(10);
            Integer c = a.minus(b);

            Assert.AreEqual(-5, c.to_Int32());
        }

        [Test]
        public void minus_Positive_subtract_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(5);
            Integer c = a.minus(b);

            Assert.AreEqual(0, c.to_Int32());
        }

        [Test]
        public void minus_Positive_subtract_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-5);
            Integer c = a.minus(b);

            Assert.AreEqual(10, c.to_Int32());
        }

        [Test]
        public void minus_Negative_subtract_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(5);
            Integer c = a.minus(b);

            Assert.AreEqual(-10, c.to_Int32());
        }

        [Test]
        public void minus_Negative_subtract_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-5);
            Integer c = a.minus(b);

            Assert.AreEqual(0, c.to_Int32());
        }

        [Test]
        public void minus_Integer_subtract_Positive_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(10d);
            Real c = a.minus(b);

            Assert.AreEqual(-5d, c.to_Double());
        }

        [Test]
        public void minus_Integer_subtract_Negative_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(-10d);
            Real c = a.minus(b);

            Assert.AreEqual(15d, c.to_Double());
        }

        [Test]
        public void minus_Integer_subtract_Zero_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(0d);
            Real c = a.minus(b);

            Assert.AreEqual(5d, c.to_Double());
        }
        #endregion

        #region multiply
        [Test]
        public void multiply_Negative_to_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-10);
            Integer c = a.multiply(b);

            Assert.AreEqual(50, c.to_Int32());
        }

        [Test]
        public void multiply_Positive_to_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-10);
            Integer c = a.multiply(b);

            Assert.AreEqual(-50, c.to_Int32());
        }

        [Test]
        public void multiply_Positive_times_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(8);
            Integer c = a.multiply(b);

            Assert.AreEqual(40, c.to_Int32());
        }

        [Test]
        public void multiply_Positive_times_Negative()
        {
            Integer a = new Integer(1);
            Integer b = new Integer(-20);
            Integer c = a.multiply(b);

            Assert.AreEqual(-20, c.to_Int32());
        }

        [Test]
        public void multiply_Negative_times_Negative()
        {
            Integer a = new Integer(-7);
            Integer b = new Integer(-3);
            Integer c = a.multiply(b);

            Assert.AreEqual(21, c.to_Int32());
        }

        [Test]
        public void multiply_Negative_times_Positive()
        {
            Integer a = new Integer(-3);
            Integer b = new Integer(4);
            Integer c = a.multiply(b);

            Assert.AreEqual(-12, c.to_Int32());
        }

        [Test]
        public void multiply_Integer_times_Positive_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(10d);
            Real c = a.multiply(b);

            Assert.AreEqual(50d, c.to_Double());
        }

        [Test]
        public void multiply_Integer_times_Negative_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(-10d);
            Real c = a.multiply(b);

            Assert.AreEqual(-50d, c.to_Double());
        }

        [Test]
        public void multiply_Integer_times_Zero_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(0d);
            Real c = a.multiply(b);

            Assert.AreEqual(0d, c.to_Double());
        }
        #endregion

        #region Divide
        [Test]
        public void divide_byZero()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(0);

            try
            {
                Integer c = a.divide(b);
                Assert.Fail();
            }
            catch (System.DivideByZeroException)
            {
                //Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void divide_Positive_to_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-4);
            Integer c = a.divide(b);

            Assert.AreEqual(-1, c.to_Int32());
        }

        [Test]
        public void divide_Negative_to_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-4);
            Integer c = a.divide(b);

            Assert.AreEqual(1, c.to_Int32());
        }

        [Test]
        public void divide_Positive_over_Positive()
        {
            Integer a = new Integer(10);
            Integer b = new Integer(5);
            Integer c = a.divide(b);

            Assert.AreEqual(2, c.to_Int32());
        }

        [Test]
        public void divide_Positive_over_Negative()
        {
            Integer a = new Integer(15);
            Integer b = new Integer(-3);
            Integer c = a.divide(b);

            Assert.AreEqual(-5, c.to_Int32());
        }

        [Test]
        public void divide_Negative_over_Negative()
        {
            Integer a = new Integer(-10);
            Integer b = new Integer(-10);
            Integer c = a.divide(b);

            Assert.AreEqual(1, c.to_Int32());
        }

        [Test]
        public void divide_Negative_over_Positive()
        {
            Integer a = new Integer(-8);
            Integer b = new Integer(2);
            Integer c = a.divide(b);

            Assert.AreEqual(-4, c.to_Int32());
        }

        [Test]
        public void divide_Integer_over_Positive_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(10d);
            Real c = a.divide(b);

            Assert.AreEqual(0.5, c.to_Double());
        }

        [Test]
        public void divide_Integer_over_Negative_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(-10d);
            Real c = a.divide(b);

            Assert.AreEqual(-0.5, c.to_Double());
        }

        [Test]
        public void divide_Integer_over_Zero_Real()
        {
            Integer a = new Integer(5);
            Real b = new Real(0d);
            Real c = a.divide(b);

            Assert.AreEqual(System.Double.PositiveInfinity, c.to_Double());
        }
        #endregion

        #region Modulo
        [Test]
        public void modulo_No_Remainder()
        {
            Integer a = new Integer(10);
            Integer b = new Integer(5);
            Integer c = a.modulo(b);

            Assert.AreEqual(0, c.to_Int32());
        }

        [Test]
        public void modulo_Remainder()
        {
            Integer a = new Integer(8);
            Integer b = new Integer(5);
            Integer c = a.modulo(b);

            Assert.AreEqual(3, c.to_Int32());
        }

        [Test]
        public void modulo_Positive()
        {
            Integer a = new Integer(29);
            Integer b = new Integer(7);
            Integer c = a.modulo(b);

            Assert.AreEqual(1, c.to_Int32());
        }

        [Test]
        public void modulo_Negative()
        {
            Integer a = new Integer(-8);
            Integer b = new Integer(3);
            Integer c = a.modulo(b);

            Assert.AreEqual(-2, c.to_Int32());
        }

        [Test]
        public void modulo_Integer_modulo_Real()
        {
            Integer a = new Integer(12);
            Real b = new Real(10d);
            Real c = a.modulo(b);

            Assert.AreEqual(2d, c.to_Double());
        }
        #endregion

        #region Max
        [Test]
        public void max_Positive_max_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(4);
            Integer c = a.max(b);

            Assert.AreEqual(5, c.to_Int32());
        }

        [Test]
        public void max_Positive_max_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-4);
            Integer c = a.max(b);

            Assert.AreEqual(5, c.to_Int32());
        }

        [Test]
        public void max_Negative_max_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(4);
            Integer c = a.max(b);

            Assert.AreEqual(4, c.to_Int32());
        }

        [Test]
        public void max_Negative_max_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-4);
            Integer c = a.max(b);

            Assert.AreEqual(-4, c.to_Int32());
        }

        [Test]
        public void max_Integer_max_BiggerReal()
        {
            Integer a = new Integer(5);
            Real b = new Real(6d);
            Real c = a.max(b);

            Assert.AreEqual(6d, c.to_Double());
        }

        [Test]
        public void max_Integer_max_SmallerReal()
        {
            Integer a = new Integer(7);
            Real b = new Real(6d);
            Real c = a.max(b);

            Assert.AreEqual(7d, c.to_Double());
        }
        #endregion

        #region Min
        [Test]
        public void min_Positive_min_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(4);
            Integer c = a.min(b);

            Assert.AreEqual(4, c.to_Int32());
        }

        [Test]
        public void min_Positive_min_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-4);
            Integer c = a.min(b);

            Assert.AreEqual(-4, c.to_Int32());
        }

        [Test]
        public void min_Negative_min_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(4);
            Integer c = a.min(b);

            Assert.AreEqual(-5, c.to_Int32());
        }

        [Test]
        public void min_Negative_min_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-4);
            Integer c = a.min(b);

            Assert.AreEqual(-5, c.to_Int32());
        }

        [Test]
        public void min_Integer_min_BiggerReal()
        {
            Integer a = new Integer(5);
            Real b = new Real(6d);
            Real c = a.min(b);

            Assert.AreEqual(5d, c.to_Double());
        }

        [Test]
        public void min_Integer_min_SmallerReal()
        {
            Integer a = new Integer(7);
            Real b = new Real(6d);
            Real c = a.min(b);

            Assert.AreEqual(6d, c.to_Double());
        }
        #endregion

        #endregion

        #region Comparisons

        #region equalTo
        [Test]
        public void equalTo_isEqual()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(5);
            Boolean isEqual = a.equalTo(b);

            Assert.AreEqual(true, isEqual.to_Boolean());
        }

        [Test]
        public void equalTo_isntEqual()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(6);
            Boolean isEqual = a.equalTo(b);

            Assert.AreEqual(false, isEqual.to_Boolean());
        }

        [Test]
        public void equalTo_Negative_equalTo_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(5);
            Boolean isEqual = a.equalTo(b);

            Assert.AreEqual(false, isEqual.to_Boolean());
        }

        [Test]
        public void equalTo_Positive_equalTo_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-5);
            Boolean isEqual = a.equalTo(b);

            Assert.AreEqual(false, isEqual.to_Boolean());
        }
        #endregion

        #region notEqualTo
        [Test]
        public void notEqualTo_isEqual()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(6);
            Boolean isEqual = a.notEqualTo(b);

            Assert.AreEqual(true, isEqual.to_Boolean());
        }

        [Test]
        public void notEqualTo_isntEqual()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(5);
            Boolean isEqual = a.notEqualTo(b);

            Assert.AreEqual(false, isEqual.to_Boolean());
        }

        [Test]
        public void notEqualTo_Negative_equalTo_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(5);
            Boolean isEqual = a.notEqualTo(b);

            Assert.AreEqual(true, isEqual.to_Boolean());
        }

        [Test]
        public void notEqualTo_Positive_equalTo_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-5);
            Boolean isEqual = a.notEqualTo(b);

            Assert.AreEqual(true, isEqual.to_Boolean());
        }
        #endregion

        #region greaterThan
        [Test]
        public void greaterThan_Positive_biggerThan_Positive()
        {
            Integer a = new Integer(7);
            Integer b = new Integer(6);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Positive_equalTo_Positive()
        {
            Integer a = new Integer(6);
            Integer b = new Integer(6);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Positive_biggerThan_Negative()
        {
            Integer a = new Integer(7);
            Integer b = new Integer(-6);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Negative_biggerThan_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-6);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Positive_smallerThan_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(6);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Negative_smallerThan_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(6);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Negative_smallerThan_Negative()
        {
            Integer a = new Integer(-7);
            Integer b = new Integer(-6);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Negative_equalTo_Negative()
        {
            Integer a = new Integer(-6);
            Integer b = new Integer(-6);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }
        #endregion

        #region greaterThanOrEqualTo
        [Test]
        public void greaterThanOrEqualTo_Positive_biggerThan_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(4);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Positive_equalTo_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(5);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Positive_smallerThan_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(6);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Negative_biggerThan_Negative()
        {
            Integer a = new Integer(-3);
            Integer b = new Integer(-4);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Negative_equalTo_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-5);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Negative_smallerThan_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-4);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Positive_biggerThan_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-4);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Negative_smallerThan_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(4);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }
        #endregion

        #region lessThan
        [Test]
        public void lessThan_Positive_biggerThan_Positive()
        {
            Integer a = new Integer(7);
            Integer b = new Integer(6);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThan_Positive_equalTo_Positive()
        {
            Integer a = new Integer(6);
            Integer b = new Integer(6);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThan_Positive_biggerThan_Negative()
        {
            Integer a = new Integer(7);
            Integer b = new Integer(-6);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThan_Negative_biggerThan_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-6);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThan_Positive_smallerThan_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(6);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThan_Negative_smallerThan_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(6);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThan_Negative_smallerThan_Negative()
        {
            Integer a = new Integer(-7);
            Integer b = new Integer(-6);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThan_Negative_equalTo_Negative()
        {
            Integer a = new Integer(-6);
            Integer b = new Integer(-6);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }
        #endregion

        #region lessThanOrEqualTo
        [Test]
        public void lessThanOrEqualTo_Positive_biggerThan_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(4);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Positive_equalTo_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(5);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Positive_smallerThan_Positive()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(6);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Negative_biggerThan_Negative()
        {
            Integer a = new Integer(-3);
            Integer b = new Integer(-4);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Negative_equalTo_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-5);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Negative_smallerThan_Negative()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(-4);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Positive_biggerThan_Negative()
        {
            Integer a = new Integer(5);
            Integer b = new Integer(-4);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Negative_smallerThan_Positive()
        {
            Integer a = new Integer(-5);
            Integer b = new Integer(4);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }
        #endregion

        #endregion

        #region Cloneable
        [Test]
        public void deepClone() {
            Integer sut = 0;
            Integer clone = sut.deepClone();

            Assert.AreEqual(sut, clone);
            Assert.True(sut != clone);
            Assert.True(sut.Equals(clone));
            Assert.True(sut.equalTo(clone));
        }
        #endregion

        #region Framework Converters

        #region asBitString
        [Test]
        public void asBitString32_0()
        {
            Integer a = new Integer(0);
            BitString bs = a.asBitString32();

            for (uint i = 1; i <= 64; ++i) {
                Assert.AreEqual(false, bs.at(i).to_Boolean());
            }
        }

        [Test]
        public void asBitString32_p1()
        {
            Integer a = new Integer(1);
            BitString bs = a.asBitString32();

            Assert.AreEqual(true, bs.at(1).to_Boolean());
            for (uint i = 2; i <= 64; ++i) {
                Assert.AreEqual(false, bs.at(i).to_Boolean());
            }
        }

        [Test]
        public void asBitString32_m1()
        {
            Integer a = new Integer(-1);
            BitString bs = a.asBitString32();

            for (uint i = 1; i <= 32; ++i) {
                Assert.AreEqual(true, bs.at(i).to_Boolean());
            }
        }
        #endregion

        #region asBoolean
        [Test]
        public void asBoolean_False()
        {
            Integer a = new Integer(0);
            Boolean b = a.asBoolean();

            Assert.AreEqual(false, b.to_Boolean());
        }

        [Test]
        public void asBoolean_True_Positive()
        {
            Integer a = new Integer(10);
            Boolean b = a.asBoolean();

            Assert.AreEqual(true, b.to_Boolean());
        }

        [Test]
        public void asBoolean_True_Negative()
        {
            Integer a = new Integer(-10);
            Boolean b = a.asBoolean();

            Assert.AreEqual(false, b.to_Boolean());
        }
        #endregion

        #region asReal
        [Test]
        public void asReal_Positive()
        {
            Integer a = new Integer(10);
            Real b = a.asReal();

            Assert.AreEqual(10d, b.to_Double());
        }

        [Test]
        public void asReal_Negative()
        {
            Integer a = new Integer(-10);
            Real b = a.asReal();

            Assert.AreEqual(-10d, b.to_Double());
        }

        [Test]
        public void asReal_Zero()
        {
            Integer a = new Integer(0);
            Real b = a.asReal();

            Assert.AreEqual(0d, b.to_Double());
        }
        #endregion

        #region asInteger
        [Test]
        public void asInteger_Positive()
        {
            Integer a = new Integer(10);
            Integer b = a.asInteger();

            Assert.AreEqual(10, b.to_Int32());
        }

        [Test]
        public void asInteger_Negative()
        {
            Integer a = new Integer(-10);
            Integer b = a.asInteger();

            Assert.AreEqual(-10, b.to_Int32());
        }

        [Test]
        public void asInteger_Zero()
        {
            Integer a = new Integer(0);
            Integer b = a.asInteger();

            Assert.AreEqual(0, b.to_Int32());
        }
        #endregion
        
        #endregion

        #region Basic Language Converters

        #region to_Int64
        [Test]
        public void to_Int64_Positive()
        {
            Integer a = new Integer(2147483647);
            System.Int64 b = a.to_Int64();

            Assert.AreEqual(2147483647, b);
        }

        [Test]
        public void to_Int64_Negative()
        {
            Integer a = new Integer(-2147483648);
            System.Int64 b = a.to_Int64();

            Assert.AreEqual(-2147483648, b);
        }

        [Test]
        public void to_Int64_Zero()
        {
            Integer a = new Integer(0);
            System.Int64 b = a.to_Int64();

            Assert.AreEqual(0, b);
        }
        #endregion

        #region to_Int32
        [Test]
        public void to_Int32_Positive()
        {
            Integer a = new Integer(2147483647);
            System.Int32 b = a.to_Int32();

            Assert.AreEqual(2147483647, b);
        }

        [Test]
        public void to_Int32_Negative()
        {
            Integer a = new Integer(-2147483648);
            System.Int32 b = a.to_Int32();

            Assert.AreEqual(-2147483648, b);
        }

        [Test]
        public void to_Int32_Zero()
        {
            Integer a = new Integer(0);
            System.Int32 b = a.to_Int32();

            Assert.AreEqual(0, b);
        }
        #endregion

        #region to_Int16
        [Test]
        public void to_Int16_Positive()
        {
            Integer a = new Integer(50);
            System.Int16 b = a.to_Int16();

            Assert.AreEqual(50, b);
        }

        [Test]
        public void to_Int16_Negative()
        {
            Integer a = new Integer(-50);
            System.Int16 b = a.to_Int16();

            Assert.AreEqual(-50, b);
        }

        [Test]
        public void to_Int16_Zero()
        {
            Integer a = new Integer(0);
            System.Int16 b = a.to_Int16();

            Assert.AreEqual(0, b);
        }

        [Test]
        public void to_Int16_Over16BitSize()
        {
            try {
                Integer a = new Integer(2147483647);
                System.Int16 b = a.to_Int16();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }

        [Test]
        public void to_Int16_Under16BitSize()
        {
            try {
                Integer a = new Integer(-2147483648);
                System.Int16 b = a.to_Int16();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }
        #endregion

        #region to_UInt64
        [Test]
        public void to_UInt64_Positive()
        {
            Integer a = new Integer(2147483647);
            System.UInt64 b = a.to_UInt64();

            Assert.AreEqual(2147483647, b);
        }

        [Test]
        public void to_UInt64_Negative()
        {
            try {
                Integer a = new Integer(-2147483648);
                System.UInt64 b = a.to_UInt64();

                Assert.AreEqual(-2147483648, b);
            } catch (System.OverflowException) {
                //Pass
            }
        }

        [Test]
        public void to_UInt64_Zero()
        {
            Integer a = new Integer(0);
            System.UInt64 b = a.to_UInt64();

            Assert.AreEqual(0, b);
        }
        #endregion

        #region to_UInt32
        [Test]
        public void to_UInt32_Positive()
        {
            Integer a = new Integer(2147483647);
            System.UInt32 b = a.to_UInt32();

            Assert.AreEqual(2147483647, b);
        }

        [Test]
        public void to_UInt32_Negative()
        {
            try {
                Integer a = new Integer(-2147483648);
                System.UInt32 b = a.to_UInt32();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }

        [Test]
        public void to_UInt32_Zero()
        {
            Integer a = new Integer(0);
            System.UInt32 b = a.to_UInt32();

            Assert.AreEqual(0, b);
        }
        #endregion

        #region to_UInt16
        [Test]
        public void to_UInt16_Positive()
        {
            Integer a = new Integer(50);
            System.UInt16 b = a.to_UInt16();

            Assert.AreEqual(50, b);
        }

        [Test]
        public void to_UInt16_Negative()
        {
            try {
                Integer a = new Integer(-50);
                System.UInt16 b = a.to_UInt16();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }

        [Test]
        public void to_UInt16_Zero()
        {
            Integer a = new Integer(0);
            System.UInt16 b = a.to_UInt16();

            Assert.AreEqual(0, b);
        }

        [Test]
        public void to_UInt16_Over16BitSize()
        {
            try {
                Integer a = new Integer(2147483647);
                System.UInt16 b = a.to_UInt16();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }

        [Test]
        public void to_UInt16_Under16BitSize()
        {
            try {
                Integer a = new Integer(-2147483648);
                System.UInt16 b = a.to_UInt16();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }
        #endregion

        #region to_Byte
        [Test]
        public void to_Byte_Positive()
        {
            Integer a = new Integer(50);
            System.Byte b = a.to_Byte();

            Assert.AreEqual(50, b);
        }

        [Test]
        public void toByte_Negative()
        {
            try {
                Integer a = new Integer(-50);
                System.Byte b = a.to_Byte();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }

        [Test]
        public void toByte_Zero()
        {
            Integer a = new Integer(0);
            System.Byte b = a.to_Byte();

            Assert.AreEqual(0, b);
        }

        [Test]
        public void toByte_OverByteSize()
        {
            try {
                Integer a = new Integer(2147483647);
                System.Byte b = a.to_Byte();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }

        [Test]
        public void toByte_UnderByteSize()
        {
            try {
                Integer a = new Integer(-2147483648);
                System.Byte b = a.to_Byte();

                Assert.Fail("Should throw an exception");
            } catch (System.OverflowException) {
                //Pass
            }
        }
        #endregion

        #endregion
    }
}
