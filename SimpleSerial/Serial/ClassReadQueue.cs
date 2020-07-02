using System.Collections.Concurrent;

namespace SimpleSerial.Serial
{
    /// <summary>
    /// Implement of read queue.
    /// </summary>
    class ClassReadQueue
    {
        /// <summary>
        /// Filed of ReadQueue.
        /// </summary>
        private BlockingCollection<byte> ReadQueue { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size"></param>
        public ClassReadQueue(int size = 256)
        {
            ReadQueue = new BlockingCollection<byte>(size);
        }

        /// <summary>
        /// ReadQueue Take method.
        /// </summary>
        /// <param name="Data">[out]</param>
        /// <returns></returns>
        public bool Take(out byte Data)
        {
            return ReadQueue.TryTake(out Data, 100); // 100 miniseconds
        }

        /// <summary>
        /// ReadQueue Add method.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool Add(byte Data)
        {
            return ReadQueue.TryAdd(Data, 100); // 100 miniseconds
        }
    }
}
