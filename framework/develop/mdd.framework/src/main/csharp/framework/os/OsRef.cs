/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.os {

	using global::framework.basicTypes;
	using global::framework.collections;

	// Class
	public class OsRef
    :
      global::framework.basicTypes.Factory,
      global::framework.collections.Factory,
      global::framework.os.threading.Threading,
      global::framework.os.interprocess.Interprocess
	
	{
	
	    public OsRef() {
	    }
	
		private static global::framework.os.OS _actualOs;
        public static global::framework.os.OS actualOs {
            get { return OsRef._actualOs; }
            set { OsRef._actualOs = value; }
		}

        #region basicType.Factory
        public String String(string value) {
            return new String(value);
        }

        public Boolean Boolean(bool value) {
            return new Boolean(value);
        }

        public Real Real(double value) {
            return new Real(value);
        }

        public Integer Integer(long value) {
            return new Integer(value);
        }

        public PositiveInteger PositiveInteger(ulong value) {
            return new PositiveInteger(value);
        }
        #endregion

        #region collections Factory
        public Sequence<T> Sequence<T>() {
            return OsRef.actualOs.Sequence<T>();
        }
        public Sequence<T> Sequence<T>(T[] array) {
            return OsRef.actualOs.Sequence(array);
        }
        public Sequence<T> Sequence<T>(System.Collections.Generic.IEnumerable<T> enumerable) {
            return OsRef.actualOs.Sequence(enumerable);
        }
        public Sequence<BitString8> SequenceOfBitString8<T>(T structure) where T : struct {
            return OsRef.actualOs.SequenceOfBitString8(structure);
        }

        public T Structure<T>(Sequence<BitString8> seq) where T : struct {
            return OsRef.actualOs.Structure<T>(seq);
        }

        public Set<T> Set<T>() { return OsRef.actualOs.Set<T>();  }
        public Set<T> Set<T>(System.Collections.Generic.IEnumerable<T> enumerable) { return OsRef.actualOs.Set<T>(enumerable); }
        #endregion

        #region Threading Members
        public void threadSleep(global::framework.time.Duration duration) {
            OsRef.actualOs.threadSleep(duration);
        }
        public global::framework.os.threading.Thread createThread(global::framework.basicTypes.String threadName) {
            return OsRef.actualOs.createThread(threadName);
        }

        public global::framework.os.threading.Mutex createMutex() {
            return OsRef.actualOs.createMutex();
        }

        public global::framework.os.threading.Monitor createMonitor() {
            return OsRef.actualOs.createMonitor();
        }

        public global::framework.os.threading.Semaphore createSemaphore(PositiveInteger initialCount, PositiveInteger maximumCount) {
            return OsRef.actualOs.createSemaphore(initialCount, maximumCount);
        }

        #endregion

        #region Interprocess Members

        public global::framework.os.interprocess.Process createProcess(global::framework.io.fileSystem.PathName pathName) {
            return OsRef.actualOs.createProcess(pathName);
        }

        public global::framework.os.interprocess.NamedMutex createNamedMutex(global::framework.os.interprocess.NamedItemIdentifier identity) {
            return OsRef.actualOs.createNamedMutex(identity);
        }

        public global::framework.os.interprocess.NamedMonitor createNamedMonitor(global::framework.os.interprocess.NamedItemIdentifier identity) {
            return OsRef.actualOs.createNamedMonitor(identity);
        }

        public global::framework.os.interprocess.NamedSemaphore createNamedSemaphore(global::framework.os.interprocess.NamedItemIdentifier identity, global::framework.basicTypes.PositiveInteger initialCount, global::framework.basicTypes.PositiveInteger maximumCount){
            return OsRef.actualOs.createNamedSemaphore(identity, initialCount, maximumCount);
        }

        public global::framework.os.interprocess.NamedMemory createNamedMemory(global::framework.os.interprocess.NamedItemIdentifier identity, global::framework.basicTypes.PositiveInteger numberOfElements, global::framework.basicTypes.PositiveInteger elementSize) {
            return OsRef.actualOs.createNamedMemory(identity, numberOfElements, elementSize);
        }

        #endregion
    }

} //namespace framework.os
