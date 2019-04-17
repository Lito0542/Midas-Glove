using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Visualizer : MonoBehaviour {

    [Header("[Audio Info]")]
    public AudioSource audio_;
    float[] samples_;

    [Header("[Spectrum]")]
    public Transform SpectrumParent;
    public int maxCount = 64;
    public GameObject spectrumCubePrefab;
    GameObject[] spectrumCube;
    public float maxScale = 20;

    [Header("[Param]")]
    public Transform[] paramCube = new Transform[8];
    public Material[] paramMat = new Material[8];
    float[] freqBand = new float[8];
    public float startScale, scaleMultiplier;

    [Header("[Buffer]")]
    public Transform[] bufferCube = new Transform[8];
    public Material[] bufferMat = new Material[8];
    float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];

    [Header("[Audio Band]")]
    float[] freqBandHighest = new float[8];
    float[] audioBand = new float[8];
    float[] audioBandBuffer = new float[8];

    [Header("[Lights]")]
    public Light redLight;
    public Light blueLight;
    public float minIntensity, maxIntensity;

    void Start ()
    {
        spectrumCube = new GameObject[maxCount];
        samples_ = new float[maxCount];

        for(int i = 0; i < 8; i++)
        {
            paramMat[i] = paramCube[i].GetComponent<Renderer>().material;
            bufferMat[i] = bufferCube[i].GetComponent<Renderer>().material;
        }

        audio_ = GetComponent<AudioSource>();
        for(int i = 0; i < maxCount; i++)
        {
            GameObject newSampleCube = Instantiate(spectrumCubePrefab);
            newSampleCube.transform.position = transform.position;
            newSampleCube.transform.SetParent(SpectrumParent);
            newSampleCube.name = "SampleCube" + i;
            transform.eulerAngles = new Vector3(0, -(360.0f / maxCount) * i, 0);
            newSampleCube.transform.position += newSampleCube.transform.forward * 0.4f;
            newSampleCube.SetActive(false);
            spectrumCube[i] = newSampleCube;
        }
        transform.eulerAngles = new Vector3(0, -90, 0);
    }

	void Update ()
    {
        if (audio_.clip == null)
            return;
        GetSpectrum();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();

        for (int i = 0;i< maxCount; i++)
        {
            if(spectrumCube != null)
            {
                spectrumCube[i].transform.localScale = new Vector3(0.005f, (samples_[i] * maxScale) * 2, 0.005f);
                spectrumCube[i].transform.localPosition =
                    new Vector3(spectrumCube[i].transform.localPosition.x, (samples_[i] * maxScale), spectrumCube[i].transform.localPosition.z);
            }
        }

        for(int i = 0; i < 8; i++)
        {
            paramCube[i].localScale = new Vector3(0.1f, (freqBand[i] * scaleMultiplier) + startScale, 0.1f);
            bufferCube[i].localScale = new Vector3(0.1f, (bandBuffer[i] * scaleMultiplier) + startScale, 0.1f);

            //Color _color = bufferMat[i].color * Mathf.LinearToGammaSpace(audioBandBuffer[i]);
            Color _color = new Color(audioBandBuffer[i], audioBandBuffer[i], audioBandBuffer[i]);
            bufferMat[i].SetColor("_EmissionColor", _color);
            //_color = paramMat[i].color * Mathf.LinearToGammaSpace(audioBand[i]);
            _color = new Color(audioBand[i], audioBand[i], audioBand[i]);
            paramMat[i].SetColor("_EmissionColor", _color);

            paramCube[i].transform.localPosition =
                new Vector3(paramCube[i].transform.localPosition.x, ((freqBand[i] * scaleMultiplier) + startScale) * 0.5f, paramCube[i].transform.localPosition.z);
            bufferCube[i].transform.localPosition =
                new Vector3(paramCube[i].transform.localPosition.x, ((bandBuffer[i] * scaleMultiplier) + startScale) * 0.5f, paramCube[i].transform.localPosition.z);

            redLight.intensity = audioBandBuffer[0] * (maxIntensity - minIntensity) + minIntensity;
            blueLight.intensity = audioBand[0] * (maxIntensity - minIntensity) + minIntensity;
        }
    }

    void GetSpectrum()
    {
        audio_.GetSpectrumData(samples_, 0, FFTWindow.Blackman);
    }

    void CreateAudioBands()
    {
        for(int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
                freqBandHighest[i] = freqBand[i];
            audioBand[i] = (freqBand[i] / freqBandHighest[i]);
            audioBandBuffer[i] = bandBuffer[i] / freqBandHighest[i];
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++) {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if(i == 7)
                sampleCount += 2;
            for(int j = 0; j < sampleCount; j++)
            {
                average += samples_[count] * (count +1);
                count++;
            }
            average /= count;
            freqBand[i] = average * 10;
        }
    }

    void BandBuffer()
    {
        for(int g = 0; g < 8; ++g)
        {
            if(freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f
;            }
            if(freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    public void VisualizerActive(bool on)
    {
        for (int i = 0; i < maxCount; i++)
        {
            spectrumCube[i].gameObject.SetActive(on);
        }
        for (int i = 0; i < 8; i++)
        {
            paramCube[i].gameObject.SetActive(on);
            bufferCube[i].gameObject.SetActive(on);
        }
        blueLight.gameObject.SetActive(on);
        redLight.gameObject.SetActive(on);
    }
}
