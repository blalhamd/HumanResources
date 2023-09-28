

using Framework.comman.Exceptions.Base;

namespace Framework.comman.Exceptions
{
    public class ItemAlreadyExistException : BaseException
    {

        public ItemAlreadyExistException() : base("ItemAlreadyExistException") { }

        public ItemAlreadyExistException(string message) : base(message) { }

        public ItemAlreadyExistException(int errorCode) : base(errorCode) { }

        public ItemAlreadyExistException(int errorCode,string message) : base(errorCode, message) { }


    }
}
