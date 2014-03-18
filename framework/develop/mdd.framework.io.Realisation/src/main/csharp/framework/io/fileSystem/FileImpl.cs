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

    public class FileImpl : File
    {
        public FileImpl(PathName fullPathName) {
            this._fullPathName = fullPathName;
            this._fileInfo = new System.IO.FileInfo(fullPathName.to_string());
        }

        System.IO.FileInfo _fileInfo;
        System.IO.FileInfo fileInfo {
            get {
                this._fileInfo.Refresh();
                return this._fileInfo;
            }
        }

        PathName _fullPathName;
        public PathName fullPathName {
            get { return _fullPathName; }
        }

        public Boolean exists {
            get { return this.fileInfo.Exists; }
        }

        public void delete() {
            if (this.exists) {
                this.fileInfo.Delete();
            }
        }

        public void create() {
            if (null!=this.directory && this.directory.exists.not()) {
                this.directory.create();
            }
            this.fileInfo.Create().Close();
        }

        public Directory directory {
            get {
                string dirName = System.IO.Path.GetDirectoryName(this.fullPathName.to_string());
                if ("".Equals(dirName)) {
                    return null;
                } else {
                    return new DirectoryImpl(new PathName( dirName ));
                }
            }
        }

        #region Writer
        public void write(String txt) {
            if (this.exists.not()) {
                this.create();
            }
            System.IO.FileStream fs = this.to_fileStream();
            byte[] info = new System.Text.UTF8Encoding(true).GetBytes(txt.to_string());
            fs.Write(info, 0, info.Length);
            fs.Close();
        }

        public void writeLine(String txt) {
            if (this.exists.not()) {
                this.create();
            }
            System.IO.FileStream fs = this.to_fileStream();
            byte[] info = new System.Text.UTF8Encoding(true).GetBytes(txt.to_string() + System.Environment.NewLine);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }

        public void write(collections.Sequence<BitString> bits) {
            if (this.exists.not()) {
                this.create();
            }
            //System.IO.FileStream fs = this.to_fileStream();
            throw new System.NotImplementedException();
            //w.Close();
        }
        #endregion

        #region
        public System.IO.FileStream to_fileStream() {
            return new System.IO.FileStream(this.fullPathName.to_string(), System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite, 1024, System.IO.FileOptions.WriteThrough);
        }
        #endregion
    }
}
