using System.Collections.Concurrent;

namespace SimpleSerial.Serial
{
    /// <summary>
    /// Implement of read queue.
    /// </summary>
    internal class ClassReadQueue
    {
        /// <summary>
        /// Filed of ReadQueue.
        /// </summary>
        private BlockingCollection<byte> ReadQueue { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">[in]</param>
        public ClassReadQueue(int size = 256)
        {
            ReadQueue = new BlockingCollection<byte>(size);
        }

        /// <summary>
        /// ReadQueue Take method.
        /// </summary>
        /// <param name="data">[out]</param>
        /// <returns>Bool - Return true for success.</returns>
        public bool Take(out byte data)
        {
            return ReadQueue.TryTake(out data, 100); // 100 mini seconds
        }

        /// <summary>
        /// ReadQueue Add method.
        /// </summary>
        /// <param name="data">[in]</param>
        /// <returns>Bool - Return true for success.</returns>
        public bool Add(byte data)
        {
            return ReadQueue.TryAdd(data, 100); // 100 mini seconds
        }
    }
}
