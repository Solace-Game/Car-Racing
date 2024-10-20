using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject buttonParent;
    [SerializeField] public GameObject buttonLv;
  public void PlayGame(){
    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    buttonParent.SetActive(false);
    buttonLv.SetActive(true);
  }
  public void QuitGame(){
    Debug.Log("Quiting Game");
    Application.Quit();
  }
}
