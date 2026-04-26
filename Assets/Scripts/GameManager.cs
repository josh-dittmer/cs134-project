using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EnableEnemyMovement() {
        // find all enemies and enable their movement
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            // enable enemy movement
            enemy.GetComponentInParent<NavMeshAgent>().enabled = true;
            // set game started to true
            enemy.GetComponent<Animator>().SetBool("GameStarted", true);
        }
    }
}
