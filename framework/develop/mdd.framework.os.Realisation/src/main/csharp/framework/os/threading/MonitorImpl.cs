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


    public class MonitorImpl : Monitor {
        public MonitorImpl() {
            this.autoResetEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset);
        }

        System.Threading.EventWaitHandle autoResetEvent;

        #region Monitor Members

        public void waitOn() {
            bool res = this.autoResetEvent.WaitOne();
        }

        public void waitOn(framework.time.Duration timeout) {
            bool res = this.autoResetEvent.WaitOne((int)timeout.asMilliseconds.to_Double());
            if (!res) {
                throw new global::framework.os.threading.TimeoutException("Timeout waiting for Monitor ");
            }
        }

        public void notifyOne() {
            bool res = this.autoResetEvent.Set();
        }

        public void notifyAll() {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}
