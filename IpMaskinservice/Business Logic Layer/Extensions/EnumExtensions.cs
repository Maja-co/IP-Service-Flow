using Data_Access_Layer.Models;

namespace Business_Logic_Layer.Extensions;

public static class EnumExtensions
{
    public static string TilDansk(this ServiceInterval interval) => interval switch
    {
        ServiceInterval.TOMÅNEDER   => "2 måneder",
        ServiceInterval.SEKSMÅNEDER => "6 måneder",
        ServiceInterval.TOLVMÅNEDER => "12 måneder",
        _ => interval.ToString()
    };

    public static string TilDansk(this ServiceType type) => type switch
    {
        ServiceType.FULDSERVICE => "Fuld service",
        ServiceType.DELSERVICE  => "Del-service",
        _ => type.ToString()
    };
}