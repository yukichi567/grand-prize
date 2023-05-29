using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class AutoDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.5f);  
    }
}