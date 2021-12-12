using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioReaction : MonoBehaviour
{
    public AudioSource source;
    public ParticleSystem particle;
    public float[] samples = new float[64];
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

    // Update is called once per frame
    void Update()
    {
        samples = new float[64];

        AudioListener.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        List<float> subSamples = new List<float>();

        for (int i = 0; i < bass; i++)
        {   
            subSamples.Add(samples[i]);
        }
        
        float size = subSamples.Max() * multiplier;
        if (size > threshold)
        {
            ParticleSystem particleSystem = ParticleSystem.Instantiate<ParticleSystem>(particle, transform.position, transform.rotation);
            particleSystem.transform.parent = this.transform;
            particleSystem.startSize = Mathf.Clamp(size, 0, 3.0f);
            var no = particleSystem.noise;
            no.strength =  Mathf.Clamp(size, 0, 2.0f);
            Destroy(particleSystem, 2.5f);
        }
        transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(size, size, size), 5.0f * Time.deltaTime);
    }
}
