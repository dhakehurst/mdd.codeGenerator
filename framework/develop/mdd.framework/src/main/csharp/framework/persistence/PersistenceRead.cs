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

    public interface PersistenceRead
	
	{

        global::framework.basicTypes.Boolean contains(PersistenceItemIdentity itemId);

        global::framework.basicTypes.Boolean fetchBoolean(PersistenceItemIdentity itemId);
        global::framework.basicTypes.Boolean fetchBoolean(PersistenceItemIdentity itemId, global::framework.basicTypes.Boolean default_);

        global::framework.basicTypes.Integer fetchInteger(PersistenceItemIdentity itemId);
        global::framework.basicTypes.Integer fetchInteger(PersistenceItemIdentity itemId, global::framework.basicTypes.Integer default_);

        global::framework.basicTypes.PositiveInteger fetchPositiveInteger(PersistenceItemIdentity itemId);
        global::framework.basicTypes.PositiveInteger fetchPositiveInteger(PersistenceItemIdentity itemId, global::framework.basicTypes.PositiveInteger default_);

        global::framework.basicTypes.Real fetchReal(PersistenceItemIdentity itemId);
        global::framework.basicTypes.Real fetchReal(PersistenceItemIdentity itemId, global::framework.basicTypes.Real default_);

        global::framework.basicTypes.String fetchString(PersistenceItemIdentity itemId);
        global::framework.basicTypes.String fetchString(PersistenceItemIdentity itemId, global::framework.basicTypes.String default_);

        global::framework.basicTypes.DateTime fetchDateTime(PersistenceItemIdentity itemId);
        global::framework.basicTypes.DateTime fetchDateTime(PersistenceItemIdentity itemId, global::framework.basicTypes.DateTime default_);

        global::framework.basicTypes.BitString fetchBitString(PersistenceItemIdentity itemId);
        global::framework.basicTypes.BitString fetchBitString(PersistenceItemIdentity itemId, global::framework.basicTypes.BitString default_);

        T fetchObject<T>(PersistenceItemIdentity itemId) where T : class;
        T fetchObject<T>(PersistenceItemIdentity itemId, T default_) where T : class;

        T? fetchEnum<T>(PersistenceItemIdentity itemId) where T : struct;
        T? fetchEnum<T>(PersistenceItemIdentity itemId, T default_) where T : struct;

        System.Collections.Generic.IList<T> fetchList<T>(PersistenceItemIdentity itemId) where T : class;
        global::framework.collections.Sequence<T> fetchSequence<T>(PersistenceItemIdentity itemId) where T : class;

        System.Collections.Generic.IDictionary<K,V> fetchMap<K,V>(PersistenceItemIdentity itemId) where K : class where V : class;
    }

}
