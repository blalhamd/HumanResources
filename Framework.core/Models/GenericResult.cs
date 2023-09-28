using Framework.core.comman;

namespace Framework.core.Models
{
    public class GenericResult<TCollection>
    {
        public Pagination pagination { get; set; }
        public TCollection collection { get; set; }
    }
}

