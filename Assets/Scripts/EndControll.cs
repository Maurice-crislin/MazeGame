using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndControll : MonoBehaviour
{
    private BoxCollider2D _circleCollider2D;

    public Text _text;
    public Tilemap tilemap;//ÒýÓÃµÄTilemap
    void Start()
    {
        _circleCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("OnTriggerEnter");
            _circleCollider2D.enabled = false;
            //GameController.Instance.levelUpdateAndSet();
            PlayerPrefs.SetInt("LevelNumber", PlayerPrefs.GetInt("LevelNumber") + 1);
            SceneManager.LoadScene(0);
        }
        
    }
}
