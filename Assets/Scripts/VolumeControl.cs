using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider volumeSlider;

    VolumeData volumeData;

    private const string AmbientVol = "AmbientVolume";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeData = new VolumeData();
        mixer.SetFloat(AmbientVol, Mathf.Log10(volumeSlider.value) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            SaveVolume(volumeData);
        }
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            volumeData = LoadVolume();
            volumeSlider.value = volumeData.ambientVolume;
        }
    }

    public void SetAmbientVolume(float volume)
    {
        mixer.SetFloat(AmbientVol, Mathf.Log10(volume) * 20);
        volumeData.ambientVolume = volume;
    }

    [System.Serializable]
    public class VolumeData
    {
        public float ambientVolume;
        public float sfxVolume;
        public float musicVolume;
        public int score;
        public string message = "This is fun!";
    }
    
    public void SaveVolume(VolumeData data)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = "volume.json"; //Path.Combine(Application.persistentDataPath, "volume.json");

        File.WriteAllText(path, json);
        Debug.Log("Data saved to:" + path);
    }

    public VolumeData LoadVolume()
    {
        string path = "volume.json"; //Path.Combine(Application.persistentDataPath, "volume.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            VolumeData data = JsonUtility.FromJson<VolumeData>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return new VolumeData();
        }
    }
}
