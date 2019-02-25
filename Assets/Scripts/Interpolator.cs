using System.Collections.Generic;


public class Interpolator
{
    private double samplingStep;

    private double sample;

    // Memory
    private bool firstRun = true;
    private double previousX;
    private double previousY;


    public Interpolator(double samplingStep)
    {
        this.samplingStep = samplingStep;
    }


    public double[] Interpolate(double x, double y)
    {
        if (firstRun)
        {
            this.sample = x + samplingStep;
            this.previousX = x;
            this.previousY = y;
            this.firstRun = false;
            return new double[] {y};
        }

        List<double> interpolated = new List<double>();
        while (sample <= x)
        {
            interpolated.Add(linear(sample, previousX, x, previousY, y));
            sample += samplingStep;
        }

        return interpolated.ToArray();
    }


    // Linear interpolate 2 values. Based on https://stackoverflow.com/a/12838328.
    static public double linear(double x, double x0, double x1, double y0, double y1)
    {
        if ((x1 - x0) == 0)
        {
            return (y0 + y1) / 2;
        }
        return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
    }
}
