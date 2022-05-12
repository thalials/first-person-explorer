using UnityEngine;
using UnityEngine.UI;

class EndgameMenu : MonoBehaviour
{
    GameManager gm;

    [SerializeField]
    Text title;

    void OnEnable()
    {
        gm = GameManager.GetInstance();
        title.text = "You lost";
    }
}
