namespace OMG.Domain;

public class Configuracao
{
    public const string HttpClientNameOMGApi = "omgapi";

    public static string ConnectionString { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;
}
