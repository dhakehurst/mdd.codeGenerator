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

    public class SetOnHashSet<T> : Set<T>
    {
        public SetOnHashSet(System.Collections.Generic.IEnumerable<T> col) {
            this.implementation = new System.Collections.Generic.HashSet<T>(col);
        }

        System.Collections.Generic.HashSet<T> implementation;

        public Boolean isSubsetOf(Set<T> other) {
            return other.includesAll(this);
        }


        public PositiveInteger size {
            get { return new PositiveInteger((ulong)implementation.Count); }
        }

        public Boolean isEmpty {
            get { return this.implementation.Count == 0; }
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

        public Boolean forAll(System.Func<T, Boolean> expr) {
            Boolean b = true;
            foreach (T e in this.implementation) {
                b = b.and(expr.Invoke(e));
            }
            return b;
        }

        public void forEvery(System.Action<T> expr) {
            throw new System.NotImplementedException();
        }

        public R iterate<R>(R initialValue, System.Func<T, R, R> expr) {
            R res = initialValue;
            foreach (T el in this.implementation) {
                res = expr.Invoke(el, res);
            }
            return res;
        }

        public Bag<T2> collect<T2>(global::System.Func<T, T2> expr) {
            return new SetOnHashSet<T2>(this.implementation.Select(expr));
        }
        public Bag<T> select(global::System.Func<T, Boolean> expr) {
            return new SetOnHashSet<T>(this.Where(el => expr.Invoke(el)));
        }

        public Bag<T> reject(global::System.Func<T, Boolean> expr) {
            return new SetOnHashSet<T>(this.Where(el => !expr.Invoke(el)));
        }

        #region System.Collections.Generic.IEnumerable<T>
        public System.Collections.Generic.IEnumerator<T> GetEnumerator() {
            return ((System.Collections.Generic.IEnumerable<T>)this.implementation).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.implementation.GetEnumerator();
        }
        #endregion


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
                return "Set{}";
            } else {
                string s = this.iterate<String>("", (e, r) => r + e.ToString() + ",").to_string();
                return "Set{" + s.Substring(0, s.Length - 1) + "}";
            }
        }
        public override int GetHashCode() {
            int hashCode = 0;
            T[] arr = this.implementation.ToArray();
            for (int i = 0; i < this.size; ++i) {
                hashCode = hashCode * 2 + arr[i].GetHashCode();
            }
            return hashCode;
        }
        public override bool Equals(object obj) {
            if (obj is Set<T>) {
                Set<T> other = obj as Set<T>;
                T[] arr = this.implementation.ToArray();
                T[] otherArr = other.ToArray();
                if (arr.Length == otherArr.Length) {
                    for (int i = 0; i < this.size; ++i) {
                        if (arr[i].Equals(otherArr[i])) {
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
