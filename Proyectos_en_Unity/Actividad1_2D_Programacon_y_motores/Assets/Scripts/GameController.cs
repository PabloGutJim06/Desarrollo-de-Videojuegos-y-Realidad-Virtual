using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
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
        menuPausa.SetActive(true);
        pausado = true;
    }

    public void reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        pausado = false;
    }

    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
