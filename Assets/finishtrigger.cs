using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes

public class FinishLineTrigger : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject go;
    Collider mesh;

    private void Start()
    {
        mesh = GetComponent<Collider>();
        canvas.enabled = false;
        go.SetActive(false);
    }

    // When the car enters the trigger zone (finish line)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by "+other.name);

        if (other.CompareTag("Player"))  // Assuming your car has the "Player" tag
        {
            // Load the Main Menu scene
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()); // Replace "MainMenu" with the actual name of your main menu scene
            //SceneManager.LoadSceneAsync(1);
            canvas.enabled = true;
            go.SetActive(true);
        }
    }
}
