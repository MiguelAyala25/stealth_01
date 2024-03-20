using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Victory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI victory;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            victory.gameObject.SetActive(true);

        }
    }
}
