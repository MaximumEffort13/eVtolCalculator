using System.Security;

namespace Domain.ConstantValues;

public class PredefinedConstantValues
{
    /// <summary>
    /// kg lift generated per kW power (kg/kW)
    /// </summary>
    public const double motorThrustToPowerRatio = 4;

    /// <summary>
    /// kW power output per kg of motor weight. (kW/kg)
    /// </summary>
    public const double motorPowerLoading = 5;

    /// <summary>
    /// Electrical power to horsepower Conversion ratio (W)
    /// </summary>
    public const double horsepowerToWatConversion = 745.69987;

    /// <summary>
    /// Battery power per kg of battery weight (Wh/kg)
    /// </summary>
    public const double currentBatteryCapacityPerKg = 200;

    /// <summary>
    /// The air density factor used for air at the same altitude as Pretoria.
    /// </summary>
    public const double airDensityFactor = 0.002378;
}
