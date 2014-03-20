/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/

﻿// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "framework.logging.log4Net.config", Watch = true)]

namespace framework.logging.log4Net {
    using framework.basicTypes;

    public class LoggerImpl : global::framework.logging.Logger, System.IDisposable {

        public LoggerImpl(global::framework.basicTypes.String loggerConfigFileName) {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(loggerConfigFileName.to_string());
            log4net.Config.XmlConfigurator.Configure(fileInfo);

            if (null == LoggerRef.actualLogger) {
                LoggerRef.actualLogger = this;
            } else {
                throw new LoggingException("There is already a Logger object instantiated, you can use only one (did you 'Dispose' the old one).");
            }
        }

        public void Dispose() {
            LoggerRef.actualLogger = null;
        }

        private System.Collections.Generic.Dictionary<Location, global::log4net.ILog> logger = new System.Collections.Generic.Dictionary<Location,log4net.ILog>();
        private global::log4net.ILog getLogger(Location location) {
            if ( ! this.logger.ContainsKey(location) ) {
                this.logger[location] = log4net.LogManager.GetLogger(location.to_string());
            }
            return this.logger[location];
        }

        #region Logger Members
        public Boolean isDebugEnabled(Location location) {
            return this.getLogger(location).IsDebugEnabled;
        }

        public void trace(Location location, Message message) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.Logger.IsEnabledFor(global::log4net.Core.Level.Trace)) {
                log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                    global::log4net.Core.Level.Trace, message.to_string(), null);
            }
        }

        public void debug(Location location, Message message) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsDebugEnabled) {
                log.Debug(message.to_string());
            }
        }

        public void info(Location location, Message message) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsInfoEnabled) {
                log.Info(message.to_string());
            }
        }

        public void warn(Location location, Message message) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsWarnEnabled) {
                log.Warn(message.to_string());
            }
        }

        public void error(Location location, Message message) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsErrorEnabled) {
                log.Error(message.to_string());
            }
        }

        public void fatal(Location location, Message message) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsFatalEnabled) {
                log.Fatal(message.to_string());
            }
        }

        public void trace(Location location, global::System.Func<Message> expr) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.Logger.IsEnabledFor(global::log4net.Core.Level.Trace)) {
                log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                    global::log4net.Core.Level.Trace, expr.Invoke().to_string(), null);
            }
        }

        public void debug(Location location, global::System.Func<Message> expr) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsDebugEnabled) {
                log.Debug(expr.Invoke().to_string());
            }
        }

        public void info(Location location, global::System.Func<Message> expr) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsInfoEnabled) {
                log.Info(expr.Invoke().to_string());
            }
        }

        public void warn(Location location, global::System.Func<Message> expr) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsWarnEnabled) {
                log.Warn(expr.Invoke().to_string());
            }
        }

        public void error(Location location, global::System.Func<Message> expr) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsErrorEnabled) {
                log.Error(expr.Invoke().to_string());
            }
        }

        public void fatal(Location location, global::System.Func<Message> expr) {
            global::log4net.ILog log = this.getLogger(location);
            if (log.IsFatalEnabled) {
                log.Fatal(expr.Invoke().to_string());
            }
        }

        #endregion
    }

}
