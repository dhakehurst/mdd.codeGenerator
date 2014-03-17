/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
﻿namespace framework.comms
{
    using global::framework.basicTypes;
    using global::framework.collections;

    /// <summary> Interface Description </summary>
    public interface Subscriber
    {
        /// <summary> Operation Description </summary>
        void update<T>(ChannelIdentity channelId, Sequence<T> data) where T : struct, BitString;
    }
}
