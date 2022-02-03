using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController Singleton;
    [SerializeField]private Text TextCountPlant;
    private int _countPlant;

    private void Awake()
    {


        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this;
        DontDestroyOnLoad(gameObject);
        
    }

    public void UpdateCountPlant()
    {
        _countPlant++;
        TextCountPlant.text = "Собрано: " + _countPlant;
    }
}
