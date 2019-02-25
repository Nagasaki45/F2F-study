using System;

// 2nd order HP and LP Butterworth filter
// Based on https://stackoverflow.com/a/19155926
// and https://stackoverflow.com/a/20932062
public class Butterworth
{
    private double a0, a1, a2, b0, b1;

    // Filter memory
    private double x1 = 0;
    private double x2 = 0;
    private double y1 = 0;
    private double y2 = 0;


    public Butterworth(double cutOff, int sampleRate, PassType passType)
    {
        double c = 1.0f / Math.Tan(Math.PI * cutOff / sampleRate);
        a0 = 1.0 / (1.0 + Math.Sqrt(2) * c + c * c);
        a1 = 2 * a0;
        a2 = a0;
        b0 = 2.0 * (c * c - 1.0) * a0;
        b1 = -(1.0 - Math.Sqrt(2) * c + c * c) * a0;

        if (passType == PassType.Highpass)
        {
            a0 = a0 * c * c;
            a1 = -a1 * c * c;
            a2 = a2 * c * c;
        }
    }


    public enum PassType
    {
        Highpass,
        Lowpass,
    }


    public double Filter(double newInput)
    {
        double newOutput = a0 * newInput + a1 * x1 + a2 * x2 + b0 * y1 + b1 * y2;

        this.x2 = this.x1;
        this.x1 = newInput;

        this.y2 = this.y1;
        this.y1 = newOutput;

        return newOutput;
    }
}
