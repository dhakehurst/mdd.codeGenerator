/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.os.threading
{

    using global::framework.basicTypes;
    using global::framework.os.interprocess;


    public class MutexImpl : Mutex {
        public MutexImpl() {
            this.mutex = new System.Threading.Mutex(false);
        }

        System.Threading.Mutex mutex;

        #region Mutex Members

        public void lock_() {
            this.mutex.WaitOne();
        }

        public void lock_(global::framework.time.Duration timeout) {
            try {
                bool res = this.mutex.WaitOne(timeout.asMilliseconds.truncate().to_Int32());
                if (!res) {
                    throw new global::framework.os.threading.TimeoutException("Timeout waiting for Mutex ");
                }
            } catch (global::System.Threading.AbandonedMutexException ex) {
                throw new ThreadingException(ex.Message);
            }
        }

        public void unlock() {
            this.mutex.ReleaseMutex();
        }

        public Boolean tryLock() {
            bool b = this.mutex.WaitOne(0);
            return b;
        }

        #endregion


        public void use(System.Action exclusiveCode) {
            this.lock_();
            try {

                exclusiveCode.Invoke();

            } finally {
            this.unlock();
            }
        }

        public void use(time.Duration timeout, System.Action exclusiveCode) {
            this.lock_(timeout);
            try {

                exclusiveCode.Invoke();

            } finally {
                this.unlock();
            }
        }
    }
}
