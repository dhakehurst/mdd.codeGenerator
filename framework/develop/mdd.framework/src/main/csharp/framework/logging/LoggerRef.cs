/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.logging {

	// Class
	public class LoggerRef
	{
	
	  // --- Constructors ---
	    public LoggerRef(Location location) {
            this.location = location;
	    }
        static public Logger actualLogger;
        Location location;

        public void trace(Message message) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.trace(this.location, message);
            }
        }

        public void debug(Message message) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.debug(this.location, message);
            }
        }

        public void info(Message message) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.info(this.location, message);
            }
        }

        public void warn(Message message) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.warn(this.location, message);
            }
        }

        public void error(Message message) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.error(this.location, message);
            }
        }

        public void fatal(Message message) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.fatal(this.location, message);
            }
        }

        public void trace(global::System.Func<Message> expr) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.trace(this.location, expr);
            }
        }

        public void debug(global::System.Func<Message> expr) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.debug(this.location, expr);
            }
        }

        public void info(global::System.Func<Message> expr) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.info(this.location, expr);
            }
        }

        public void warn(global::System.Func<Message> expr) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.warn(this.location, expr);
            }
        }

        public void error(global::System.Func<Message> expr) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.error(this.location, expr);
            }
        }

        public void fatal(global::System.Func<Message> expr) {
            if (null != LoggerRef.actualLogger) {
                LoggerRef.actualLogger.fatal(this.location, expr);
            }
        }
	}

} //namespace framework.logging
