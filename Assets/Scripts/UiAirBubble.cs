using UnityEngine;
using UnityEngine.UI;

public class UiAirBubble : MonoBehaviour
{
    [SerializeField]
    private Image airBubble1;
    [SerializeField]
    private Image airBubble2;
    [SerializeField]
    private Image airBubble3;


    public void UpdateAirBubbles(int currentAir)
    {
        airBubble1.enabled = currentAir >= 1;
        airBubble2.enabled = currentAir >= 2;
        airBubble3.enabled = currentAir >= 3;
    }
}
