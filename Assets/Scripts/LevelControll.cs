using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelControll : MonoBehaviour
{
    public  Text _text;
    //private int levelNumber;
    // Start is called before the first frame update
    void Start()
    {
  
        _text = GetComponent<Text>();
        //deleteAllCache();
        reStart();

    }

    void deleteAllCache()
    {
        PlayerPrefs.DeleteAll();
    }
    void reStart()
    {
        if (!PlayerPrefs.HasKey("LevelNumber"))
        {
            PlayerPrefs.SetInt("LevelNumber", 1);
        }
        
        UpdataLevelNumber(PlayerPrefs.GetInt("LevelNumber"));
    }
   
    void UpdataLevelNumber(int LevelNumber)
    {
       
        _text.text = LevelNumber.ToString();
        Debug.Log(_text.text);
    }



    // Update is called once per frame
    void Update()
    {
        

    }
}
