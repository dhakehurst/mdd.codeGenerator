/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.os.dotNet_4_0 {

    using framework.basicTypes;
    using framework.collections;

    public class OsImpl : framework.os.OS, System.IDisposable {

        public OsImpl() {
            if (null == OsRef.actualOs) {
                OsRef.actualOs = this;
            } else {
                throw new OsException("Os object already instantiated, you can have only one (did you 'Dispose' the old one).");
            }
        }

        #region basicTypes Factory
        public String String(System.String value) {
            return new String(value);
        }
        public Boolean Boolean(System.Boolean value) {
            return new Boolean(value);
        }
        public Real Real(System.Double value) {
            return new Real(value);
        }
        public Integer Integer(System.Int64 value) {
            return new Integer(value);
        }
        public PositiveInteger PositiveInteger(System.UInt64 value) {
            return new PositiveInteger(value);
        }
        #endregion

        #region collections Factory
        public Sequence<T> Sequence<T>() {
            return new framework.os.collections.SequenceOnArray<T>(new T[]{});
        }
        public Sequence<T> Sequence<T>(T[] array) {
            return new framework.os.collections.SequenceOnArray<T>(array);
        }
        public Sequence<T> Sequence<T>(System.Collections.Generic.IEnumerable<T> enumerable) {
            return new framework.os.collections.SequenceOnArray<T>(enumerable);
        }

        public Sequence<BitString8> SequenceOfBitString8<T>(T structure) where T : struct {
            return structure.asSequenceOfBitString8();
        }
        public T Structure<T>(Sequence<BitString8> seq) where T : struct {
            return seq.asStructure<T>();
        }

        public Set<T> Set<T>() {
            return new framework.os.collections.SetOnHashSet<T>(new T[] { });
        }
        public Set<T> Set<T>(System.Collections.Generic.IEnumerable<T> enumerable) {
            return new framework.os.collections.SetOnHashSet<T>(enumerable);
        }

        #endregion

        #region Threading Members
        public void threadSleep(global::framework.time.Duration duration) {
            System.Threading.Thread.Sleep(duration.asMilliseconds.truncate().to_Int32());
        }
        public framework.os.threading.Thread createThread(global::framework.basicTypes.String threadName) {
            return new global::framework.os.threading.ThreadImpl(threadName);
        }

        public framework.os.threading.Mutex createMutex() {
            return new global::framework.os.threading.MutexImpl();
        }

        public framework.os.threading.Monitor createMonitor() {
            return new global::framework.os.threading.MonitorImpl();
        }

        public framework.os.threading.Semaphore createSemaphore(PositiveInteger initialCount, PositiveInteger maximumCount) {
            return new global::framework.os.threading.SemaphoreImpl(initialCount, maximumCount);
        }

        #endregion

        #region Interprocess Members

        public framework.os.interprocess.Process createProcess(global::framework.io.fileSystem.PathName pathName) {
            return new global::framework.os.interprocess.ProcessImpl(pathName);
        }

        public framework.os.interprocess.NamedMutex createNamedMutex(global::framework.os.interprocess.NamedItemIdentifier identity) {
            return new global::framework.os.interprocess.NamedMutexImpl(identity);
        }

        public framework.os.interprocess.NamedMonitor createNamedMonitor(global::framework.os.interprocess.NamedItemIdentifier identity) {
            return new global::framework.os.interprocess.NamedMonitorImpl(identity);
        }

        public framework.os.interprocess.NamedMemory createNamedMemory(global::framework.os.interprocess.NamedItemIdentifier identity, global::framework.basicTypes.PositiveInteger numberOfElements, global::framework.basicTypes.PositiveInteger elementSize) {
            return new global::framework.os.interprocess.NamedMemoryImpl(identity, numberOfElements, elementSize);
        }

        public framework.os.interprocess.NamedSemaphore createNamedSemaphore(global::framework.os.interprocess.NamedItemIdentifier identity, PositiveInteger initialCount, PositiveInteger maximumCount) {
            return new global::framework.os.interprocess.NamedSemaphoreImpl(identity, initialCount, maximumCount);
        }

        #endregion

        public void Dispose() {
            OsRef.actualOs = null;
        }
    }

}
