using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI endGameMessage;
    public float returnDelay = 3f;

    public void PlayerDied()
    {
        StartCoroutine(EndGameRoutine("You Died"));
    }

    public void BossDied()
    {
        StartCoroutine(EndGameRoutine("You Won"));
    }

    private System.Collections.IEnumerator EndGameRoutine(string message)
    {
        if (endGameMessage != null)
        {
            endGameMessage.text = message;
            endGameMessage.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(returnDelay);

        SceneManager.LoadScene("MainMenu");
        
    }
}
