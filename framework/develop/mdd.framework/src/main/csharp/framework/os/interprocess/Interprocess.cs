/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.os.interprocess {

	public interface Interprocess
	
	{
	  // --- Receptions ---
	
	  // --- Operations ---
        global::framework.os.interprocess.Process createProcess(global::framework.io.fileSystem.PathName pathName);
        global::framework.os.interprocess.NamedMutex createNamedMutex(global::framework.os.interprocess.NamedItemIdentifier identity);
        global::framework.os.interprocess.NamedMonitor createNamedMonitor(global::framework.os.interprocess.NamedItemIdentifier identity);
        global::framework.os.interprocess.NamedSemaphore createNamedSemaphore(global::framework.os.interprocess.NamedItemIdentifier identity, global::framework.basicTypes.PositiveInteger initialCount, global::framework.basicTypes.PositiveInteger maximumCount);
        global::framework.os.interprocess.NamedMemory createNamedMemory(global::framework.os.interprocess.NamedItemIdentifier identity, global::framework.basicTypes.PositiveInteger numberOfElements, global::framework.basicTypes.PositiveInteger elementSize);
	
	  // --- Properties ---
	
	}

}
