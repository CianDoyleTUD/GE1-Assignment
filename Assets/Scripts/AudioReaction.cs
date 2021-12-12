using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReaction : MonoBehaviour
{
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[64];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        Debug.Log(spectrum[0].ToString() + " " + Mathf.Log(spectrum[0]));

        float size = spectrum[0] * 50;
        transform.localScale = new Vector3(size, size, size);
        //transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(size, size, size), 2.0f * Time.deltaTime);
        /*for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.Log(spectrum[i].ToString() + " " + Mathf.Log(spectrum[i]));
        }*/
    }
}
