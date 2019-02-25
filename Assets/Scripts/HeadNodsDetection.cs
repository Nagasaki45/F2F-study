using UnityEngine;

public class HeadNodsDetection : MonoBehaviour {

    public int sampleRate;
    public double noddingThreshold;  // the minimum movement to detect a nod
    public double notNoddingThreshold;  // epsilon value

    private Interpolator interpolator;
    private Butterworth lowPassFilter;
    private Butterworth highPassFilter;

    private bool readyToNod;


    void Start()
    {
        interpolator = new Interpolator(1.0 / sampleRate);
        lowPassFilter = new Butterworth(4, sampleRate, Butterworth.PassType.Lowpass);
        highPassFilter = new Butterworth(1, sampleRate, Butterworth.PassType.Highpass);
    }


    void Update()
    {
        float now = Time.time;
        double value = (double) transform.position.y;
        foreach (double val in interpolator.Interpolate(now, value))
        {
            value = lowPassFilter.Filter(val);
            value = highPassFilter.Filter(value);

            // Nod happens upon negative movement, larger than threshold.
            if (readyToNod && value < -noddingThreshold)
            {
                readyToNod = false;
                Logger.Event("Nodding");
            }

            if (value < notNoddingThreshold && value > -notNoddingThreshold)
            {
                readyToNod = true;
            }
        }
    }
}
