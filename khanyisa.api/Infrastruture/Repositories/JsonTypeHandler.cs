using System.Data;
using System.Text.Json;
using Dapper;

namespace Infrastruture.Repositories;

public class JsonTypeHandler : SqlMapper.ITypeHandler
{
    public void SetValue(IDbDataParameter parameter, object value) => parameter.Value = JsonSerializer.Serialize(value);

    public object? Parse(Type destinationType, object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }

        if (value is string jsonString)
        {
            return JsonSerializer.Deserialize(jsonString, destinationType);
        }

        throw new ArgumentException("The value is not a string and cannot be deserialized.", nameof(value));
    }
}
