using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int stageID;

    public GameObject monster;
    public Vector3 monsterSpawnPos;

    public GameObject hero;
    public GameObject heroSpawnPos;

    public GameObject partyMem1;
    public Vector3 partyMem1SpawnPos;

    public GameObject partyMem2;
    public Vector3 partyMem2SpawnPos;

    public Controller_CharData charDataController;

    public static Model_CharData charDataAlex;
    public Model_CharData charDataFreya;
    public Model_CharData charDataMagnus;

   

    // Start is called before the first frame update

    void Awake()
    {
        GetStageID();
        GenerateMonster();
    }
    void Start()
    {
        /*charDataAlex = LoadCharData();*/
   

    }

    // Update is called once per frame
    void Update()
    {
        /*saveController(charDataController.charData1);*/

    }



    public void GenerateMonster()
    {
        if(monster != null)
        {
            Instantiate(monster, monsterSpawnPos, Quaternion.Euler(0,90,0));
        }
    }

    public void GenerateChars() 
    {

    }

    public void saveController(Model_CharData charData)
    {
        // Konversi objek CharDataModel menjadi string JSON
        string json = JsonConvert.SerializeObject(charData, Formatting.Indented);

        // Tentukan path file tempat menyimpan data
        // Ubah "path/to/your/savefile.json" sesuai dengan lokasi penyimpanan yang diinginkan
        string filePath = "Assets/SaveData/savefile.json";

        // Tulis data JSON ke dalam file
        File.WriteAllText(filePath, json);
    }

    public static Model_CharData LoadCharData()
    {
        // Tentukan path file tempat data disimpan
        string filePath = "Assets/SaveData/savefile.json";

        if (File.Exists(filePath))
        {
            // Baca semua teks dari file
            string json = File.ReadAllText(filePath);

            // Konversi string JSON kembali menjadi objek CharDataModel
            Model_CharData charData = JsonConvert.DeserializeObject<Model_CharData>(json);
            return charData;
        }
        else
        {
            Debug.Log("Gagal load");
        }
        return null;

    }

    public void GetStageID()
    {

    }
}