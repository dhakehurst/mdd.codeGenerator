/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.io.fileSystem {

	using global::framework.basicTypes;

	// Class
	public class FileSystemRef : FileSystem
	{
	    public FileSystemRef() {

	    }

        private static FileSystem _actualFileSystem;
        public static FileSystem actualFileSystem {
            get { return FileSystemRef._actualFileSystem; }
            set { FileSystemRef._actualFileSystem = value; }
        }

        public File createFile(PathName fullPathName) {
            return actualFileSystem.createFile(fullPathName);
        }
    }

} //namespace framework.io.fileSystem
