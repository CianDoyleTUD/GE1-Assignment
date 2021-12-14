using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioReaction : MonoBehaviour
{
    public AudioSource source;
    public ParticleSystem particle;
    private float[] samples = new float[64];
    private int bass = 8; 
    private float multiplier = 150.0f; // How much the audio affects the scale
    private float threshold = 5.0f;

    void Start()
    {

    }

    System.Collections.IEnumerator AudioVisualsRoutine()
    {
        while(true)
        {
            //transform.localScale = new Vector3(size, size, size);
            yield return new WaitForSeconds(1);
        }
    }

    // Returns a sub list of the subset of the given list between given indexes
    float[] sliceFloatArray(float[] list, int startIndex, int endIndex)
    {
        float[] toReturn = new float[(endIndex - startIndex) + 1];
        for(int i = startIndex; i <= endIndex; i++)
        {
            toReturn[i] = list[i];
        }
        return toReturn;
    }

    // Returns a dictionary containing the frequency values
    Dictionary<string, float> getFrequencyValues(float[] list) 
    {
        int length = list.Length;
        int div = length / 6;

        // Divid the sample array into 6 portions
        float subBass = sliceFloatArray(list, 0, div - 1).Max() * multiplier;
        float bass = sliceFloatArray(list, div, div*2 - 1).Max() * multiplier;
        float lowerMid =  sliceFloatArray(list, div*2, div*3 - 1).Max() * multiplier;
        float mid = sliceFloatArray(list, div*3, div*4 - 1).Max() * multiplier;
        float higherMid = sliceFloatArray(list, div*4, div*5 - 1).Max() * multiplier;
        float prescence = sliceFloatArray(list, div*5, div*6 - 1).Max()* multiplier;

        Dictionary<string, float> audioValues = new Dictionary<string, float>();

        audioValues.Add("Sub bass", subBass);
        audioValues.Add("Bass", bass);
        audioValues.Add("Lower mid", lowerMid);
        audioValues.Add("Mid", mid);
        audioValues.Add("Higer mid", higherMid);
        audioValues.Add("Presence", prescence);

        return audioValues;
    }

    // Update is called once per frame
    void Update()
    {
        samples = new float[64];

        AudioListener.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        Dictionary<string, float> audioValues = getFrequencyValues(samples);
        
        float size = audioValues["Sub bass"];
        if (size > threshold)
        {
            ParticleSystem particleSystem = ParticleSystem.Instantiate<ParticleSystem>(particle, transform.position, transform.rotation);
            particleSystem.transform.parent = this.transform;
            particleSystem.startSize = Mathf.Clamp(size, 0, 3.0f);
            var no = particleSystem.noise;
            no.strength =  Mathf.Clamp(size, 0, 2.0f);
            Destroy(particleSystem, 1.0f);
        }
        transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(size, size, size), 5.0f * Time.deltaTime);
    }
}
