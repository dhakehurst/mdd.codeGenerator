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

    public interface PersistenceWrite
	
	{
        void putBoolean(PersistenceItemIdentity itemId, global::framework.basicTypes.Boolean value);
        void putInteger(PersistenceItemIdentity itemId, global::framework.basicTypes.Integer value);
        void putPositiveInteger(PersistenceItemIdentity itemId, global::framework.basicTypes.PositiveInteger value);
        void putReal(PersistenceItemIdentity itemId, global::framework.basicTypes.Real value);
        void putString(PersistenceItemIdentity itemId, global::framework.basicTypes.String value);
        void putDateTime(PersistenceItemIdentity itemId, global::framework.basicTypes.DateTime value);
        void putBitString(PersistenceItemIdentity itemId, global::framework.basicTypes.BitString value);

        void putObject<T>(PersistenceItemIdentity itemId, T value) where T : class;
        void putEnum<T>(PersistenceItemIdentity itemId, T? value) where T : struct;

        void putList<T>(PersistenceItemIdentity itemId, System.Collections.Generic.IList<T> list) where T : class;
        void putMap<K, V>(PersistenceItemIdentity itemId, System.Collections.Generic.IDictionary<K, V> map) where K : class where V : class;
	}

}
