using TMPro;
using UnityEngine;
using System.Collections;

public class OutputManager : MonoBehaviour
{
    public TextMeshProUGUI TextMeshProUGUI;

    public void Display(string data)
    {
        TextMeshProUGUI.text = data;

        StartCoroutine(WaitAndPrint());
    }

    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(5);
        TextMeshProUGUI.text = null;
    }
}
