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


    public class NamedSemaphoreImpl : NamedSemaphore {
        public NamedSemaphoreImpl(NamedItemIdentifier identity, PositiveInteger initialCount, PositiveInteger maximumCount) {
            this.identity = identity;

            this.semaphore = new System.Threading.Semaphore(initialCount.to_Int32(), maximumCount.to_Int32(), identity.to_string());
        }

        NamedItemIdentifier identity;

        System.Threading.Semaphore semaphore;

        #region Semaphore Members

        public void acquire() {
            this.semaphore.WaitOne();
        }

        public void acquire(global::framework.time.Duration timeout) {
            this.semaphore.WaitOne(timeout.asMilliseconds.asInteger().to_Int32());
        }

        public void release() {
            try {
                this.semaphore.Release();
            } catch (System.Threading.SemaphoreFullException) {
                // don't worry its already released to the max!
            }
        }

        #endregion
    }
}
