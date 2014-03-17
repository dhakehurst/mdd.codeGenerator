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
    /// <summary> Primitive Type Description </summary>
    public class ChannelIdentity
    : global::framework.basicTypes.String
    {

        /// <summary> Constructor Description </summary>
        public ChannelIdentity(System.String value) : this(new global::framework.basicTypes.String(value)) { }

        /// <summary> Constructor Description </summary>
        public ChannelIdentity(global::framework.basicTypes.String value) : base(value) { }

        #region framework Specific
        /// <summary> Clone </summary>
        protected override object _deepClone() {
            ChannelIdentity clone = new ChannelIdentity(this);
            return clone;
        }
        public new ChannelIdentity deepClone() {
            return this._deepClone() as ChannelIdentity;
        }
        #endregion

    }
}
