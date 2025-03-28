using System;
using System.Runtime.InteropServices;

public sealed class Radiation
{
    public const double Emissivity = 1.0F;
    public const double Emissivity_97 = 0.97F;
    public const double ReflectedTemperature = 293.15F;
    public const double AtmosphericTemperature = 293.15F;
    public const double Distance = 1.0F;
    public const double RelativeHumidity = 0.5F;
    public const double ExternalOpticsTransmission = 1.0F;
    public const double ExternalOpticsTemperature = 293.15F;

    public static void CalculateFromFactoryLUT_SH(
        double toEmissivity,
        double toReflectedTemperature,
        double toAtmosphericTemperature,
        double toDistance,
        double toRelativeHumidity,
        double toExternalOpticsTransmission,
        double toExternalOpticsTemperature,
        float[] values, int from, int to)
    {
        int count = values.Length;
        IntPtr hValues = IntPtr.Zero;
        try
        {
            hValues = Marshal.AllocHGlobal(count * sizeof(float));
            Marshal.Copy(values, 0, hValues, count);
            CalculateFromFactoryLUT_SH(
                toEmissivity,
                toReflectedTemperature,
                toAtmosphericTemperature,
                toDistance,
                toRelativeHumidity,
                toExternalOpticsTransmission,
                toExternalOpticsTemperature,
                hValues, from, to);
            Marshal.Copy(hValues, values, 0, count);
        }
        finally
        {
            Marshal.FreeHGlobal(hValues);
        }
    }

    [DllImport("Radiation.dll", EntryPoint = "CalculateFromFactoryLUT_SH")]
    private static extern void CalculateFromFactoryLUT_SH(
        double toEmissivity,
        double toReflectedTemperature,
        double toAtmosphericTemperature,
        double toDistance,
        double toRelativeHumidity,
        double toExternalOpticsTransmission,
        double toExternalOpticsTemperature,
        IntPtr hValus, int from, int to);
}