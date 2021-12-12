using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioReaction : MonoBehaviour
{
    public AudioSource source;
    public float average = 0;
    public float[] samples = new float[64];
    private int bass = 8; 
    private float multiplier = 100.0f; 
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(AudioVisualsRoutine());
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
        
        Debug.Log(subSamples.Max().ToString());
        float size = subSamples.Max() * 150;
        transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(size, size, size), 5.0f * Time.deltaTime);
    }
}
