/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace framework.os.interprocess
{

    public class ProcessImpl : global::framework.os.interprocess.Process {

        public ProcessImpl(global::framework.io.fileSystem.PathName pathName) {
            this.pathName = pathName;
            this.impl = new System.Diagnostics.Process();
            this.impl.StartInfo.FileName = pathName.to_string();
        }

        global::framework.io.fileSystem.PathName pathName;

        System.Diagnostics.Process impl;
        

        #region Process Members

        public void start(global::System.Collections.Generic.IList<global::framework.basicTypes.String> arguments) {
            this.impl.StartInfo.Arguments = arguments.Aggregate("", (sum,s)=> sum + " " + s.to_string() );
            this.impl.Start();
        }

        public void stop() {
            throw new System.NotImplementedException();
        }

        public void join() {
            this.impl.WaitForExit();
        }

        public void interrupt() {
            throw new System.NotImplementedException();
        }

        public void kill() {
            this.impl.Kill();
        }

        #endregion
    }
}
