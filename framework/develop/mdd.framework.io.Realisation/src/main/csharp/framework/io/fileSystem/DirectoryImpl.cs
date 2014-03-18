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

    class DirectoryImpl : Directory
    {
        public DirectoryImpl(PathName fullPathName) {
            this._fullPathName = fullPathName;
            this.create();
        }

        PathName _fullPathName;
        public PathName fullPathName {
            get { return _fullPathName; }
        }

        public Boolean exists {
            get { return System.IO.Directory.Exists(this.fullPathName.to_string()); }
        }
        
        public void create() {
            if (this.exists.not()) {
                System.IO.Directory.CreateDirectory(fullPathName.to_string());
            }
        }

        public void delete() {
            System.IO.Directory.Delete(fullPathName.to_string());
        }

    }
}
