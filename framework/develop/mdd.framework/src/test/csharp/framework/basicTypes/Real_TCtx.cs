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
    class Real_TCtx
    {
        #region Constructors
        [Test]
        public void construct_0p0() 
        {
            Real sut = new Real(0.0);

            Assert.AreEqual(0.0, sut.to_Double());
        }

        [Test]
        public void construct_Positive()
        {
            Real a = new Real(583.284);

            Assert.AreEqual(583.284, a.to_Double());
        }

        [Test]
        public void construct_Negative()
        {
            Real a = new Real(-726.148);

            Assert.AreEqual(-726.148, a.to_Double());
        }

        [Test]
        public void construct_With_Another_Real()
        {
            Real a = new Real(1122.3344);
            Real b = new Real(a);

            Assert.AreEqual(1122.3344, b.to_Double());
        }

        [Test]
        public void construct_With_Integer()
        {
            Integer a = new Integer(1234);
            Real b = new Real(a);

            Assert.AreEqual(1234d, b.to_Double());
        }
        #endregion

        #region Operations

        #region Squared
        [Test]
        public void squared_Positive()
        {
            Real a = new Real(1234.5678);
            Real b = a.squared();

            Assert.AreEqual(1524157.6528, b.to_Double(), 0.0002);
        }

        [Test]
        public void squared_Negative()
        {
            Real a = new Real(-1122.3344);
            Real b = a.squared();

            Assert.AreEqual(1259634.50542, b.to_Double(), 0.00002);
        }

        [Test]
        public void squared_Zero()
        {
            Real a = new Real(0.0);
            Real b = a.squared();
            
            Assert.AreEqual(0.0, b.to_Double());
        }
        #endregion

        #region Power
        [Test]
        public void power_PositiveReal()
        {
            Real a = new Real(1234.56);
            Real b = a.power(new Real(3d));

            Assert.AreEqual(1881640295.2, b.to_Double(), 0.1);
        }

        [Test]
        public void power_NegativeReal_OddPower()
        {
            Real a = new Real(-4);
            Real b = a.power(new Real(3d));

            Assert.AreEqual(-64, b.to_Double(), 0.1);
        }

        [Test]
        public void power_NegativeReal_EvenPower()
        {
            Real a = new Real(-4);
            Real b = a.power(new Real(4d));

            Assert.AreEqual(256d, b.to_Double());
        }

        [Test]
        public void power_ZeroReal()
        {
            Real a = new Real(0);
            Real b = a.power(new Real(5d));

            Assert.AreEqual(0.0, b.to_Double());
        }

        [Test]
        public void power_PositivePower()
        {
            Real a = new Real(1234.56);
            Real b = a.power(new Real(3d));

            Assert.AreEqual(1881640295.2, b.to_Double(), 0.1);
        }

        [Test]
        public void power_NegativePower()
        {
            Real a = new Real(12.3);
            Real b = a.power(new Real(-3d));

            Assert.AreEqual(0.00053738391, b.to_Double(), 0.00000000005);
        }

        [Test]
        public void power_ZeroPower()
        {
            Real a = new Real(50000);
            Real b = a.power(new Real(0d));

            Assert.AreEqual(1, b.to_Double());
        }
        #endregion

        #region Absolute
        [Test]
        public void absolute_Positive()
        {
            Real a = new Real(5.3);
            Real b = a.absolute();

            Assert.AreEqual(5.3, b.to_Double());
        }

        [Test]
        public void absolute_Negative()
        {
            Real a = new Real(-5.2);
            Real b = a.absolute();

            Assert.AreEqual(5.2, b.to_Double());
        }

        [Test]
        public void absolute_Zero()
        {
            Real a = new Real(0);
            Real b = a.absolute();

            Assert.AreEqual(0, b.to_Double());
        }
        #endregion

        #region Exponential
        [Test]
        public void exponential_Positive()
        {
            Real a = new Real(5d);
            Real b = a.exponential();

            Assert.AreEqual(148.4131, b.to_Double(), 0.0001);
        }

        [Test]
        public void exponential_Negative()
        {
            Real a = new Real(-5d);
            Real b = a.exponential();

            Assert.AreEqual(0.006737, b.to_Double(), 0.000001);
        }

        [Test]
        public void exponential_Zero()
        {
            Real a = new Real(-0d);
            Real b = a.exponential();

            Assert.AreEqual(1, b.to_Double());
        }
        #endregion

        #region Log
        [Test]
        public void log_Positive()
        {
            Real a = new Real(10d);
            Real b = a.log();

            Assert.AreEqual(2.30258, b.to_Double(), 0.0001);
        }

        [Test]
        public void log_Negative()
        {
            Real a = new Real(-10d);
            Real b = a.log();

            Assert.AreEqual(System.Double.NaN, b.to_Double(), 0.0001);
        }

        [Test]
        public void log_Zero()
        {
            Real a = new Real(0d);
            Real b = a.log();

            Assert.AreEqual(System.Double.NegativeInfinity, b.to_Double(), 0.0001);
        }

        [Test]
        public void log1param_Positive()
        {
            Real a = new Real(10d);
            Real b = a.log(new Real(10.0));

            Assert.AreEqual(1, b.to_Double(), 0.0001);
        }

        [Test]
        public void log1param_Negative()
        {
            Real a = new Real(-10d);
            Real b = a.log(new Real(10.0));

            Assert.AreEqual(System.Double.NaN, b.to_Double(), 0.0001);
        }

        [Test]
        public void log1param_Zero()
        {
            Real a = new Real(0d);
            Real b = a.log(new Real(10.0));

            Assert.AreEqual(System.Double.NegativeInfinity, b.to_Double(), 0.0001);
        }
        #endregion

        #region Truncate
        [Test]
        public void truncate_Positive()
        {
            Real a = new Real(5.2);
            Integer b = a.truncate();

            Assert.AreEqual(5, b.to_Int32());
        }

        [Test]
        public void truncate_Negative()
        {
            Real a = new Real(-5.2);
            Integer b = a.truncate();

            Assert.AreEqual(-5, b.to_Int32());
        }

        [Test]
        public void truncate_Zero()
        {
            Real a = new Real(0.5);
            Integer b = a.truncate();

            Assert.AreEqual(0, b.to_Int32());
        }
        #endregion

        #region Floor
        [Test]
        public void floor_Positive()
        {
            Real a = new Real(5.2);
            Integer b = a.floor();

            Assert.AreEqual(5, b.to_Int32());
        }

        [Test]
        public void floor_Negative()
        {
            Real a = new Real(-5.2);
            Integer b = a.floor();

            Assert.AreEqual(-6, b.to_Int32());
        }

        [Test]
        public void floor_Zero()
        {
            Real a = new Real(0.5);
            Integer b = a.floor();

            Assert.AreEqual(0, b.to_Int32());
        }
        #endregion

        #region Ceiling
        [Test]
        public void ceiling_Positive()
        {
            Real a = new Real(5.2);
            Integer b = a.ceiling();

            Assert.AreEqual(6, b.to_Int32());
        }

        [Test]
        public void ceiling_Negative()
        {
            Real a = new Real(-5.2);
            Integer b = a.ceiling();

            Assert.AreEqual(-5, b.to_Int32());
        }

        [Test]
        public void ceiling_ZeroPoint5()
        {
            Real a = new Real(0.5);
            Integer b = a.ceiling();

            Assert.AreEqual(1, b.to_Int32());
        }
        #endregion

        #region Negate
        [Test]
        public void negate_Positive_to_Negative()
        {
            Real a = new Real(5d);
            Real b = a.negate();

            Assert.AreEqual(-5d, b.to_Double());
        }

        [Test]
        public void negate_Negative_to_Positive()
        {
            Real a = new Real(-5d);
            Real b = a.negate();

            Assert.AreEqual(5d, b.to_Double());
        }
        #endregion

        #region Modulo
        [Test]
        public void modulo_No_Remainder()
        {
            Real a = new Real(10d);
            Real b = new Real(5d);
            Real c = a.modulo(b);

            Assert.AreEqual(0d, c.to_Double());
        }

        [Test]
        public void modulo_Remainder()
        {
            Real a = new Real(8d);
            Real b = new Real(5d);
            Real c = a.modulo(b);

            Assert.AreEqual(3d, c.to_Double());
        }

        [Test]
        public void modulo_Positive()
        {
            Real a = new Real(29d);
            Real b = new Real(7d);
            Real c = a.modulo(b);

            Assert.AreEqual(1d, c.to_Double());
        }

        [Test]
        public void modulo_Negative()
        {
            Real a = new Real(-8d);
            Real b = new Real(3d);
            Real c = a.modulo(b);

            Assert.AreEqual(-2d, c.to_Double());
        }

        [Test]
        public void modulo_Real_modulo_Integer()
        {
            Real a = new Real(12);
            Integer b = new Integer(10);
            Real c = a.modulo(b);

            Assert.AreEqual(2d, c.to_Double());
        }
        #endregion

        #region Plus
        [Test]
        public void plus_Positive_to_Negative()
        {
            Real a = new Real(15d);
            Real b = new Real(-30d);
            Real c = a.plus(b);

            Assert.AreEqual(-15d, c.to_Double());
        }

        [Test]
        public void plus_Negative_to_Positive()
        {
            Real a = new Real(-15d);
            Real b = new Real(30d);
            Real c = a.plus(b);

            Assert.AreEqual(15d, c.to_Double());
        }

        [Test]
        public void plus_Positive_add_Positive()
        {
            Real a = new Real(15d);
            Real b = new Real(30d);
            Real c = a.plus(b);

            Assert.AreEqual(45d, c.to_Double());
        }

        [Test]
        public void plus_Negative_add_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(10d);
            Real c = a.plus(b);

            Assert.AreEqual(5d, c.to_Double());
        }

        [Test]
        public void plus_Positive_add_Negative()
        {
            Real a = new Real(50d);
            Real b = new Real(-5d);
            Real c = a.plus(b);

            Assert.AreEqual(45d, c.to_Double());
        }

        [Test]
        public void plus_Negative_add_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-10d);
            Real c = a.plus(b);

            Assert.AreEqual(-15d, c.to_Double());
        }

        [Test]
        public void plus_Real_plus_Positive_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(10);
            Real c = a.plus(b);

            Assert.AreEqual(15d, c.to_Double());
        }

        [Test]
        public void plus_Real_plus_Negative_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(-10);
            Real c = a.plus(b);

            Assert.AreEqual(-5d, c.to_Double());
        }

        [Test]
        public void plus_Real_plus_Zero_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(0);
            Real c = a.plus(b);

            Assert.AreEqual(5d, c.to_Double());
        }
        #endregion

        #region Minus
        [Test]
        public void minus_Negative_to_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(-10d);
            Real c = a.minus(b);

            Assert.AreEqual(5d, c.to_Double());
        }

        [Test]
        public void minus_Positive_to_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(10d);
            Real c = a.minus(b);

            Assert.AreEqual(-5d, c.to_Double());
        }

        [Test]
        public void minus_Positive_subtract_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(5d);
            Real c = a.minus(b);

            Assert.AreEqual(0d, c.to_Double());
        }

        [Test]
        public void minus_Positive_subtract_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(-5d);
            Real c = a.minus(b);

            Assert.AreEqual(10d, c.to_Double());
        }

        [Test]
        public void minus_Negative_subtract_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(5d);
            Real c = a.minus(b);

            Assert.AreEqual(-10d, c.to_Double());
        }

        [Test]
        public void minus_Negative_subtract_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-5d);
            Real c = a.minus(b);

            Assert.AreEqual(0d, c.to_Double());
        }

        [Test]
        public void minus_Real_subtract_Positive_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(10);
            Real c = a.minus(b);

            Assert.AreEqual(-5d, c.to_Double());
        }

        [Test]
        public void minus_Real_subtract_Negative_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(-10);
            Real c = a.minus(b);

            Assert.AreEqual(15d, c.to_Double());
        }

        [Test]
        public void minus_Real_subtract_Zero_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(0);
            Real c = a.minus(b);

            Assert.AreEqual(5d, c.to_Double());
        }
        #endregion

        #region multiply
        [Test]
        public void multiply_Negative_to_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(-10d);
            Real c = a.multiply(b);

            Assert.AreEqual(50d, c.to_Double());
        }

        [Test]
        public void multiply_Positive_to_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(-10d);
            Real c = a.multiply(b);

            Assert.AreEqual(-50d, c.to_Double());
        }

        [Test]
        public void multiply_Positive_times_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(8d);
            Real c = a.multiply(b);

            Assert.AreEqual(40d, c.to_Double());
        }

        [Test]
        public void multiply_Positive_times_Negative()
        {
            Real a = new Real(1d);
            Real b = new Real(-20d);
            Real c = a.multiply(b);

            Assert.AreEqual(-20d, c.to_Double());
        }

        [Test]
        public void multiply_Negative_times_Negative()
        {
            Real a = new Real(-7d);
            Real b = new Real(-3d);
            Real c = a.multiply(b);

            Assert.AreEqual(21d, c.to_Double());
        }

        [Test]
        public void multiply_Negative_times_Positive()
        {
            Real a = new Real(-3d);
            Real b = new Real(4d);
            Real c = a.multiply(b);

            Assert.AreEqual(-12d, c.to_Double());
        }

        [Test]
        public void multiply_Real_times_Positive_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(10);
            Real c = a.multiply(b);

            Assert.AreEqual(50d, c.to_Double());
        }

        [Test]
        public void multiply_Real_times_Negative_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(-10);
            Real c = a.multiply(b);

            Assert.AreEqual(-50d, c.to_Double());
        }

        [Test]
        public void multiply_Real_times_Zero_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(0);
            Real c = a.multiply(b);

            Assert.AreEqual(0d, c.to_Double());
        }
        #endregion

        #region Divide
        [Test]
        public void divide_byZero()
        {
            Real a = new Real(5);
            Real b = new Real(0);
            Real c = a.divide(b);

            Assert.AreEqual(System.Double.PositiveInfinity, c.to_Double(), 0.01);
        }

        [Test]
        public void divide_Positive_to_Negative()
        {
            Real a = new Real(5);
            Real b = new Real(-4);
            Real c = a.divide(b);

            Assert.AreEqual(-1.25, c.to_Double(), 0.01);
        }

        [Test]
        public void divide_Negative_to_Positive()
        {
            Real a = new Real(-5);
            Real b = new Real(-4);
            Real c = a.divide(b);

            Assert.AreEqual(1.25, c.to_Double(), 0.01);
        }

        [Test]
        public void divide_Positive_over_Positive()
        {
            Real a = new Real(10);
            Real b = new Real(5);
            Real c = a.divide(b);

            Assert.AreEqual(2, c.to_Double());
        }

        [Test]
        public void divide_Positive_over_Negative()
        {
            Real a = new Real(15);
            Real b = new Real(-3);
            Real c = a.divide(b);

            Assert.AreEqual(-5, c.to_Double());
        }

        [Test]
        public void divide_Negative_over_Negative()
        {
            Real a = new Real(-10);
            Real b = new Real(-10);
            Real c = a.divide(b);

            Assert.AreEqual(1, c.to_Double());
        }

        [Test]
        public void divide_Negative_over_Positive()
        {
            Real a = new Real(-8);
            Real b = new Real(2);
            Real c = a.divide(b);

            Assert.AreEqual(-4, c.to_Double());
        }

        [Test]
        public void divide_Real_over_Positive_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(10);
            Real c = a.divide(b);

            Assert.AreEqual(0.5, c.to_Double());
        }

        [Test]
        public void divide_Real_over_Negative_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(-10);
            Real c = a.divide(b);

            Assert.AreEqual(-0.5, c.to_Double());
        }

        [Test]
        public void divide_Real_over_Zero_Integer()
        {
            Real a = new Real(5d);
            Integer b = new Integer(0);
            Real c = a.divide(b);

            Assert.AreEqual(System.Double.PositiveInfinity, c.to_Double());
        }
        #endregion

        #region operator_divide
        [Test]
        public void operator_divide_Positive_over_Positive() {
            Real a = new Real(10);
            Real b = new Real(5);
            Real c = a / b;

            Assert.AreEqual(2, c.to_Double());
        }
        #endregion

        #region Max
        [Test]
        public void max_Positive_max_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(4d);
            Real c = a.max(b);

            Assert.AreEqual(5d, c.to_Double());
        }

        [Test]
        public void max_Positive_max_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(-4d);
            Real c = a.max(b);

            Assert.AreEqual(5d, c.to_Double());
        }

        [Test]
        public void max_Negative_max_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(4d);
            Real c = a.max(b);

            Assert.AreEqual(4d, c.to_Double());
        }

        [Test]
        public void max_Negative_max_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-4d);
            Real c = a.max(b);

            Assert.AreEqual(-4d, c.to_Double());
        }

        [Test]
        public void max_Real_max_BiggerInteger()
        {
            Real a = new Real(5d);
            Integer b = new Integer(6);
            Real c = a.max(b);

            Assert.AreEqual(6d, c.to_Double());
        }

        [Test]
        public void max_Real_max_SmallerInteger()
        {
            Real a = new Real(7d);
            Integer b = new Integer(6);
            Real c = a.max(b);

            Assert.AreEqual(7d, c.to_Double());
        }
        #endregion

        #region Min
        [Test]
        public void min_Positive_min_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(4d);
            Real c = a.min(b);

            Assert.AreEqual(4d, c.to_Double());
        }

        [Test]
        public void min_Positive_min_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(-4d);
            Real c = a.min(b);

            Assert.AreEqual(-4d, c.to_Double());
        }

        [Test]
        public void min_Negative_min_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(4d);
            Real c = a.min(b);

            Assert.AreEqual(-5d, c.to_Double());
        }

        [Test]
        public void min_Negative_min_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-4d);
            Real c = a.min(b);

            Assert.AreEqual(-5d, c.to_Double());
        }

        [Test]
        public void min_Real_min_BiggerInteger()
        {
            Real a = new Real(5d);
            Integer b = new Integer(6);
            Real c = a.min(b);

            Assert.AreEqual(5d, c.to_Double());
        }

        [Test]
        public void min_Real_min_SmallerInteger()
        {
            Real a = new Real(7d);
            Integer b = new Integer(6);
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
            Real a = new Real(5d);
            Real b = new Real(5d);
            Boolean isEqual = a.equalTo(b);

            Assert.AreEqual(true, isEqual.to_Boolean());
        }

        [Test]
        public void equalTo_isntEqual()
        {
            Real a = new Real(5d);
            Real b = new Real(6d);
            Boolean isEqual = a.equalTo(b);

            Assert.AreEqual(false, isEqual.to_Boolean());
        }

        [Test]
        public void equalTo_Negative_equalTo_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(5d);
            Boolean isEqual = a.equalTo(b);

            Assert.AreEqual(false, isEqual.to_Boolean());
        }

        [Test]
        public void equalTo_Positive_equalTo_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(-5d);
            Boolean isEqual = a.equalTo(b);

            Assert.AreEqual(false, isEqual.to_Boolean());
        }
        #endregion

        #region notEqualTo
        [Test]
        public void notEqualTo_isEqual()
        {
            Real a = new Real(5d);
            Real b = new Real(6d);
            Boolean isEqual = a.notEqualTo(b);

            Assert.AreEqual(true, isEqual.to_Boolean());
        }

        [Test]
        public void notEqualTo_isntEqual()
        {
            Real a = new Real(5d);
            Real b = new Real(5d);
            Boolean isEqual = a.notEqualTo(b);

            Assert.AreEqual(false, isEqual.to_Boolean());
        }

        [Test]
        public void notEqualTo_Negative_equalTo_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(5d);
            Boolean isEqual = a.notEqualTo(b);

            Assert.AreEqual(true, isEqual.to_Boolean());
        }

        [Test]
        public void notEqualTo_Positive_equalTo_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(-5d);
            Boolean isEqual = a.notEqualTo(b);

            Assert.AreEqual(true, isEqual.to_Boolean());
        }
        #endregion

        #region greaterThan
        [Test]
        public void greaterThan_Positive_biggerThan_Positive()
        {
            Real a = new Real(7d);
            Real b = new Real(6d);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Positive_equalTo_Positive()
        {
            Real a = new Real(6d);
            Real b = new Real(6d);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Positive_biggerThan_Negative()
        {
            Real a = new Real(7d);
            Real b = new Real(-6d);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Negative_biggerThan_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-6d);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Positive_smallerThan_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(6d);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Negative_smallerThan_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(6d);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Negative_smallerThan_Negative()
        {
            Real a = new Real(-7d);
            Real b = new Real(-6d);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThan_Negative_equalTo_Negative()
        {
            Real a = new Real(-6d);
            Real b = new Real(-6d);
            Boolean c = a.greaterThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }
        #endregion

        #region greaterThanOrEqualTo
        [Test]
        public void greaterThanOrEqualTo_Positive_biggerThan_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(4d);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Positive_equalTo_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(5d);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Positive_smallerThan_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(6d);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Negative_biggerThan_Negative()
        {
            Real a = new Real(-3d);
            Real b = new Real(-4d);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Negative_equalTo_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-5d);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Negative_smallerThan_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-4d);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Positive_biggerThan_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(-4d);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void greaterThanOrEqualTo_Negative_smallerThan_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(4d);
            Boolean c = a.greaterThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }
        #endregion

        #region lessThan
        [Test]
        public void lessThan_Positive_biggerThan_Positive()
        {
            Real a = new Real(7d);
            Real b = new Real(6d);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThan_Positive_equalTo_Positive()
        {
            Real a = new Real(6d);
            Real b = new Real(6d);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThan_Positive_biggerThan_Negative()
        {
            Real a = new Real(7d);
            Real b = new Real(-6d);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThan_Negative_biggerThan_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-6d);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThan_Positive_smallerThan_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(6d);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThan_Negative_smallerThan_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(6d);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThan_Negative_smallerThan_Negative()
        {
            Real a = new Real(-7d);
            Real b = new Real(-6d);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThan_Negative_equalTo_Negative()
        {
            Real a = new Real(-6d);
            Real b = new Real(-6d);
            Boolean c = a.lessThan(b);

            Assert.AreEqual(false, c.to_Boolean());
        }
        #endregion

        #region lessThanOrEqualTo
        [Test]
        public void lessThanOrEqualTo_Positive_biggerThan_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(4d);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Positive_equalTo_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(5d);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Positive_smallerThan_Positive()
        {
            Real a = new Real(5d);
            Real b = new Real(6d);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Negative_biggerThan_Negative()
        {
            Real a = new Real(-3d);
            Real b = new Real(-4d);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Negative_equalTo_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-5d);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Negative_smallerThan_Negative()
        {
            Real a = new Real(-5d);
            Real b = new Real(-4d);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Positive_biggerThan_Negative()
        {
            Real a = new Real(5d);
            Real b = new Real(-4d);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(false, c.to_Boolean());
        }

        [Test]
        public void lessThanOrEqualTo_Negative_smallerThan_Positive()
        {
            Real a = new Real(-5d);
            Real b = new Real(4d);
            Boolean c = a.lessThanOrEqualTo(b);

            Assert.AreEqual(true, c.to_Boolean());
        }
        #endregion

        #endregion

        #region Cloneable
        [Test]
        public void deepClone() {
            Real sut = 0;
            Real clone = sut.deepClone();

            Assert.AreEqual(sut, clone);
            Assert.True(sut!=clone);
            Assert.True(sut.Equals(clone));
            Assert.True(sut.equalTo(clone));
        }
        #endregion

        #region Framework Converters
        [Test]
        public void asBitStringNoSignBit__0() {
            Real sut = 0;
            BitStringN res = sut.asBitStringNoSignBit(2, 1);

            Assert.AreEqual("00", res.asStringBinary().to_string());
        }
        [Test]
        public void asBitStringNoSignBit__p1p5_3_1() {
            Real sut = 1.5;
            BitStringN res = sut.asBitStringNoSignBit(2, 1);

            Assert.AreEqual("11", res.asStringBinary().to_string());
        }

        [Test]
        public void asBitStringMostSignificantSignBit__p1p5_3_1() {
            Real sut = 1.5;
            BitStringN res = sut.asBitStringMostSignificantSignBit(3, 1);

            Assert.AreEqual("011", res.asStringBinary().to_string());
        }
        [Test]
        public void asBitStringMostSignificantSignBit__p1p5_4_1() {
            Real sut = 1.5;
            BitStringN res = sut.asBitStringMostSignificantSignBit(4, 1);

            Assert.AreEqual("0011", res.asStringBinary().to_string());
        }

        [Test]
        public void asBitStringMostSignificantSignBit__p1p5_4_2() {
            Real sut = 1.5;
            BitStringN res = sut.asBitStringMostSignificantSignBit(4, 2);

            Assert.AreEqual("0110", res.asStringBinary().to_string());
        }

        [Test]
        public void asBitStringMostSignificantSignBit__p1p25_4_2() {
            Real sut = 1.25;
            BitStringN res = sut.asBitStringMostSignificantSignBit(4, 2);

            Assert.AreEqual("0101", res.asStringBinary().to_string());
        }        
        
        [Test]
        public void asBitStringMostSignificantSignBit__m1p25_4_2() {
            Real sut = -1.25;
            BitStringN res = sut.asBitStringMostSignificantSignBit(4, 2);

            Assert.AreEqual("1101", res.asStringBinary().to_string());
        }   
        
        
        #endregion

        #region Basic Language Converters

        #region to_Double
        [Test]
        public void to_Double_Positive()
        {
            Real a = new Real(9223372036854775807);
            System.Double b = a.to_Double();

            Assert.AreEqual(9223372036854775807, b);
        }

        [Test]
        public void to_Double_Negative()
        {
            Real a = new Real(-9223372036854775808);
            System.Double b = a.to_Double();

            Assert.AreEqual(-9223372036854775808, b);
        }

        [Test]
        public void to_Double_Zero()
        {
            Real a = new Real(0);
            System.Double b = a.to_Double();

            Assert.AreEqual(0, b);
        }
        #endregion

        #endregion
    }
}
