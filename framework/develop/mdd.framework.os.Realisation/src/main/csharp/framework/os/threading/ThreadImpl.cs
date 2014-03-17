/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.os.threading
{
    public class ThreadImpl : Thread
    {
        public ThreadImpl(global::framework.basicTypes.String threadName) {
            this.threadName = threadName;
        }

        global::framework.basicTypes.String threadName;
        public System.Threading.Thread impl;

        public void start(Runnable runnable) {
            this.impl = new System.Threading.Thread(new System.Threading.ThreadStart(runnable.run));
            this.impl.Name = this.threadName.to_string();
            this.impl.Start();
        }

        public void join() {
            this.impl.Join();
        }

        public void interrupt() {
            this.impl.Interrupt();
        }

        public global::framework.basicTypes.Boolean isRunning {
            get { return this.impl.ThreadState == System.Threading.ThreadState.Running; }
        }
    }
}
