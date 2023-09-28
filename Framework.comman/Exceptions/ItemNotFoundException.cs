
using Framework.comman.Exceptions.Base;

namespace Framework.comman.Exceptions
{
    public class ItemNotFoundException : BaseException
    {

        public ItemNotFoundException() : base("ItemNotFoundException") { }

        public ItemNotFoundException(string message) : base(message) { }

        public ItemNotFoundException(int errorCode) : base(errorCode) { }

        public ItemNotFoundException(int errorCode,string message) : base(errorCode,message) { }

    }
}
