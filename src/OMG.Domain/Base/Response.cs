using System.Text.Json.Serialization;

namespace OMG.Domain.Base;

public class Response<TData>
{
    public Response(TData? data, int code = 200)
    {
        Data = data;
        Code = code;
    }

    public Response(int code, string message)
    {
        Message = message;
        Code = code;
    }

    public TData? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }

    [JsonIgnore]
    public bool IsSuccess
        => Code is >= 200 and <= 299;
}
