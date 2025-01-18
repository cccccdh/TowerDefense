using UnityEngine;
using UnityEngine.UI;

public class TimeSpeed : MonoBehaviour
{
    [SerializeField]
    private Image X1;
    [SerializeField]
    private Image X2;

    private bool isFastSpeed = true;

    public void FastSpeed()
    {
        if (!isFastSpeed)
        {
            isFastSpeed = true;
            Time.timeScale = 2f;
            X1.gameObject.SetActive(false);
            X2.gameObject.SetActive(true);
        }
        else
        {
            isFastSpeed = false;
            Time.timeScale = 1f;
            X1.gameObject.SetActive(true);
            X2.gameObject.SetActive(false);
        }
    }
}
