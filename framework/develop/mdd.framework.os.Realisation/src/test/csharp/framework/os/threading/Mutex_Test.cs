/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.os.threading.test
{

    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using global::framework.basicTypes;
    using global::framework.os.interprocess;
    using global::framework.os.threading;

    [TestFixture]
    public class Mutex_Test
    {

        [SetUp]
        public void MyTestInitialize() {
            this.os = new global::framework.os.dotNet_4_0.OsImpl();
        }

        [TearDown]
        public void MyTestCleanup() {
            global::framework.os.OsRef.actualOs = null;
        }


        global::framework.os.dotNet_4_0.OsImpl os;

        class Lockit : Runnable
        {
            global::framework.os.OsRef os;
            public Lockit(Mutex mutex) {
                this.os = new global::framework.os.OsRef();
                this.mutex = mutex;
            }
            Mutex mutex;
            bool running = true;
            public bool use = false;
            public void run() {
                if (use) {
                    this.useIt();
                } else {
                    this.lockIt();
                }
            }
            void lockIt() {
                this.mutex.lock_();
                while (this.running) {
                    os.threadSleep(new global::framework.time.DurationMilliseconds(10));
                }
                this.mutex.unlock();
            }
            void useIt() {
                this.mutex.use(()=>{
                    while (this.running) {
                        os.threadSleep(new global::framework.time.DurationMilliseconds(10));
                    }
                });
            }
            public void stop() {
                this.running = false;
            }
        }
        Lockit lockit;

        void lockIt(Mutex mutex) {
            this.lockit = new Lockit(mutex);
            Thread lockIt_thread = os.createThread("lockIt");
            lockIt_thread.start(this.lockit);
            //sleep so that started thread get activated before continuing
            os.threadSleep(new global::framework.time.DurationMilliseconds(10));
        }

        void useIt(Mutex mutex) {
            this.lockit = new Lockit(mutex);
            this.lockit.use = true;
            Thread lockIt_thread = os.createThread("useIt");
            lockIt_thread.start(this.lockit);
            //sleep so that started thread get activated before continuing
            os.threadSleep(new global::framework.time.DurationMilliseconds(10));
        }

        [Test]
        public void createMutex() {
           Mutex mux = this.os.createMutex();
        }

        [Test]
        public void lock__100_timeout() {
            Mutex mux = this.os.createMutex();

            lockIt(mux);
            try {
                mux.lock_(new global::framework.time.DurationMilliseconds(100));
                Assert.Fail();
            } catch (TimeoutException ex) {
                //passed
            } finally {

                this.lockit.stop();
            }
        }

        [Test]
        public void use__100_timeout() {
            Mutex mux = this.os.createMutex();
            useIt(mux);
            try {
                mux.use(new global::framework.time.DurationMilliseconds(100), ()=>{
                    Assert.Fail();
                });
            } catch (TimeoutException ex) {
                //passed
                int i = 0;
            } finally {

                this.lockit.stop();
            }
        }

    }
}
