/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.io.fileSystem
{
    using framework.basicTypes;

    public class ActualFileSystem : FileSystem
    {
        public ActualFileSystem() {
            if (null == FileSystemRef.actualFileSystem) {
                FileSystemRef.actualFileSystem = this;
            } else {
                throw new Exception("FileSystem object already instantiated, you can have only one (did you 'Dispose' the old one).");
            }
        }

        public void Dispose() {
            FileSystemRef.actualFileSystem = null;
        }

        public File createFile(PathName fullPathName) {
            return new FileImpl(fullPathName);
        }
    }
}
