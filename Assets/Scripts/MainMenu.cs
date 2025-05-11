using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public string levelToStart;

   public void PlayGame(){

    SceneManager.LoadScene(levelToStart);


   }
public void QuitGame(){

    Application.Quit();

    Debug.Log("Saindo do jogo");

}}



