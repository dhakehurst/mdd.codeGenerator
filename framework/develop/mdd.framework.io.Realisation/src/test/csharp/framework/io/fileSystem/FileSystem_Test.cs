/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.io.fileSystem.test
{

    using NUnit.Framework;
    using global::framework.basicTypes;
    using global::framework.collections;
    using global::framework.io.fileSystem;

    [TestFixture]
    public class FileSystem_Test
    {

        [SetUp]
        public void setUp() {
            this.os = new global::framework.os.dotNet_4_0.OsImpl();
            FileSystem fileSystem = new ActualFileSystem();
        }

        [TearDown]
        public void tearDown() {
            global::framework.os.OsRef.actualOs = null;
            FileSystemRef.actualFileSystem = null;
        }


        global::framework.os.dotNet_4_0.OsImpl os;

        [Test]
        public void exists__false() {
            
            FileSystemRef fs = new FileSystemRef();

            File file = fs.createFile(new PathName("./testFolder/testFile.txt"));
            Assert.AreEqual(false, file.exists.to_Boolean());

        }


        [Test]
        public void write() {
            FileSystemRef fs = new FileSystemRef();

            File file = fs.createFile(new PathName("./testFolder/testFile.txt"));
            Assert.AreEqual(false, file.exists.to_Boolean());

            file.write("Hello");

            Assert.AreEqual(true, file.exists.to_Boolean());

            file.delete();
        }
        
        [Test]
        public void delete() {

            FileSystemRef fs = new FileSystemRef();

            File file = fs.createFile(new PathName("./testFolder/testFile.txt"));
            Assert.AreEqual(false, file.exists.to_Boolean());

            file.write("Hello");

            Assert.AreEqual(true, file.exists.to_Boolean());

            file.delete();
            Assert.AreEqual(false, file.exists.to_Boolean());

        }

        [Test]
        public void delete_then_recreate() {

            FileSystemRef fs = new FileSystemRef();

            File file = fs.createFile(new PathName("./testFolder/testFile.txt"));
            Assert.AreEqual(false, file.exists.to_Boolean());

            file.write("Hello");

            Assert.AreEqual(true, file.exists.to_Boolean());

            file.delete();
            Assert.AreEqual(false, file.exists.to_Boolean());

            file.write("Hello");

            Assert.AreEqual(true, file.exists.to_Boolean());

            file.delete();
            Assert.AreEqual(false, file.exists.to_Boolean());
        }
    }
}
