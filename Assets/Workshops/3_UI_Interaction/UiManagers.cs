using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI tmpGUI;
    public string scene;
    public GameObject cube;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HelloWorld()
    {
        UnityEngine.Debug.Log("Hello World");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void Cube()
    {
        Instantiate(cube);
    }
}
