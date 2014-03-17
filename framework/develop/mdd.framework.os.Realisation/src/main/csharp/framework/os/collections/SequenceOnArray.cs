/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.os.collections
{
    using framework.basicTypes;
    using framework.collections;
    using System.Linq;

    public class SequenceOnArray<T> : Sequence<T>
    {
        public SequenceOnArray(T[] array) {
            this._array = array;
        }
        public SequenceOnArray(System.Collections.Generic.IEnumerable<T> col) {
            this._array = col.ToArray();
        }

        T[] _array;
        public T[] array {
            get { return this._array; }
        }

        public PositiveInteger size {
            get { return (uint)this.array.Length; }
        }
        public Boolean isEmpty {
            get { return 0 == this.size.to_UInt32(); }
        }

        public Boolean includes(T other) {
            return this.Contains(other);
        }
        public Boolean includesAll(Bag<T> other) {
            return other.forAll(e => this.includes(e));
        }

        public Boolean excludes(T other) {
            return this.includes(other).not();
        }
        public Boolean excludesAll(Bag<T> other) {
            return this.includesAll(other).not();
        }

        public T at(Integer index) {
            int i = index.to_Int32() - 1;
            if (0 <= i && i < this.array.Length) {
                return this.array[i];
            } else {
                throw new System.IndexOutOfRangeException();
            }
        }

        public PositiveInteger indexOf(T element) {
            int i = this.array.ToList().IndexOf(element);
            if (-1 == i) {
                return null;
            } else {
                //adjust to 1-based indexing
                return new PositiveInteger((ulong)i + 1);
            }
        }

        public Sequence<T2> cast<T2>()
        {
            return new SequenceOnArray<T2>(this.Cast<T2>());
        }

        #region Cloneable
        public Sequence<T> deepClone() {
            return this.collect(e => {
                if (e is Cloneable) {
                    System.Reflection.MethodInfo m = e.GetType().GetMethod("deepClone");
                    return (T)m.Invoke(e,new object[0]);
                } else {
                    throw new Exception("Cannont deepClone the Sequence. " + e + " is not cloneable.");
                }
            }) as Sequence<T>;
        }
        #endregion

        #region System.Collections.Generic.IEnumerable<T>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator() {
            return ((System.Collections.Generic.IEnumerable<T>)this.array).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.array.GetEnumerator();
        }
        #endregion

        #region Bag
        public void forEvery(global::System.Action<T> expr) {
            foreach (T el in this.array) {
                expr.Invoke(el);
            }
        }
        public R iterate<R>(R initialValue, global::System.Func<T, R, R> expr) {
            R res = initialValue;
            foreach(T el in this.array) {
                res = expr.Invoke(el, res);
            }
            return res;
        }
        public Boolean forAll(global::System.Func<T, Boolean> expr) {
            Boolean b = true;
            foreach (T e in this.array) {
                b = b.and(expr.Invoke(e));
            }
            return b;
        }

        public Bag<T2> collect<T2>(global::System.Func<T, T2> expr) {
            return new SequenceOnArray<T2>( this.array.Select(expr) );
        }
        public Bag<T> select(global::System.Func<T, Boolean> expr) {
            return new SequenceOnArray<T>(this.Where(el => expr.Invoke(el)));
        }

        public Bag<T> reject(global::System.Func<T, Boolean> expr) {
            return new SequenceOnArray<T>(this.Where(el => !expr.Invoke(el)));
        }
        #endregion

        public Sequence<T> append(T newElement) {
            System.Collections.Generic.List<T> l = this.array.ToList();
            l.Add(newElement);
            return new SequenceOnArray<T>(l.ToArray());
        }
        public Sequence<T> concatinate(Sequence<T> other) {
            System.Collections.Generic.List<T> l = this.array.ToList();
            l.AddRange(other.array);
            return new SequenceOnArray<T>(l.ToArray());
        }
        public Sequence<T> prepend(T newElement) {
            System.Collections.Generic.List<T> l = this.array.ToList();
            l.Insert(0,newElement);
            return new SequenceOnArray<T>(l.ToArray());
        }

        public Sequence<T> including(T newElement) {
            System.Collections.Generic.List<T> l = this.array.ToList();
            l.Add(newElement);
            return new SequenceOnArray<T>(l.ToArray());
        }

        public Sequence<T> excluding(T element) {
            System.Collections.Generic.List<T> l = this.array.ToList();
            l.Remove(element);
            return new SequenceOnArray<T>(l.ToArray());
        }

        public Sequence<T> subSequence(PositiveInteger firstIndex, PositiveInteger lastIndex) {
            int f = firstIndex.to_Int32() - 1;
            int l = lastIndex.to_Int32() - 1;
            int len = this.size.to_Int32();
            if (   f >= len 
                || l >= len
                || f > l ) {
                throw new global::System.IndexOutOfRangeException("subSequence("+firstIndex+", "+lastIndex+")");
            } else {
                int c = l - f + 1;
                return new SequenceOnArray<T>( this.array.ToList().GetRange(f,c) );
            }
        }

        public Sequence<T> transitiveClosure(global::System.Func<T, Sequence<T>> expr) {
            //Could improve the efficiency of this perhaps,
            //the loop invokes the expression on oldEleemnts as well as new
            //would be more efficient to only invoke it on new elements
            framework.os.OsRef os = new framework.os.OsRef();
            Set<T> oldResult = os.Set<T>();
            Sequence<T> result = os.Sequence<T>();
            result = result.concatinate(this);
            while(result.asSet().isSubsetOf(oldResult).not()) {
                oldResult = result.asSet();
                foreach (T elem in oldResult) {
                    Sequence<T> r = expr.Invoke(elem);
                    Sequence<T> newElements = (Sequence<T>)r.select(x => result.excludes(x));
                    result = result.concatinate(newElements);
                }
            }
            return result;
        }

        public T first() {
            return this.at(1);
        }

        public Sequence<T> tail() {
            return this.excluding(this.first());
        }

        public T last() {
            return this.Last();
        }
        public Sequence<T> reverseTail() {
            return this.excluding(this.last());
        }

        public void set(PositiveInteger index, T value) {
            int i = index.to_Int32() - 1;
            if (0 <= i && i < this.array.Length) {
                this.array[i] = value;
            } else {
                throw new System.IndexOutOfRangeException();
            }
        }
        public void setRange(PositiveInteger index, Sequence<T> value) {
            uint i = index.to_UInt32();
            foreach (T t in value) {
                this.set(i, t);
                i++;
            }
        }


        public Set<T> asSet() {
            framework.os.OsRef os = new framework.os.OsRef();
            return os.Set<T>(this);
        }
        public Sequence<T> asSequence() {
            framework.os.OsRef os = new framework.os.OsRef();
            return os.Sequence<T>(this);
        }
        public OrderedSet<T> asOrderedSet() {
            throw new System.NotImplementedException();
            //framework.os.OsRef os = new framework.os.OsRef();
           // return os.OrderedSet<T>(this);
        }


        public override string ToString() {
            if (this.isEmpty) {
                return "Sequence{}";
            } else {
                string s = this.iterate<String>("", (e, r) => r + e.ToString() + ",").to_string();
                return "Sequence{" + s.Substring(0, s.Length - 1) + "}";
            }
        }

        public override int GetHashCode() {
            int hashCode = 0;
            for (int i = 0; i < this.size; ++i) {
                hashCode = hashCode * 2 + this.array[i].GetHashCode();
            }
            return hashCode;
        }
        public override bool Equals(object obj) {
            if (obj is Sequence<T>) {
                Sequence<T> other = obj as Sequence<T>;
                if (this.array.Length == other.array.Length) {
                    for (int i = 0; i < this.size; ++i) {
                        if (this.array[i].Equals(other.array[i])) {
                            //continue
                        } else {
                            return false;
                        }
                    }
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
    }
}
