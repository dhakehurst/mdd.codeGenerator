/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.persistence.xml
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using framework.basicTypes;
    using framework.collections;

    public class XmlPersistence : PersistenceStore, System.IDisposable
    {
        global::framework.os.OsRef os;
        framework.io.fileSystem.FileSystem fs;
        global::framework.logging.LoggerRef log;

        public XmlPersistence(PersistenceStoreIdentity id, string fileName) {
            this.identity = id;
            this.os = new framework.os.OsRef();
            this.fs = new framework.io.fileSystem.FileSystemRef();
            this.log = new global::framework.logging.LoggerRef(new global::framework.logging.Location("framework.persistence.xml.XmlPersistence"));

            if (PersistenceRef.actualPersistenceStore.ContainsKey(id)) {
                throw new PersistenceException("Can have only one store with the identity " + id +"(did you 'Dispose' the old one).");
            }

            //should be passing some file options here!
            this.file = fs.createFile(new framework.io.fileSystem.PathName(fileName));

            this.settings = new System.Xml.XmlWriterSettings {
                NewLineHandling = System.Xml.NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true,
                IndentChars = "  ",
                NewLineChars = System.Environment.NewLine
            };

            if (this.file.exists) {
                System.IO.FileStream fstr = this.file.to_fileStream();
                try {
                    this.impl = XDocument.Load(fstr);
                } finally {
                    fstr.Close();
                }
                log.info(new logging.Message("Loaded file: " + fileName));
            } else {
                this.impl = new XDocument();
                log.error(new logging.Message("File not found, created new file: " + fileName));
            }

            PersistenceRef.actualPersistenceStore.Add(id, this);
        }

        public void Dispose() {
            PersistenceRef.actualPersistenceStore.Remove(this.identity);
        }

        public void save() {
            //delete old file
            this.file.delete();
            //create new one
            this.file.create();
            log.debug(new logging.Message("Created file: " + this.file.fullPathName));
            System.IO.FileStream fs = this.file.to_fileStream();
            System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(fs, this.settings);
            this.impl.Save(writer);
            writer.Close();
            fs.Close();
            log.error(new logging.Message("File saved: " + this.file.fullPathName));
            fs.Dispose();
        }

        framework.io.fileSystem.File file;
        //string fileName;
        PersistenceStoreIdentity identity;
        XDocument impl;
        System.Xml.XmlWriterSettings settings;

        String getXPathAttribute(PersistenceItemIdentity itemId) {
            if (itemId.contains(".")) {
                basicTypes.String xpath = itemId.replaceAll(".", "/");
                xpath = xpath.replaceLast("/", "/@");
                xpath = xpath.replaceFirst("self", ".");
                return xpath;
            } else {
                if (itemId.equalTo("self")) {
                    return ".";
                } else {
                    return "@" + itemId;
                }
            }
        }

        String getXPathElement(PersistenceItemIdentity itemId) {
            basicTypes.String xpath = itemId.replaceAll(".", "/");
            xpath = xpath.replaceFirst("self", ".");
            return xpath;
        }

        XElement createOrFetchElement(XNode element, PersistenceItemIdentity itemId) {
            List<String> names = itemId.split(".").array.ToList();
            return this.createOrFetchElement(element, names) as XElement;
        }

        XElement createOrFetchElement(XNode parent, List<String> nameList) {
            if (0 == nameList.Count) {
                return parent as XElement;
            } else {
                XElement child = this.createOrFetchElement(parent, nameList[0]);
                return this.createOrFetchElement(child, nameList.GetRange(1, nameList.Count - 1));
            }
        }

        XElement createOrFetchElement(XNode parent, String name) {
            IEnumerable<object> col = (IEnumerable<object>)parent.XPathEvaluate(name.to_string());
            XElement child = col.Cast<XElement>().FirstOrDefault();
            if (null == child) {
                child = new XElement(name.to_string());
                if (parent is XContainer) {
                    (parent as XContainer).Add(child);
                }
            }
            return child;
        }

        XElement createElement(XNode parent, String name) {
            XElement child = new XElement(name.to_string());
            (parent as XElement).Add(child);

            return child;
        }

        XAttribute createOrFetchAttribute(XNode root, List<String> nameList) {
            if (0 == nameList.Count) {
                throw new PersistenceException("Can't create XAttribute with empty name");
            } else if (1 == nameList.Count) {
                string n = nameList[0].to_string();
                XAttribute att = (root as XElement).Attribute(n);
                if (null == att) {
                    att = new XAttribute(n, "");
                    (root as XElement).Add(att);
                }
                return att;
            } else {
                XElement child = this.createOrFetchElement(root, nameList[0]);
                return this.createOrFetchAttribute(child, nameList.GetRange(1, nameList.Count - 1));
            }
        }

        XAttribute createOrFetchAttribute(XNode root, PersistenceItemIdentity itemId) {
            List<String> names = itemId.split(".").array.ToList();
            return this.createOrFetchAttribute(root, names);
        }

        basicTypes.Boolean contains(XNode root, PersistenceItemIdentity itemId) {

            IEnumerable<object> col = (IEnumerable<object>)root.XPathEvaluate(this.getXPathAttribute(itemId).to_string());
            return (0 < col.Count());
        }

        basicTypes.Boolean fetchBoolean(XNode root, PersistenceItemIdentity itemId) {
            String s = this.fetchString(root, itemId);
            return null == s ? null : s.asBoolean();
        }

        basicTypes.Boolean fetchBoolean(XNode root, PersistenceItemIdentity itemId, basicTypes.Boolean default_) {
            String s = this.fetchString(root, itemId);
            if (null == s) {
                log.debug(new logging.Message("Using default value: " + default_ + " for " + itemId));
                return default_;
            } else {
                return s.asBoolean();
            }
        }

        void putBoolean(XNode root, PersistenceItemIdentity itemId, Boolean value) {
            putString(root, itemId, value.ToString());
        }

        basicTypes.Integer fetchInteger(XNode root, PersistenceItemIdentity itemId) {
            String s = this.fetchString(root, itemId);
            return null == s ? null : s.asInteger();
        }

        basicTypes.Integer fetchInteger(XNode root, PersistenceItemIdentity itemId, basicTypes.Integer default_) {
            String s = this.fetchString(root, itemId);
            if (null == s) {
                log.debug(new logging.Message("Using default value: " + default_ + " for " + itemId));
                return default_;
            } else {
                return s.asInteger();
            }
        }

        void putInteger(XNode root, PersistenceItemIdentity itemId, Integer value) {
            if (null != value) {
                putString(root, itemId, value.ToString());
            }
        }

        basicTypes.PositiveInteger fetchPositiveInteger(XNode root, PersistenceItemIdentity itemId) {
            String s = this.fetchString(root, itemId);
            return null == s ? null : s.asPositiveInteger();
        }
        basicTypes.PositiveInteger fetchPositiveInteger(XNode root, PersistenceItemIdentity itemId, basicTypes.PositiveInteger default_) {
            String s = this.fetchString(root, itemId);
            if (null == s) {
                log.debug(new logging.Message("Using default value: " + default_ + " for " + itemId));
                return default_;
            } else {
                return s.asPositiveInteger();
            }
        }

        void putPositiveInteger(XNode root, PersistenceItemIdentity itemId, PositiveInteger value) {
            putString(root, itemId, value.ToString());
        }

        basicTypes.Real fetchReal(XNode root, PersistenceItemIdentity itemId) {
            String s = this.fetchString(root, itemId);
            return null == s ? null : s.asReal();
        }
        basicTypes.Real fetchReal(XNode root, PersistenceItemIdentity itemId, basicTypes.Real default_) {
            String s = this.fetchString(root, itemId);
            if (null == s) {
                log.debug(new logging.Message("Using default value: " + default_ + " for " + itemId));
                return default_;
            } else {
                return s.asReal();
            }
        }

        void putReal(XNode root, PersistenceItemIdentity itemId, Real value) {
            if (null != value) {
                putString(root, itemId, value.ToString());
            }
        }

        basicTypes.String fetchString(XNode root, PersistenceItemIdentity itemId) {
            string xpath = this.getXPathAttribute(itemId).to_string();
            IEnumerable<object> col = (IEnumerable<object>)root.XPathEvaluate(xpath);
            object f = col.FirstOrDefault();
            if (null == f) {
                return null;
            } else if (f is XAttribute) {
                XAttribute att = f as XAttribute;
                return new basicTypes.String(att.Value);
            } else if (f is XElement) {
                XElement el = f as XElement;
                return new basicTypes.String(el.Attribute("value").Value);
            } else {
                return null;
            }
        }
        basicTypes.String fetchString(XNode root, PersistenceItemIdentity itemId, basicTypes.String default_) {
            string xpath = this.getXPathAttribute(itemId).to_string();
            IEnumerable<object> col = (IEnumerable<object>)root.XPathEvaluate(xpath);
            object f = col.FirstOrDefault();
            if (null == f) {
                log.debug(new logging.Message("Using default value: " + default_ + " for " + itemId));
                return default_;
            } else if (f is XAttribute) {
                XAttribute att = f as XAttribute;
                return new basicTypes.String(att.Value);
            } else if (f is XElement) {
                XElement el = f as XElement;
                return new basicTypes.String(el.Attribute("value").Value);
            } else {
                log.debug(new logging.Message("Using default value: " + default_ + " for " + itemId));
                return default_;
            }
        }
        void putString(XNode root, PersistenceItemIdentity itemId, String value) {
            if (null != value) {
                string xpath = this.getXPathAttribute(itemId).to_string();
                IEnumerable<object> col = (IEnumerable<object>)root.XPathEvaluate(xpath);
                XAttribute att = this.createOrFetchAttribute(root, itemId);
                att.Value = value.to_string();
            }
        }

        basicTypes.BitString8? fetchBitString8(XNode root, PersistenceItemIdentity itemId) {
            String s = this.fetchString(root, itemId);
            if (null == s) { return null; } else { return s.asBitString8Hex(); }
        }
        basicTypes.BitString8? fetchBitString8(XNode root, PersistenceItemIdentity itemId, basicTypes.BitString8 default_) {
            String s = this.fetchString(root, itemId);
            if (null == s) {
                log.debug(new logging.Message("Using default value: " + default_ + " for " + itemId));
                return default_;
            } else {
                return s.asBitString8Hex();
            }
        }

        basicTypes.BitString16? fetchBitString16(XNode root, PersistenceItemIdentity itemId) {
            String s = this.fetchString(root, itemId);
            if (null == s) { return null; } else { return s.asBitString16Hex(); }
        }
        basicTypes.BitString32? fetchBitString32(XNode root, PersistenceItemIdentity itemId) {
            String s = this.fetchString(root, itemId);
            if (null == s) { return null; } else { return s.asBitString32Hex(); }
        }
        basicTypes.BitString64? fetchBitString64(XNode root, PersistenceItemIdentity itemId) {
            String s = this.fetchString(root, itemId);
            if (null == s) { return null; } else { return s.asBitString64Hex(); }
        }


        void putBitString(XNode root, PersistenceItemIdentity itemId, BitString value) {
            throw new System.NotImplementedException();
        }

        basicTypes.DateTime fetchDateTime(XNode root, PersistenceItemIdentity itemId) {
            throw new System.NotImplementedException();
        }
        basicTypes.DateTime fetchDateTime(XNode root, PersistenceItemIdentity itemId, basicTypes.DateTime default_) {
            throw new System.NotImplementedException();
        }
        void putDateTime(XNode root, PersistenceItemIdentity itemId, DateTime value) {
            throw new System.NotImplementedException();
        }

        internal T? fetchEnum<T>(XNode root, PersistenceItemIdentity itemId) where T : struct {
            IEnumerable<object> col = (IEnumerable<object>)root.XPathEvaluate(this.getXPathAttribute(itemId).to_string());
            XAttribute att = col.Cast<XAttribute>().FirstOrDefault();
            if (att == null) {
                return null;
            } else {
                System.Type type = typeof(T);

                object eObj = System.Enum.Parse(type, att.Value);

                return eObj as T?;
            }
        }
        internal T? fetchEnum<T>(XNode root, PersistenceItemIdentity itemId, T default_) where T : struct {
            IEnumerable<object> col = (IEnumerable<object>)root.XPathEvaluate(this.getXPathAttribute(itemId).to_string());
            XAttribute att = col.Cast<XAttribute>().FirstOrDefault();
            if (att == null) {
                log.debug(new logging.Message("Using default value: " + default_ + " for " + itemId));
                return default_;
            } else {
                System.Type type = typeof(T);
                object eObj = System.Enum.Parse(type, att.Value);
                return eObj as T?;
            }
        }
        void putEnum2<T>(XNode root, PersistenceItemIdentity itemId, T? value) where T : struct {
            if (null != value) {
                putString(root, itemId, value.ToString());
            }
        }

        System.Type getRootFrameworkType(System.Type type) {
            if (null == type) return null;
            if (typeof(object) == type) {
                return null;
            } else if (type.FullName.StartsWith("framework.basicTypes")) {
                return type;
            } else {
                return this.getRootFrameworkType(type.BaseType);
            }

        }

        object fetchBitStringObject(System.Type type, XNode root, PersistenceItemIdentity itemId) {
            if (typeof(BitString8) == type) {
                return this.fetchBitString8(root, itemId);
            } else if (typeof(BitString16) == type) {
                return this.fetchBitString16(root, itemId);
            } if (typeof(BitString32) == type) {
                return this.fetchBitString32(root, itemId);
            } if (typeof(BitString64) == type) {
                return this.fetchBitString64(root, itemId);
            } else {
                return null;
            }
        }

        internal T fetchObject<T>(XNode root, PersistenceItemIdentity itemId) where T : class {
            System.Type type = typeof(T);
            System.Type rootFwType = getRootFrameworkType(type);

            if (typeof(Boolean) == type) {
                return this.fetchBoolean(root, itemId) as T;
            } else if (typeof(Integer) == type) {
                return this.fetchInteger(root, itemId) as T;
            } else if (typeof(PositiveInteger) == type) {
                return this.fetchPositiveInteger(root, itemId) as T;
            } else if (typeof(String) == type) {
                return this.fetchString(root, itemId) as T;
            } else if (typeof(Real) == type) {
                return this.fetchReal(root, itemId) as T;
            } else if (typeof(BitString) == type) {
                return fetchBitStringObject(type, root, itemId) as T;
            } else if (typeof(Boolean) == rootFwType) {
                System.Reflection.ConstructorInfo cinf = type.GetConstructor(new System.Type[] { typeof(Boolean) });
                Boolean v = this.fetchBoolean(root, itemId);
                if (null == v) {
                    return null;
                    //throw new PersistenceException("Path not found: " + itemId);
                }
                T obj = (T)cinf.Invoke(new object[] { v });
                return obj;
            } else if (typeof(Integer) == rootFwType) {
                System.Reflection.ConstructorInfo cinf = type.GetConstructor(new System.Type[] { typeof(Integer) });
                Integer v = this.fetchInteger(root, itemId);
                if (null == v) {
                    return null;
                    //throw new PersistenceException("Path not found: " + itemId);
                }
                T obj = (T)cinf.Invoke(new object[] { v });
                return obj;
            } else if (typeof(PositiveInteger) == rootFwType) {
                System.Reflection.ConstructorInfo cinf = type.GetConstructor(new System.Type[] { typeof(PositiveInteger) });
                PositiveInteger v = this.fetchPositiveInteger(root, itemId);
                if (null == v) {
                    return null;
                    //throw new PersistenceException("Path not found: " + itemId);
                }
                T obj = (T)cinf.Invoke(new object[] { v });
                return obj;
            } else if (typeof(String) == rootFwType) {
                System.Reflection.ConstructorInfo cinf = type.GetConstructor(new System.Type[] { typeof(String) });
                String v = this.fetchString(root, itemId);
                if (null == v) {
                    return null;
                    //throw new PersistenceException("Path not found: " + itemId);
                }
                T obj = (T)cinf.Invoke(new object[] { v });
                return obj;
            } else if (typeof(BitString) == rootFwType) {
                return fetchBitStringObject(rootFwType, root, itemId) as T;
            } else if (typeof(Real) == rootFwType) {
                System.Reflection.ConstructorInfo cinf = type.GetConstructor(new System.Type[] { typeof(double) });
                //System.Reflection.ConstructorInfo cinf = type.GetConstructor(new System.Type[] { typeof(Real) });
                Real v = this.fetchReal(root, itemId);
                if (null == v) {
                    return null;
                    //throw new PersistenceException("Path not found: " + itemId);
                }
                T obj = (T)cinf.Invoke(new object[] { v.to_Double() });
                return obj;
            } if (type.IsGenericType && (typeof(Sequence<>) == type.GetGenericTypeDefinition())) {
                System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("fetchSequence", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { typeof(XNode), typeof(PersistenceItemIdentity) }, null);
                System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(type.GetGenericArguments()[0]);
                object v = genericMeth.Invoke(this, new object[] { root, itemId });
                return v as T;
            } else {

                IEnumerable<object> col = (IEnumerable<object>)root.XPathEvaluate(this.getXPathElement(itemId).to_string());
                XElement el = col.Cast<XElement>().FirstOrDefault();
                if (el == null) {
                    return default(T);
                } else {
                    String objClassName = this.fetchString(el, new PersistenceItemIdentity("_type"));
                    if (null != objClassName) {
                        string cName = objClassName.replaceAll("::", ".").to_string();
                        System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
                        var t = assemblies.Select(a => a.GetType(cName));
                        t = t.Where(tt => null != tt);
                        if (0 < t.Count()) {
                            type = t.First();
                        }
                    }
                    System.Reflection.ConstructorInfo cons = type.GetConstructor(new System.Type[] { });
                    if (null == cons) {
                        throw new PersistenceException("No default constructor found for - " + type.FullName);
                    }
                    T obj = (T)cons.Invoke(new object[] { });
                    foreach (System.Reflection.PropertyInfo prop in type.GetProperties()) {
                        if (prop.PropertyType.IsEnum) {
                            System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("fetchEnum", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { typeof(XNode), typeof(PersistenceItemIdentity) }, null);
                            System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType);
                            object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name) });
                            if (null != v) prop.SetValue(obj, v, null);
                        } if (typeof(BitString).IsAssignableFrom(prop.PropertyType)) {
                            object v = this.fetchBitStringObject(prop.PropertyType, el, new PersistenceItemIdentity(prop.Name));
                            //System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("fetchBitString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { typeof(XNode), typeof(PersistenceItemIdentity) }, null);
                            //System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType);
                            //object v = fetchMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name) });
                            if (null != v) prop.SetValue(obj, v, null);
                        } else if (prop.PropertyType.IsGenericType && typeof(System.Nullable<>) == prop.PropertyType.GetGenericTypeDefinition()) {
                            System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("fetchEnum", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { typeof(XNode), typeof(PersistenceItemIdentity) }, null);
                            System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType.GetGenericArguments()[0]);
                            object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name) });
                            if (null != v) prop.SetValue(obj, v, null);
                        } else if (prop.PropertyType.IsGenericType && (typeof(System.Collections.Generic.IList<>) == prop.PropertyType.GetGenericTypeDefinition() || typeof(System.Collections.Generic.List<>) == prop.PropertyType.GetGenericTypeDefinition())) {
                            System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("fetchList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { typeof(XNode), typeof(PersistenceItemIdentity) }, null);
                            System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType.GetGenericArguments()[0]);
                            object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name) });
                            if (null != v) prop.SetValue(obj, v, null);
                        } else if (prop.PropertyType.IsGenericType && (typeof(Sequence<>) == prop.PropertyType.GetGenericTypeDefinition() || typeof(Sequence<>) == prop.PropertyType.GetGenericTypeDefinition())) {
                            System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("fetchSequence", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { typeof(XNode), typeof(PersistenceItemIdentity) }, null);
                            System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType.GetGenericArguments()[0]);
                            object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name) });
                            if (null != v) prop.SetValue(obj, v, null);
                        } else if (prop.PropertyType.IsGenericType && typeof(System.Collections.Generic.IDictionary<,>) == prop.PropertyType.GetGenericTypeDefinition()) {
                            System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("fetchMap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { typeof(XNode), typeof(PersistenceItemIdentity) }, null);
                            System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType.GetGenericArguments());
                            object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name) });
                            if (null != v) prop.SetValue(obj, v, null);
                        } else {
                            System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("fetchObject", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { typeof(XNode), typeof(PersistenceItemIdentity) }, null);
                            System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType);
                            string relPath = prop.Name;
                            //if (null != getRootFrameworkType(prop.PropertyType)) {
                            //    relPath = relPath;
                            //}
                            object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(relPath) });
                            if (null != v) prop.SetValue(obj, v, null);
                        }
                    }
                    return obj;
                }

            }
        }

        // Named putObject2 to make it easy to find using reflection instead of having to add more complex database style select statements
        internal void putObject2<T>(XNode root, PersistenceItemIdentity itemId, T obj, Boolean createOnly) where T : class {
            if (null == obj) {
                return;
            }
            System.Type type = typeof(T);
            System.Type rootFwType = getRootFrameworkType(type);

            if (typeof(Boolean) == type) {
                this.putBoolean(root, itemId, obj as Boolean);
            } else if (typeof(Integer) == type) {
                this.putInteger(root, itemId, obj as Integer);
            } else if (typeof(PositiveInteger) == type) {
                this.putPositiveInteger(root, itemId, obj as PositiveInteger);
            } else if (typeof(String) == type) {
                this.putString(root, itemId, obj as String);
            } else if (typeof(Real) == type) {
                this.putReal(root, itemId, obj as Real);
            } else if (typeof(BitString) == type) {
                this.putBitString(root, itemId, obj as BitString);
            } else if (typeof(Boolean) == rootFwType) {
                this.putBoolean(root, itemId, obj as Boolean);
            } else if (typeof(Integer) == rootFwType) {
                this.putInteger(root, itemId, obj as Integer);
            } else if (typeof(PositiveInteger) == rootFwType) {
                this.putPositiveInteger(root, itemId, obj as PositiveInteger);
            } else if (typeof(String) == rootFwType) {
                this.putString(root, itemId, obj as String);
            } else if (typeof(BitString) == rootFwType) {
                this.putBitString(root, itemId, obj as BitString);
            } else if (typeof(Real) == rootFwType) {
                this.putReal(root, itemId, obj as Real);
            } else {
                XElement el = createOnly ? this.createElement(root, itemId) : this.createOrFetchElement(root, itemId);
                string cName = new String(obj.GetType().FullName).replaceAll(".", "::").to_string();
                XAttribute cAtt = this.createOrFetchAttribute(el, new PersistenceItemIdentity("_type"));
                cAtt.Value = cName;

                foreach (System.Reflection.PropertyInfo prop in type.GetProperties()) {
                    if (prop.PropertyType.IsEnum) {
                        System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("putEnum2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType);
                        genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name), obj });
                    } else if (typeof(BitString).IsAssignableFrom(prop.PropertyType)) {
                        //ToDo:
                    } else if (prop.PropertyType.IsGenericType && typeof(System.Nullable<>) == prop.PropertyType.GetGenericTypeDefinition()) {
                        System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("putEnum2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType.GetGenericArguments()[0]);
                        object propObj = prop.GetValue(obj, null);
                        object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name), propObj });
                    } else if (prop.PropertyType.IsGenericType && typeof(System.Collections.Generic.IList<>) == prop.PropertyType.GetGenericTypeDefinition()) {
                        System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("putList2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType.GetGenericArguments()[0]);
                        object propObj = prop.GetValue(obj, null);
                        object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name), propObj });
                    } else if (prop.PropertyType.IsGenericType && typeof(Sequence<>) == prop.PropertyType.GetGenericTypeDefinition()) {
                        System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("putSequence2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType.GetGenericArguments()[0]);
                        object propObj = prop.GetValue(obj, null);
                        object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name), propObj });
                    } else if (prop.PropertyType.IsGenericType && typeof(System.Collections.Generic.IDictionary<,>) == prop.PropertyType.GetGenericTypeDefinition()) {
                        System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("putMap2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType.GetGenericArguments());
                        object propObj = prop.GetValue(obj, null);
                        object v = genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(prop.Name), propObj });
                    } else {
                        System.Reflection.MethodInfo fetchMeth = this.GetType().GetMethod("putObject2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        System.Reflection.MethodInfo genericMeth = fetchMeth.MakeGenericMethod(prop.PropertyType);
                        string relPath = prop.Name;
                        object propObj = prop.GetValue(obj, null);
                        genericMeth.Invoke(this, new object[] { el, new PersistenceItemIdentity(relPath), propObj, new Boolean(false) });
                    }
                }
            }
        }

        internal System.Collections.Generic.IList<T> fetchList<T>(XNode root, PersistenceItemIdentity itemId) where T : class {
            string xpath = this.getXPathElement(itemId).to_string();
            XElement els = root.XPathSelectElement(xpath);
            if (null == els) {
                return new System.Collections.Generic.List<T>();
            } else {
                IEnumerable<T> elsT = els.Elements().Select(el =>
                    this.fetchObject<T>(el, new PersistenceItemIdentity("self"))
                );

                return elsT.ToList();
            }

        }
        void putList2<T>(XNode root, PersistenceItemIdentity itemId, System.Collections.Generic.IList<T> list) where T : class {
            XElement els = this.createOrFetchElement(root, itemId);

            els.RemoveAll();

            foreach (T obj in list) {
                this.putObject2<T>(els, new PersistenceItemIdentity("element"), obj, true);
            }
        }

        internal Sequence<T> fetchSequence<T>(XNode root, PersistenceItemIdentity itemId) where T : class {
            string xpath = this.getXPathElement(itemId).to_string();
            XElement els = root.XPathSelectElement(xpath);
            if (null == els) {
                return os.Sequence<T>();
            } else {
                IEnumerable<T> elsT = els.Elements().Select(el =>
                    this.fetchObject<T>(el, new PersistenceItemIdentity("self"))
                );

                return os.Sequence<T>(elsT);
            }

        }
        void putSequence2<T>(XNode root, PersistenceItemIdentity itemId, Sequence<T> list) where T : class {
            XElement els = this.createOrFetchElement(root, itemId);

            els.RemoveAll();

            foreach (T obj in list) {
                this.putObject2<T>(els, new PersistenceItemIdentity("element"), obj, true);
            }
        }


        internal System.Collections.Generic.IDictionary<K, V> fetchMap<K, V>(XNode root, PersistenceItemIdentity itemId)
            where K : class
            where V : class {
            string xpath = this.getXPathElement(itemId).to_string();
            XElement els = root.XPathSelectElement(xpath);
            if (null == els) {
                return new System.Collections.Generic.Dictionary<K, V>();
            } else {
                Dictionary<K, V> d = new Dictionary<K, V>();
                foreach (XElement el in els.Elements()) {
                    d.Add(
                      this.fetchObject<K>(el, new PersistenceItemIdentity("key")),
                      this.fetchObject<V>(el, new PersistenceItemIdentity("value"))
                    );
                }

                return d;
            }

        }
        void putMap2<K, V>(XNode root, PersistenceItemIdentity itemId, System.Collections.Generic.IDictionary<K, V> value)
            where K : class
            where V : class {
            XElement els = this.createOrFetchElement(root, itemId);

            els.RemoveAll();

            foreach (KeyValuePair<K, V> obj in value) {
                XElement pair = new XElement("Pair");
                els.Add(pair);
                this.putObject2<K>(pair, new PersistenceItemIdentity("key"), obj.Key, true);
                this.putObject2<V>(pair, new PersistenceItemIdentity("value"), obj.Value, true);
            }
        }

        #region PersistenceRead
        public basicTypes.Boolean contains(PersistenceItemIdentity itemId) {
            return null == this.impl ? null : this.contains(this.impl, itemId);
        }

        public basicTypes.Boolean fetchBoolean(PersistenceItemIdentity itemId) {
            return null == this.impl ? null : this.fetchBoolean(this.impl, itemId);
        }
        public basicTypes.Boolean fetchBoolean(PersistenceItemIdentity itemId, basicTypes.Boolean default_) {
            return null == this.impl ? default_ : this.fetchBoolean(this.impl, itemId, default_);
        }

        public basicTypes.Integer fetchInteger(PersistenceItemIdentity itemId) {
            return null == this.impl ? null : this.fetchInteger(this.impl, itemId);
        }
        public basicTypes.Integer fetchInteger(PersistenceItemIdentity itemId, basicTypes.Integer default_) {
            return null == this.impl ? default_ : this.fetchInteger(this.impl, itemId, default_);
        }

        public basicTypes.PositiveInteger fetchPositiveInteger(PersistenceItemIdentity itemId) {
            return null == this.impl ? null : this.fetchPositiveInteger(this.impl, itemId);
        }
        public basicTypes.PositiveInteger fetchPositiveInteger(PersistenceItemIdentity itemId, basicTypes.PositiveInteger default_) {
            return null == this.impl ? default_ : this.fetchPositiveInteger(this.impl, itemId, default_);
        }

        public basicTypes.Real fetchReal(PersistenceItemIdentity itemId) {
            return null == this.impl ? null : this.fetchReal(this.impl, itemId);
        }
        public basicTypes.Real fetchReal(PersistenceItemIdentity itemId, basicTypes.Real default_) {
            return null == this.impl ? default_ : this.fetchReal(this.impl, itemId, default_);
        }

        public basicTypes.String fetchString(PersistenceItemIdentity itemId) {
            return null == this.impl ? null : this.fetchString(this.impl, itemId);
        }
        public basicTypes.String fetchString(PersistenceItemIdentity itemId, basicTypes.String default_) {
            return null == this.impl ? default_ : this.fetchString(this.impl, itemId, default_);
        }

        public basicTypes.BitString fetchBitString(PersistenceItemIdentity itemId) {
            return null == this.impl ? null : this.fetchBitString8(this.impl, itemId);
        }
        public basicTypes.BitString fetchBitString(PersistenceItemIdentity itemId, basicTypes.BitString default_) {
            //fix this later..should not do the cast
            return null == this.impl ? default_ : this.fetchBitString8(this.impl, itemId, (BitString8)default_);
        }

        public basicTypes.DateTime fetchDateTime(PersistenceItemIdentity itemId) {
            return null == this.impl ? null : this.fetchDateTime(this.impl, itemId);
        }
        public basicTypes.DateTime fetchDateTime(PersistenceItemIdentity itemId, basicTypes.DateTime default_) {
            return null == this.impl ? default_ : this.fetchDateTime(this.impl, itemId, default_);
        }

        public T fetchObject<T>(PersistenceItemIdentity itemId) where T : class {
            return null == this.impl ? null : this.fetchObject<T>(this.impl, itemId);
        }
        public T fetchObject<T>(PersistenceItemIdentity itemId, T default_) where T : class {
            return null == this.impl ? default_ : this.fetchObject<T>(this.impl, itemId);
        }

        public T? fetchEnum<T>(PersistenceItemIdentity itemId) where T : struct {
            return null == this.impl ? null : this.fetchEnum<T>(this.impl, itemId);
        }
        public T? fetchEnum<T>(PersistenceItemIdentity itemId, T default_) where T : struct {
            return null == this.impl ? default_ : this.fetchEnum<T>(this.impl, itemId, default_);
        }

        public System.Collections.Generic.IList<T> fetchList<T>(PersistenceItemIdentity itemId) where T : class {
            return null == this.impl ? null : this.fetchList<T>(this.impl, itemId);
        }

        public Sequence<T> fetchSequence<T>(PersistenceItemIdentity itemId) where T : class {
            return null == this.impl ? null : this.fetchSequence<T>(this.impl, itemId);
        }

        public System.Collections.Generic.IDictionary<K, V> fetchMap<K, V>(PersistenceItemIdentity itemId)
            where K : class
            where V : class {
            return null == this.impl ? null : this.fetchMap<K, V>(this.impl, itemId);
        }


        #endregion

        #region PersistenceWrite
        public void putBoolean(PersistenceItemIdentity itemId, Boolean value) {
            if (this.impl != null) {
                this.putBoolean(this.impl, itemId, value);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }

        public void putInteger(PersistenceItemIdentity itemId, Integer value) {
            if (this.impl != null) {
                this.putInteger(this.impl, itemId, value);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }

        public void putPositiveInteger(PersistenceItemIdentity itemId, PositiveInteger value) {
            if (this.impl != null) {
                this.putPositiveInteger(this.impl, itemId, value);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }

        public void putReal(PersistenceItemIdentity itemId, Real value) {
            if (this.impl != null) {
                this.putReal(this.impl, itemId, value);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }

        public void putString(PersistenceItemIdentity itemId, String value) {
            if (this.impl != null) {
                this.putString(this.impl, itemId, value);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }

        public void putDateTime(PersistenceItemIdentity itemId, DateTime value) {
            throw new System.NotImplementedException();
        }

        public void putBitString(PersistenceItemIdentity itemId, BitString value) {
            throw new System.NotImplementedException();
        }

        public void putObject<T>(PersistenceItemIdentity itemId, T value) where T : class {
            if (this.impl != null) {
                this.putObject2(this.impl, itemId, value, false);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }

        public void putEnum<T>(PersistenceItemIdentity itemId, T? value) where T : struct {
            throw new System.NotImplementedException();
        }

        public void putList<T>(PersistenceItemIdentity itemId, IList<T> list) where T : class {
            if (this.impl != null) {
                this.putList2(this.impl, itemId, list);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }

        public void putSequence<T>(PersistenceItemIdentity itemId, Sequence<T> list) where T : class {
            if (this.impl != null) {
                this.putSequence2(this.impl, itemId, list);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }

        public void putMap<K, V>(PersistenceItemIdentity itemId, IDictionary<K, V> map)
            where K : class
            where V : class {
            if (this.impl != null) {
                this.putMap2(this.impl, itemId, map);
                this.save();
            } else {
                throw new PersistenceException("Document not found");
            }
        }
        #endregion
    }
}
