using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

using UnityEditor.Overlays;

public class MainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    // Makes MainManager accessible everywhere
    public static MainManager Instance;

    // New variable for color
    public Color TeamColor;

    private void Awake()
    {

        // Singleton design pattern!
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load initial color
        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    // Saves color to json file
    public void SaveColor()
    {
        // Created new instance of save data
        SaveData data = new SaveData();

        // Set savedata color
        data.TeamColor = TeamColor;

        // Converted data to json string
        string json = JsonUtility.ToJson(data);

        // Wrote json string to file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Loads color from json file

    public void LoadColor()
    {
        // Checks if file exists in path
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            // Takes all text from path
            string json = File.ReadAllText(path);

            // Turns all text into savedata from json
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Sets team color to color from json
            TeamColor = data.TeamColor;
        } 
    }


}