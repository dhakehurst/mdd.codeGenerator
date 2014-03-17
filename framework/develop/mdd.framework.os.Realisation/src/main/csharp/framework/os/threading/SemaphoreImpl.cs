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

    public class SemaphoreImpl : Semaphore {
        public SemaphoreImpl(PositiveInteger initialCount, PositiveInteger maximumCount) {
            this.semaphore = new System.Threading.Semaphore(initialCount.to_Int32(), maximumCount.to_Int32());
        }

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

        public override string ToString() {
            bool gotIt = this.semaphore.WaitOne(0);
            if (gotIt) this.semaphore.Release();
            return gotIt ? "Acquirable" : "Maxed";
        }
    }
}
