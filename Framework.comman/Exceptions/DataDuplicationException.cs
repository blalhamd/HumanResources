

using Framework.comman.Exceptions.Base;

namespace Framework.comman.Exceptions
{
    public class DataDuplicationException : BaseException
    {

        public DataDuplicationException() : base("DataDuplicationException") { }

        public DataDuplicationException(string message) : base(message) { }

        public DataDuplicationException(int errorCode) : base(errorCode) { }

        public DataDuplicationException(int errorCode, string message) : base(errorCode, message) { }

        
    }
}
