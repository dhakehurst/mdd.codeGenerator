/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.os.threading {
    using framework.basicTypes;
	public interface Threading
	
	{
	  // --- Receptions ---
	
	  // --- Operations ---
        void threadSleep(global::framework.time.Duration duration);
        global::framework.os.threading.Thread createThread(global::framework.basicTypes.String threadName);
		global::framework.os.threading.Mutex createMutex();
        global::framework.os.threading.Monitor createMonitor();
        global::framework.os.threading.Semaphore createSemaphore(PositiveInteger initialCount, PositiveInteger maximumCount);
	
	  // --- Properties ---
	
	}

}
