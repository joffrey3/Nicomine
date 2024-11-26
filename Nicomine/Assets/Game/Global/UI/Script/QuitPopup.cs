using UnityEngine;
using UnityEngine.UI;


public class QuitPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
