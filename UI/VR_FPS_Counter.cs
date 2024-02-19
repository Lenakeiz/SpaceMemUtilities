using UnityEngine;
using System.Collections;
using TMPro;

public class VR_FPS_Counter : MonoBehaviour
{
    private float fpsCount;
    [SerializeField] private TextMeshProUGUI fpsText;

    private IEnumerator Start()
    {        
        while (true)
        {
            fpsCount = 1f / Time.unscaledDeltaTime;
            fpsText.text = "FPS: " + Mathf.RoundToInt(fpsCount).ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}