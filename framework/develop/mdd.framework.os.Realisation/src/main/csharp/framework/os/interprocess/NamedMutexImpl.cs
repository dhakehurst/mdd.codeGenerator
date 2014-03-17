/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.os.interprocess
{

    using global::framework.basicTypes;
    using global::framework.os.interprocess;


    public class NamedMutexImpl : NamedMutex {
        public NamedMutexImpl(NamedItemIdentifier identity) {
            this.identity = identity;

            this.mutex = new System.Threading.Mutex(false, identity.to_string());
        }

        NamedItemIdentifier identity;

        System.Threading.Mutex mutex;

        #region Mutex Members

        public void lock_() {
            this.mutex.WaitOne();
        }

        public void lock_(global::framework.time.Duration timeout) {
            try {
                bool res = this.mutex.WaitOne(timeout.asMilliseconds.truncate().to_Int32());
                if (!res) {
                    throw new global::framework.os.threading.TimeoutException("Timeout waiting for NamedMutex " + this.identity.to_string());
                }
            } catch (global::System.Threading.AbandonedMutexException ex) {
                throw new InterprocessException(ex.Message);
            }
        }

        public void unlock() {
            this.mutex.ReleaseMutex();
        }

        public Boolean tryLock() {
            bool b = this.mutex.WaitOne(0);
            return b;
        }

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

        #endregion
    }
}
