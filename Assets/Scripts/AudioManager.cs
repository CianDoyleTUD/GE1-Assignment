/*  
    AudioManager.cs

    This script is responsible for processing the audio which will be visualised.
    It takes in game objects which will be used as the visualiser entities and 
    based on different properties of the audio, will affect the size of 
    these visualisers and will emit particles under a certain condition.
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source; // AudioSource for picking audio to play
    public ParticleSystem particle; // Particle system for emitting particles from visualiser
    public List<GameObject> visualisers; // List of game objects to use as visualisers
    public Vector3 minScale = new Vector3(1.0f, 1.0f, 1.0f); // Minimum size for visualiser objects
    public float volume = 0; // Custom "volume" variable, used in GenerateMesh.cs
    private float[] samples = new float[512]; // Array for holding audio frequency data
    private float multiplier = 1000.0f; // How much the audio affects the scale
    private float threshold = 5.0f; // Frequency threshold for drawing particles

    // Returns a list of frequency bands for an audio sample array given the number of visualisers 
    float[] getFrequencyBands(float[] list) 
    {
        // Create array for frequency bands based on number of visualisers
        int length = visualisers.Count;
        float[] frequencyBands = new float[length];

        int index = 0;
        // Loop through frequency bands and calculate Bands 
        for (int i = 0; i < length; i++)
        {
            float total = 0;
            // Each successive audio band has 2^i samples (because of hoq frequency bands are defined)
            int count = (int)Mathf.Pow(2, i) * 2;
            // Get the average value of each band
            for(int j = 0; j < count; j++)
            {
                total = list[index] * (index + 1);
                index++;
            }
            float avg = total / index;
            frequencyBands[i] = avg * multiplier;
        }
        return frequencyBands;
    }

    void Update()
    {
        // Get audio spectrum data
        samples = new float[512];
        AudioListener.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        // Get the "loudness" (not volume property) of the audio, aka the average amplitude of the frequencies
        float total = 0;

        foreach (var sample in samples) 
        {
            total += sample * multiplier;
        }

        volume = Mathf.Clamp((total / 512), 0.0f, 1.0f);

        // Get frequency band values
        float[] frequencyBands = getFrequencyBands(samples);
        int counter = 0;

        // Iterate through each visualiser game object
        foreach (GameObject obj in visualisers)
        {
            // Transform the scale of the visualiser based on it's assigned frequency band
            float size = frequencyBands[counter];
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, new Vector3(size, size, size) + minScale, 3.0f * Time.deltaTime);
            // Emit particles if the frequency band is particularly strong
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
