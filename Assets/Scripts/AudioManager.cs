using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public ParticleSystem particle;
    public List<GameObject> visualisers; 
    public Vector3 minScale = new Vector3(1.0f, 1.0f, 1.0f); // Minimum size for visualiser objects
    public float volume = 0;
    private float[] samples = new float[512];
    private float multiplier = 1000.0f; // How much the audio affects the scale
    private float threshold = 5.0f;

    void Start()
    {

    }

    float[] getFrequencyValues(float[] list) 
    {
        int length = visualisers.Count;
        float[] frequencyValues = new float[length];
        int index = 0;

        for (int i = 0; i < length; i++)
        {
            float total = 0;
            int count = (int)Mathf.Pow(2, i) * 2;

            for(int j = 0; j < count; j++)
            {
                total = list[index] * (index + 1);
                index++;
            }
            float avg = total / index;
            frequencyValues[i] = avg * multiplier;
        }
        return frequencyValues;
    }

    // Update is called once per frame
    void Update()
    {
        samples = new float[512];

        AudioListener.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        float total = 0;

        foreach (var sample in samples) 
        {
            total += sample * multiplier;
        }

        volume = Mathf.Clamp((total / 512), 0.0f, 1.0f);

        float[] frequencyValues = getFrequencyValues(samples);
        int counter = 0;

        foreach (GameObject obj in visualisers)
        {
            float size = frequencyValues[counter];
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, new Vector3(size, size, size) + minScale, 3.0f * Time.deltaTime);
            
            if (size > threshold)
            {
                ParticleSystem particleSystem = ParticleSystem.Instantiate<ParticleSystem>(particle, obj.transform.position, obj.transform.rotation);
                particleSystem.transform.parent = obj.transform;
                var noise = particleSystem.noise;
                noise.strength =  Mathf.Clamp(size, 0, 5.0f);
                Destroy(particleSystem, 0.5f * Mathf.Clamp(size/5.0f, 1.0f, 2.0f));
            }
            counter++;
        }
    }
}
