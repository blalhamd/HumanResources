

namespace Framework.comman.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}


/*
 public IActionResult Get(int id)
{
    var entity = repository.GetEntityById(id);
    if (entity == null)
    {
        throw new HttpResponseException
        {
            Status = 404,
            Value = "Entity not found"
        };
    }

    // Other logic
}
 
 */