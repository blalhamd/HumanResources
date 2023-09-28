using System;
using System.Runtime.Serialization;

namespace Framework.comman.Exceptions.Base
{
    public class BaseException : ApplicationException
    {
        public int errorCode { get; set; }

        public BaseException() { }

        public BaseException(string message) : base(message) { }

        public BaseException(int errorCode) : base(errorCode.ToString())
        {
            this.errorCode = errorCode;
        }

        public BaseException(int errorCode, string message) : base(message)
        {
            this.errorCode = errorCode;
        }

        public BaseException(string message,Exception innerException) 
            : base(message,innerException) { }

        public BaseException(SerializationInfo serialization,StreamingContext streamingContext)
            : base(serialization,streamingContext) 
        {

        }


    }
}

/*

    public int ErrorCode { get; set; }

    This line declares a public property ErrorCode of type int.
    It allows you to store an error code associated with the exception.
    The get and set keywords define the property's accessors, allowing you
    to get and set the value of ErrorCode from outside the class.
   
    --------------------------------------------------------------------------------------------------------------------------------
    --------------------------------------------------------------------------------------------------------------------------------

    public BaseException()
    { }

    This line declares a default constructor for the BaseException class with no parameters.
    It allows you to create an instance of the BaseException class without providing any arguments.

    --------------------------------------------------------------------------------------------------------------------------------
    --------------------------------------------------------------------------------------------------------------------------------
    
    public BaseException(string message) : base(message)
    { }

    This line declares a constructor for the BaseException class that takes a message parameter of type string.
    It calls the base class constructor (ApplicationException) passing the message parameter to it. 
    This constructor allows you to create an instance of BaseException with a custom error message.

    --------------------------------------------------------------------------------------------------------------------------------
    --------------------------------------------------------------------------------------------------------------------------------

    
    public BaseException(string message, Exception innerException) : base(message, innerException)
    { }

    This line declares a constructor for the BaseException class that takes two parameters:
    message of type string and innerException of type Exception. It calls the base class constructor (ApplicationException)
    passing both the message and innerException parameters to it. This constructor allows you to create an instance of BaseException
    with a custom error message and an inner exception.

    --------------------------------------------------------------------------------------------------------------------------------
    --------------------------------------------------------------------------------------------------------------------------------

    protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }

    This line declares a constructor for the BaseException class that is used for deserialization purposes.
    It takes a SerializationInfo object and a StreamingContext object as parameters. It calls the base class constructor passing
    the info and context parameters to it. This constructor allows the BaseException class to be properly deserialized when needed.

    --------------------------------------------------------------------------------------------------------------------------------
    --------------------------------------------------------------------------------------------------------------------------------
   
    public BaseException(int errorCode) : base(errorCode.ToString())
    {
        this.ErrorCode = errorCode;
    }

    This line declares a constructor for the BaseException class that takes an errorCode parameter of type int.
    It converts the errorCode to a string using errorCode.ToString() and passes it as the message to
    the base class constructor (ApplicationException). It also sets the ErrorCode property with the provided errorCode value. 
    This constructor allows you to create an instance of BaseException with a custom error code.
    
    --------------------------------------------------------------------------------------------------------------------------------
    --------------------------------------------------------------------------------------------------------------------------------

    public BaseException(int errorCode, string message) : base(message)
    {
        this.ErrorCode = errorCode;
    }

    This line declares a constructor for the BaseException class that takes two parameters:
    errorCode of type int and message of type string. It passes the message parameter to
    the base class constructor (ApplicationException). It also sets the ErrorCode property with the provided errorCode value.
    This constructor allows you to create an instance of BaseException with a custom error code and a custom error message.
    
    Each line in the code serves a specific purpose:
    
    Property declaration: ErrorCode provides a public property to store the error code associated with the exception.
    Constructors: They define various ways to create instances of the BaseException class with different sets of parameters. Each constructor allows you to customize the error code, error message, and handle serialization/deserialization scenarios.
 
 */
