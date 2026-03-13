using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.IO.Compression;

public class RuntimeAudioLoader : MonoBehaviour
{
    public static RuntimeAudioLoader Instance;

    public string audiofolderName;
 

    Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

    [SerializeField]private string[] audioZipNams;
    public string CurrentaudioZipName;


    void Awake()
    {
        Instance = this;
         DontDestroyOnLoad(gameObject);
        
    }

    public IEnumerator Start() 
    {

        string langStr = PlayerPrefs.GetString("PlayschoolLanguageAudio");

        switch (langStr)
        {
            case "English":
                CurrentaudioZipName = audioZipNams[0];
                break;

            case "Hindi":
                CurrentaudioZipName = audioZipNams[1];
                break;

            case "Marathi":
                CurrentaudioZipName = audioZipNams[2];
                break;
            case "Bhojpuri":
                CurrentaudioZipName = audioZipNams[3];
                break;
                


        }
        yield return StartCoroutine(CheckDownloadExtract());
        StartCoroutine(LoadAllAudio());
    }

   //IEnumerator Start()
   // {
   //     yield return StartCoroutine(CheckDownloadExtract());
   //     yield return StartCoroutine(LoadAllAudio());
   // }
  

       IEnumerator CheckDownloadExtract()
    {
        string bundleFolder = Path.Combine(Application.persistentDataPath, "AudioBundles");
        string zipPath = Path.Combine(bundleFolder, CurrentaudioZipName + ".zip");
        string extractPath = Path.Combine(bundleFolder, CurrentaudioZipName);

        if (!Directory.Exists(bundleFolder))
            Directory.CreateDirectory(bundleFolder);

        if (!Directory.Exists(extractPath))
        {
            if (!File.Exists(zipPath))
            {
                yield return StartCoroutine(DownloadZip(zipPath));
            }

            ZipFile.ExtractToDirectory(zipPath, extractPath);

            File.Delete(zipPath);
        }
    }

      IEnumerator DownloadZip(string savePath)
    {
        string url = $"https://playschool-audio.s3.ap-south-1.amazonaws.com/{audiofolderName}/{CurrentaudioZipName}.zip";

        UnityWebRequest www = UnityWebRequest.Get(url);

        www.downloadHandler = new DownloadHandlerFile(savePath);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
    }

    // void ExtractZipIfNeeded()
    // {
    //     string zipPath = Path.Combine(Application.persistentDataPath, Language + ".zip");
    //     string extractPath = Path.Combine(Application.persistentDataPath, Language);

    //     if (File.Exists(zipPath) && !Directory.Exists(extractPath))
    //     {
    //         Debug.Log("Extracting ZIP: " + zipPath);

    //         ZipFile.ExtractToDirectory(zipPath, extractPath);

    //         Debug.Log("Extraction complete");

    //         // File.Delete(zipPath);
    //     }
    // }


    IEnumerator LoadAllAudio()
    {

        audioDict.Clear();
          string path = Path.Combine(Application.persistentDataPath, "AudioBundles", CurrentaudioZipName);

        if (!Directory.Exists(path))
        {
            Debug.LogError("Audio folder not found: " + path);
            yield break;
        }
    
        string[] files = Directory.GetFiles(path, "*.mp3");

        foreach (string file in files)
        {
            yield return LoadClip(file);
        }
    }

    IEnumerator LoadClip(string filePath)
    {
        UnityWebRequest www =
            UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            yield break;
        }

        AudioClip clip = DownloadHandlerAudioClip.GetContent(www);

        string key = Path.GetFileNameWithoutExtension(filePath);

        audioDict[key] = clip;
    }

    public AudioClip GetClip(string name)
    {
        if (audioDict.ContainsKey(name))
            return audioDict[name];

        Debug.LogWarning("Audio not found: " + name);

        return null;
    }
}