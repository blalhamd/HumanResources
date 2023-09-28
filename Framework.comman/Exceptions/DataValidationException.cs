

using Framework.comman.Exceptions.Base;

namespace Framework.comman.Exceptions
{
    public class DataValidationException : BaseException
    {
        public DataValidationException() :base("DataValidationException") { }

        public DataValidationException(string message) : base(message) { }

        public DataValidationException(int errorCode) : base(errorCode) { }

        public DataValidationException(int errorCode,string message) : base(errorCode,message) { }
    }
}
