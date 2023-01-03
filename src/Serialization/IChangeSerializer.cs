public interface IChangeSerializer
{

    public string Serialize(object value);

    public object? Deserialize(string value);

}