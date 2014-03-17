/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.persistence {

	using global::framework.basicTypes;

	// Class
	public class PersistenceRef
    : global::framework.persistence.PersistenceStore
    {
        global::framework.logging.LoggerRef log = new global::framework.logging.LoggerRef(new global::framework.logging.Location("framework.persistence.PersistenceRef"));

        #region Constructors ---
        public PersistenceRef(PersistenceStoreIdentity referingTo) {
            this._identity = referingTo;
	    }
        #endregion

        #region Properties

        private PersistenceStoreIdentity _identity;
        public PersistenceStoreIdentity identity {
            get { return _identity; }
        }

        private static System.Collections.Generic.Dictionary<PersistenceStoreIdentity, PersistenceStore> _actualPersistenceStore = new System.Collections.Generic.Dictionary<PersistenceStoreIdentity,PersistenceStore>();
        public static System.Collections.Generic.Dictionary<PersistenceStoreIdentity, PersistenceStore> actualPersistenceStore {
            get { return PersistenceRef._actualPersistenceStore; }
            set { PersistenceRef._actualPersistenceStore = value; }
        }

        #endregion

        public PersistenceStore store(PersistenceStoreIdentity id) {
            if (actualPersistenceStore.ContainsKey(id)) {
                return actualPersistenceStore[id];
            } else {
                log.error(new logging.Message("Persistence Store with identity '" + id + "' not found"));
                //throw new PersistenceException("Persistence Store with identity '" + id + "' not found");
                return null;
            }
        }


        #region PersistenceRead Members

        public Boolean contains(PersistenceItemIdentity itemId) {
            return null==this.store(this.identity) ? null : this.store(this.identity).contains(itemId);
        }

        public Boolean fetchBoolean(PersistenceItemIdentity itemId) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchBoolean(itemId);
        }
        public Boolean fetchBoolean(PersistenceItemIdentity itemId, Boolean default_) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchBoolean(itemId, default_);
        }

        public Integer fetchInteger(PersistenceItemIdentity itemId) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchInteger(itemId);
        }
        public Integer fetchInteger(PersistenceItemIdentity itemId, Integer default_) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchInteger(itemId, default_);
        }

        public PositiveInteger fetchPositiveInteger(PersistenceItemIdentity itemId) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchPositiveInteger(itemId);
        }
        public PositiveInteger fetchPositiveInteger(PersistenceItemIdentity itemId, PositiveInteger default_) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchPositiveInteger(itemId, default_);
        }

        public Real fetchReal(PersistenceItemIdentity itemId) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchReal(itemId);
        }
        public Real fetchReal(PersistenceItemIdentity itemId, Real default_) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchReal(itemId, default_);
        }

        public String fetchString(PersistenceItemIdentity itemId) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchString(itemId);
        }
        public String fetchString(PersistenceItemIdentity itemId, String default_) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchString(itemId, default_);
        }

        public BitString fetchBitString(PersistenceItemIdentity itemId) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchBitString(itemId);
        }
        public BitString fetchBitString(PersistenceItemIdentity itemId, BitString default_) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchBitString(itemId, default_);
        }
        
        public DateTime fetchDateTime(PersistenceItemIdentity itemId) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchDateTime(itemId);
        }
        public DateTime fetchDateTime(PersistenceItemIdentity itemId, DateTime default_) {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchDateTime(itemId, default_);
        }

        public T? fetchEnum<T>(PersistenceItemIdentity itemId) where T : struct {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchEnum<T>(itemId);
        }
        public T? fetchEnum<T>(PersistenceItemIdentity itemId, T default_) where T : struct {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchEnum<T>(itemId, default_);
        }

        public T fetchObject<T>(PersistenceItemIdentity itemId) where T : class {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchObject<T>(itemId);
        }
        public T fetchObject<T>(PersistenceItemIdentity itemId, T default_) where T : class {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchObject<T>(itemId, default_);
        }

        public System.Collections.Generic.IList<T> fetchList<T>(PersistenceItemIdentity itemId) where T : class {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchList<T>(itemId);
        }
        public global::framework.collections.Sequence<T> fetchSequence<T>(PersistenceItemIdentity itemId) where T : class {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchSequence<T>(itemId);
        }
        public System.Collections.Generic.IDictionary<K,V> fetchMap<K,V>(PersistenceItemIdentity itemId) where K : class where V : class {
            return null == this.store(this.identity) ? null : this.store(this.identity).fetchMap<K, V>(itemId);
        }
        #endregion

        #region PersistenceWrite Members
        public void putBoolean(PersistenceItemIdentity itemId, global::framework.basicTypes.Boolean value) {
            if (null == this.store(this.identity) ) {
            } else {
                this.store(this.identity).putBoolean(itemId, value);
            }
        }

        public void putInteger(PersistenceItemIdentity itemId, global::framework.basicTypes.Integer value) {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putInteger(itemId, value);
            }
        }

        public void putPositiveInteger(PersistenceItemIdentity itemId, global::framework.basicTypes.PositiveInteger value) {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putPositiveInteger(itemId, value);
            }
        }

        public void putReal(PersistenceItemIdentity itemId, global::framework.basicTypes.Real value) {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putReal(itemId, value);
            }
        }

        public void putString(PersistenceItemIdentity itemId, global::framework.basicTypes.String value) {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putString(itemId, value);
            }
        }

        public void putDateTime(PersistenceItemIdentity itemId, global::framework.basicTypes.DateTime value) {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putDateTime(itemId, value);
            }
        }

        public void putBitString(PersistenceItemIdentity itemId, global::framework.basicTypes.BitString value) {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putBitString(itemId, value);
            }
        }


        public void putObject<T>(PersistenceItemIdentity itemId, T value) where T : class {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putObject(itemId, value);
            }
        }

        public void putEnum<T>(PersistenceItemIdentity itemId, T? value) where T : struct {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putEnum(itemId, value);
            }
        }


        public void putList<T>(PersistenceItemIdentity itemId, System.Collections.Generic.IList<T> list) where T : class {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putList(itemId, list);
            }
        }

        public void putMap<K, V>(PersistenceItemIdentity itemId, System.Collections.Generic.IDictionary<K, V> map)
            where K : class
            where V : class
        {
            if (null == this.store(this.identity)) {
            } else {
                this.store(this.identity).putMap(itemId, map);
            }
        }

        #endregion
    }

} //namespace framework.configuration
