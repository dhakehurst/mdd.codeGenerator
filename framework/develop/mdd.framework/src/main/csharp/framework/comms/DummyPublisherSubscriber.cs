/*************************************************************************
* Copyright (c) 2013 - 2014 Dr David H. Akehurst.
* All rights reserved. This program and the accompanying materials
* are made available under the terms of the Eclipse Public License v1.0
* and is available at http://www.eclipse.org/legal/epl-v10.html
*
* Contributors:
* 
*************************************************************************/
namespace framework.comms
{

    using global::System.Linq;
    using global::framework.basicTypes;
    using global::framework.collections;

    /// <summary> Class Description </summary>
    public class DummyPublisherSubscriber
    : global::framework.comms.Publisher,
        global::framework.os.threading.Runnable
    {
        private global::framework.os.OsRef os;
        private global::framework.logging.LoggerRef log;
        private global::framework.persistence.PersistenceRef config;
        private global::framework.basicTypes.String objectId;

        /// <summary> Constructor Description </summary>
        public DummyPublisherSubscriber(global::framework.basicTypes.String objectId) {
            this.objectId = objectId;
            this.os = new global::framework.os.OsRef();
            this.log = new global::framework.logging.LoggerRef(new global::framework.logging.Location(objectId));
            this.config = new global::framework.persistence.PersistenceRef(new framework.persistence.PersistenceStoreIdentity("configuration"));
            #region Ports
            #endregion
            #region Composite Properties
            this.subscriber = os.Sequence<Subscriber>();
            #endregion
            #region Connectors
            #endregion
        }

        #region Operations
        /// <summary> Operation Description </summary>
        public void publish<T>(ChannelIdentity channelId, Sequence<T> data)
            where T : struct, BitString
        {
            this._fw_eventQueue.Add( new signal_publish<T>(channelId, data) );
        }
        #endregion

        #region Properties
        /// <summary> Property Description </summary>
        private global::framework.collections.Sequence<global::framework.comms.Subscriber> _subscriber;
        /// <summary> Property Description </summary>
        public global::framework.collections.Sequence<global::framework.comms.Subscriber> subscriber {
            get { return this._subscriber; }
            set { this._subscriber = value; }
        }
        /// <summary> Property Description </summary>
        public void addSubscriber(global::framework.comms.Subscriber value) { this.subscriber = this.subscriber.including(value); }
        /// <summary> Property Description </summary>
        public void removeSubscriber(global::framework.comms.Subscriber value) { this.subscriber = this.subscriber.excluding(value); }

        /// <summary> Property Description </summary>
        private global::framework.collections.Sequence<global::framework.comms.Publisher> _publisher;
        /// <summary> Property Description </summary>
        public global::framework.collections.Sequence<global::framework.comms.Publisher> publisher {
            get { return this._publisher; }
            set { this._publisher = value; }
        }
        /// <summary> Property Description </summary>
        public void addPublisher(global::framework.comms.Publisher value) { this.publisher = this.publisher.including(value); }
        /// <summary> Property Description </summary>
        public void removePublisher(global::framework.comms.Publisher value) { this.publisher = this.publisher.excluding(value); }

        #endregion

        #region Active Support
        private global::framework.os.threading.Thread _thread;
        public global::framework.os.threading.Thread thread {
            get { return _thread; }
        }

        public void start() {
            this._thread = os.createThread(this.objectId);
            this.thread.start(this);
        }

        private System.Threading.CancellationTokenSource _fw_cancellationTokenSource = new System.Threading.CancellationTokenSource();
        /// <summary> Operation Description </summary>
        public void runStart() {

        }
                /// <summary> Operation Description </summary>
        public void runStep() {
            object o = this._fw_eventQueue.Take(this._fw_cancellationTokenSource.Token);
            if(o is signal_publish<BitString8>) {
                signal_publish<BitString8> signal = o as signal_publish<BitString8>;
                this.subscriber.forEvery( s => s.update(signal.channelId, signal.data) );
            } else if (o is signal_publish<BitString16>) {
                signal_publish<BitString16> signal = o as signal_publish<BitString16>;
                this.subscriber.forEvery(s => s.update(signal.channelId, signal.data));
            } else if (o is signal_publish<BitString32>) {
                signal_publish<BitString32> signal = o as signal_publish<BitString32>;
                this.subscriber.forEvery(s => s.update(signal.channelId, signal.data));
            } else if (o is signal_publish<BitString64>) {
                signal_publish<BitString64> signal = o as signal_publish<BitString64>;
                this.subscriber.forEvery(s => s.update(signal.channelId, signal.data));
            }
        }
        /// <summary> Operation Description </summary>
        public void runFinish() {

        }
        /// <summary> Operation Description </summary>
        public void run() {
            try {
                this.runStart();
                while (false == _fw_cancellationTokenSource.IsCancellationRequested) {
                    this.runStep();
                }
                this.runFinish();
            } catch (System.Exception ex) {
                log.error(new global::framework.logging.Message(ex.Message));
            }
        }
        /// <summary> Operation Description </summary>
        public void stop() {
            this._fw_cancellationTokenSource.Cancel();
        }
        #endregion

        #region Receptions
        private System.Collections.Concurrent.BlockingCollection<object> _fw_eventQueue = new System.Collections.Concurrent.BlockingCollection<object>();
        public T _fw_receive<T>(framework.time.Duration timeout) where T : class {
            object obj;
            bool res = this._fw_eventQueue.TryTake(out obj, timeout.asMilliseconds.asInteger().to_Int32());
            if (true == res && obj is T) {
                return obj as T;
            } else {
                return null;
            }
        }

        //internal signal
        class signal_publish<T> where T : struct, BitString
        {
            public signal_publish(ChannelIdentity channelId, Sequence<T> data) {
                this.channelId = channelId;
                this.data = data;

            }
            public ChannelIdentity channelId;
            public Sequence<T> data;
        }
        #endregion
    }

} //namespace optMast.astute.boat4.mastToControl.test.channel
