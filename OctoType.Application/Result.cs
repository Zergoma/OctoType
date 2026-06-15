namespace OctoType.Application;

public class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }

    public T GetValue
    {
        get 
        {
            if (Success) return Value!;
            throw new ArgumentNullException();
        }
    }
    
    public string Error { get; }

    private Result(
        bool success,
        T? value,
        string error)
    {
        Success = success;
        Value = value;
        Error = error;
    }

    public static Result<T> Ok(T value)
    {
        return new(true, value, string.Empty);
    }

    public static Result<T> Fail(string error)
    {
        return new(false, default, error);
    }
}
