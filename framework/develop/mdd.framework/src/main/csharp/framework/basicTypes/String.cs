/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.basicTypes
{
    using System.Linq;
    using System.Collections.Generic;
    using framework.collections;

    static public class StringSum
    {
        static public string concat(this System.Collections.Generic.IEnumerable<string> self) {
            return self.Aggregate("", (t, s) => t += s);
        }

        static public String concat(this System.Collections.Generic.IEnumerable<String> self) {
            return self.Aggregate(new String(""), (t, s) => t = t + s);
        }

        static public String asString<T>(this Sequence<T> self) where T : BitString {
            String s = new String("");
            uint i = 1;
            T t = self.at(i);
            while (0 != t.asInteger().to_Int32()) {
                s = s + t.asString();
                i++;
                t = self.at(i);
            }
            return s;
        }
    }

    //Primitive Type
    public class String : Cloneable
    {
        public static implicit operator String(string value)  // implicit conversion operator
        {
            return new String(value);
        }

        #region Variables
        string value;
        #endregion

        #region Constructors
        public String(string value) { this.value = value; }
        public String(String value) { this.value = value.value; }
        #endregion

        #region Operations
        public PositiveInteger length() { return new PositiveInteger(System.Convert.ToUInt32(this.value.Length)); }
        public String concat(String other) { return new String(this.value + other.value); }
        public String subString(PositiveInteger first, PositiveInteger last) {
            //range check
            
            if (first <= 0) first = 1;
            if (first > this.value.Length) first = (uint)this.value.Length;
            if (last <= first) last = first;

            int f = first.to_Int32() - 1;
            int len = (last.to_Int32() - f);

            return this.value.Substring(f, len);
        }
        public String replaceAll(String oldStr, String newStr) { return this.value.Replace(oldStr.value, newStr.value); }
        public String replaceFirst(String oldStr, String newStr) {
            int i = this.value.IndexOf(oldStr.to_string());
            if (i < 0) return this.value;
            string result = this.value.Remove(i, oldStr.to_string().Length).Insert(i, newStr.to_string());
            return result;

        }
        public String replaceNth(Integer n, String oldStr, String newStr) {
            System.Collections.Generic.List<string> split = this.value.Split(new string[] { oldStr.to_string() }, System.StringSplitOptions.None).ToList();
            System.Collections.Generic.IEnumerable<string> f = split.Take(n.to_Int32()-1);
            System.Collections.Generic.IEnumerable<string> l = split.Skip(n.to_Int32());
            String ns = f.concat() + newStr + l.concat();
            return ns;
        }
        public String replaceLast(String oldStr, String newStr) {
            int i = this.value.LastIndexOf(oldStr.to_string());
            if (i < 0) return this.value;
            string result = this.value.Remove(i, oldStr.to_string().Length).Insert(i, newStr.to_string());
            return result;
        }
        public Boolean startsWith(String str) { return this.value.StartsWith(str.value); }
        public Boolean endsWith(String str) { return this.value.EndsWith(str.value); }
        public Boolean contains(String str) { return this.value.Contains(str.value); }
        public Boolean isEmpty() { return "" == this.value.Trim(); }
        public Boolean isNotEmpty() { return "" != this.value.Trim(); }
        public String reverse() { throw new System.NotImplementedException(); }
        public Boolean isMatch(String regularExpression) { return System.Text.RegularExpressions.Regex.IsMatch(this.value, regularExpression.to_string()); }
        public Sequence<String> split(String splitChars) {
            framework.os.OsRef os = new framework.os.OsRef();
            IEnumerable<String> res = this.value.Split(splitChars.to_string().ToCharArray(), System.StringSplitOptions.None).Select(s => new String(s));
            return os.Sequence<String>(res);
        }
        public String toLower() { return this.value.ToLower(); }
        public String toUpper() { return this.value.ToUpper(); }
        public String toLowerFirst() { return this.value.Substring(0, 1).ToLower() + this.value.Substring(1,this.value.Length-1); }
        public String toUpperFirst() { return this.value.Substring(0, 1).ToUpper() + this.value.Substring(1, this.value.Length - 1); }
        public PositiveInteger indexOf(String str) {
            return (uint) this.value.IndexOf(str.to_string()) + 1;
        }
        #endregion

        #region Framework Converters
        public E? asEnum<E>() where E : struct {
            E e;
            string es = this.value.Trim().Replace("\0",string.Empty);
            bool success = System.Enum.TryParse(es, out e);
            if (success) {
                return e;
            } else {
                return null;
            }
        }
        public String asString() { return new String(this.value); }
        public Sequence<BitString8> asSequenceOfBitString8() {
            os.OsRef os = new framework.os.OsRef();
            System.Char[] arr = this.value.ToCharArray();
            List<BitString8> res = arr.Select(c => new BitString8(System.Convert.ToByte(c))).ToList();
            return os.Sequence(res);
        }
        public Sequence<BitString16> asSequenceOfBitString16() {
            os.OsRef os = new framework.os.OsRef();
            System.Char[] arr = this.value.ToCharArray();
            List<BitString16> res = arr.Select(c => new BitString16( System.Convert.ToUInt16(c) ) ).ToList();
            return os.Sequence(res);
        }
        public Sequence<BitString32> asSequenceOfBitString32() {
            os.OsRef os = new framework.os.OsRef();
            System.Char[] arr = this.value.ToCharArray();
            List<BitString32> res = arr.Select(c => new BitString32(System.Convert.ToUInt32(c))).ToList();
            return os.Sequence(res);
        }
        public Sequence<BitString64> asSequenceOfBitString64() {
            os.OsRef os = new framework.os.OsRef();
            System.Char[] arr = this.value.ToCharArray();
            List<BitString64> res = arr.Select(c => new BitString64(System.Convert.ToUInt64(c))).ToList();
            return os.Sequence(res);
        }
        public BitString64 asBitString64Binary() { return new BitString64(this.value); }
        public BitString64 asBitString64Octal() { return new BitString64(System.Convert.ToUInt64(this.value, 8)); }
        public BitString8 asBitString8Hex() { return new BitString8(System.Convert.ToByte(this.value, 16)); }
        public BitString16 asBitString16Hex() { return new BitString16(System.Convert.ToUInt16(this.value, 16)); }
        public BitString32 asBitString32Hex() { return new BitString32(System.Convert.ToUInt32(this.value, 16)); }
        public BitString64 asBitString64Hex() { return new BitString64(System.Convert.ToUInt64(this.value, 16)); }
        public Boolean asBoolean() { return new Boolean(System.Boolean.Parse(this.value)); }
        public Real asReal() { return new Real(System.Double.Parse(this.value)); }
        public Integer asInteger() { return new Integer(System.Int32.Parse(this.value)); }
        public PositiveInteger asPositiveInteger() { return new PositiveInteger(System.UInt32.Parse(this.value)); }
        public DateTime asDateTime() { return new DateTime(System.DateTime.Parse(this.value)); }
        public DateTime asDateTime(String format) { return new DateTime(System.DateTime.ParseExact(this.value, format.to_string(), System.Globalization.CultureInfo.InvariantCulture)); }
        #endregion

        #region Comparison
        public Boolean equalTo(String other) { return this.Equals(other); }
        public Boolean notEqualTo(String other) { return ! this.Equals(other); }
        public Boolean greaterThan(String other) { throw new System.NotImplementedException(); }
        public Boolean greaterThanOrEqualTo(String other) { throw new System.NotImplementedException(); }
        public Boolean lessThan(String other) { throw new System.NotImplementedException(); }
        public Boolean lessThanOrEqualTo(String other) { throw new System.NotImplementedException(); }

        public override bool Equals(object obj)
        {
            if (obj is String)
                return ((String)obj).value.Equals(this.value);
            else
                return false;
        }
        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
        #endregion

        #region Cloneable
        protected virtual object _deepClone() {
            return new String(this.value);
        }
        public String deepClone() {
            return this._deepClone() as String;
        }
        #endregion

        #region Base Language Converters
        public string to_string() { return value; }


        public static String[] convert(string[] arr)
        {
            String[] converted = new String[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                converted[i] = new String(arr[i]);
            }
            return converted;
        }
        #endregion

        #region Base Language operators
        public static String operator +(String left, String right) {
            return left.concat(right);
        }
        public static String operator +(String left, Integer right) {
            return left.concat(right.asString());
        }
        public static String operator +(String left, Real right) {
            return left.concat(right.asString());
        }
        #endregion

        public override System.String ToString() {
            return value.ToString();
        }
    }

}
