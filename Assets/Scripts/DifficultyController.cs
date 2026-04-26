using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DifficultyController : MonoBehaviour
{
    // reference to the difficulty dropdown
    private TMP_Dropdown difficultyDropdown;

    public void Start() {
        // get difficulty dropdown
        difficultyDropdown = GetComponent<TMP_Dropdown>();

        // set difficulty upon startup
        SetDifficulty();
    }

    public void SetDifficulty() {
        // difficulty slider is 0 to 2. make adjustedDifficulty 1 to 5.
        float adjustedDifficulty = difficultyDropdown.value / 2f * 8f + 1f;

        // find all enemies and set their speed to the adjusted difficulty
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            enemy.GetComponentInParent<NavMeshAgent>().speed = adjustedDifficulty;
        }
    }
}
