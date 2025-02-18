using Core.Interfaces;

namespace Infrastructure.Config;

public class AppConfig : IAppConfig
{
    public string SqlConnection { get; set; }
}
