/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.logging
{

    public interface Logger
    {
        void trace(Location location, Message message);
        void debug(Location location, Message message);
        void info(Location location, Message message);
        void warn(Location location, Message message);
        void error(Location location, Message message);
        void fatal(Location location, Message message);

        void trace(Location location, global::System.Func<Message> expr);
        void debug(Location location, global::System.Func<Message> expr);
        void info(Location location, global::System.Func<Message> expr);
        void warn(Location location, global::System.Func<Message> expr);
        void error(Location location, global::System.Func<Message> expr);
        void fatal(Location location, global::System.Func<Message> expr);
    }

}
