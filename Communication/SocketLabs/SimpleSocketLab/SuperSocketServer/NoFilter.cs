using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocketServer
{
    public abstract class NoFilter<TRequestInfo> : IReceiveFilter<TRequestInfo>, IOffsetAdapter,
            IReceiveFilterInitializer
            where TRequestInfo : IRequestInfo
    {
        private int m_ParsedLength;

        private int m_Size;

        /// <summary>
        /// Gets the size of the fixed size Receive filter.
        /// </summary>
        public int Size
        {
            get { return m_Size; }
        }

        /// <summary>
        /// Null RequestInfo
        /// </summary>
        protected readonly static TRequestInfo NullRequestInfo = default(TRequestInfo);

        /// <summary>
        /// Initializes a new instance of the <see cref="NoFilter&lt;TRequestInfo&gt;"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        protected NoFilter()
        {

        }


        void IReceiveFilterInitializer.Initialize(IAppServer appServer, IAppSession session)
        {
        }

        private List<byte[]> currentReadBuffer = null;
        /// <summary>
        /// Filters the specified session.
        /// </summary>
        /// <param name="readBuffer">The read buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <param name="toBeCopied">if set to <c>true</c> [to be copied].</param>
        /// <param name="rest">The rest.</param>
        /// <returns></returns>
        public virtual TRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            rest = 0;

            var requestInfo = ProcessMatchedRequest(readBuffer, offset, length, toBeCopied);
            InternalReset();
            return requestInfo;
        }

        /// <summary>
        /// Filters the buffer after the server receive the enough size of data.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <param name="toBeCopied">if set to <c>true</c> [to be copied].</param>
        /// <returns></returns>
        protected abstract TRequestInfo ProcessMatchedRequest(byte[] buffer, int offset, int length, bool toBeCopied);

        /// <summary>
        /// Gets the size of the rest buffer.
        /// </summary>
        /// <value>
        /// The size of the rest buffer.
        /// </value>
        public int LeftBufferSize
        {
            get { return m_ParsedLength; }
        }

        /// <summary>
        /// Gets the next Receive filter.
        /// </summary>
        public virtual IReceiveFilter<TRequestInfo> NextReceiveFilter
        {
            get { return null; }
        }


        private int m_OffsetDelta;

        /// <summary>
        /// Gets the offset delta.
        /// </summary>
        int IOffsetAdapter.OffsetDelta
        {
            get { return m_OffsetDelta; }
        }

        /// <summary>
        /// Gets the filter state.
        /// </summary>
        /// <value>
        /// The filter state.
        /// </value>
        public FilterState State { get; protected set; }

        private void InternalReset()
        {
            m_ParsedLength = 0;
            m_OffsetDelta = 0;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public virtual void Reset()
        {
            InternalReset();
        }
    }
}
