using UnityEngine;
using UnityEngine.UI;

class EndgameMenu : MonoBehaviour
{
    GameManager gm;

    [SerializeField]
    Text title;

    [SerializeField]
    Text description;

    void OnEnable()
    {
        gm = GameManager.GetInstance();
        if (gm.timer.TimeLimitReached())
        {
            title.text = "You lost!!";
            description.text =
                "You couldn't arrive at your ship fast enough and ran out of oxygen...";
        }
        else
        {
            title.text = "You won!!";
            description.text =
                "You could arrive at on time and escaped the planet!";
        }
    }
}
