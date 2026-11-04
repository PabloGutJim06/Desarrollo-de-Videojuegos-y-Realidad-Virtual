using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    [SerializeField] AudioSource audioSource;
    private bool pausado = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pausado == false)
            {
                pausarJuego();
            } else
            {
                reanudar();
            }
                
        }
    }

    public void pausarJuego()
    {
        Time.timeScale = 0f;
        audioSource.Pause();
        menuPausa.SetActive(true);
        pausado = true;
    }

    public void reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        audioSource.UnPause();
        pausado = false;
    }

    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
