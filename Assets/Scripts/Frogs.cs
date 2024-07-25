using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Frogs : MonoBehaviour
{
    private Animator frogAnimator;
    public string frogColorName;
    public GameObject frogToungeObject;
    [Header("Frog Material")]
    public Material materialBlue, materialGreen, materialRed, materialYellow;
    public GameObject cellGameObject;
    public GameObject frogGameObject;

    private void Start()
    {
        frogAnimator = GetComponent<Animator>();
        Renderer renderer = transform.GetChild(0).GetComponent<Renderer>();
        switch (frogColorName)
        {
            case "Blue":
                renderer.material = materialBlue;
                break;
            case "Green":
                renderer.material = materialGreen;
                break;
            case "Red":
                renderer.material = materialRed;
                break;
            case "Yellow":
                renderer.material = materialYellow;
                break;
        }
    }
    public void MouseOnClicked()
    {

        frogAnimator.SetBool("MouthOpened", true);
        frogToungeObject.GetComponent<FrogTongue>().OnClickedFrog();
    }
    public void DestroyFrog()
    {
        cellGameObject.GetComponent<Cell>().DestroyCell("Frog");
        StartCoroutine(DestroyFrogIE());
    }
    IEnumerator DestroyFrogIE()
    {
        frogAnimator.SetBool("destroyBool", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(frogGameObject);
        Destroy(this.gameObject);
    }
}
