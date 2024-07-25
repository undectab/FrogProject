using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Arrow : MonoBehaviour
{
    public string arrowColor;
    public GameObject cellGameObject;
    private Animator arrowAnimator;
    public float otherAngle;
    public bool isFirstTouch;
    // Start is called before the first frame update
    void Start()
    {
        arrowAnimator = GetComponent<Animator>();
        Color color = new Color();
        switch (arrowColor)
        {
            case "Blue":
                color = Color.blue;
                break;
            case "Green":
                color = Color.green;
                break;
            case "Red":
                color = Color.red;
                break;
            case "Yellow":
                color = Color.yellow;
                break;
        }
        GetComponent<TextMeshPro>().color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DestroyArrow()
    {
        StartCoroutine(DestroyArrowIE());
    }
    IEnumerator DestroyArrowIE()
    {
        cellGameObject.GetComponent<Cell>().DestroyCell();
        arrowAnimator.SetBool("destroyBool", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Tounge" && other.GetComponent<FrogTongue>().frogColorName == arrowColor)
        {
            if (!isFirstTouch)
            {
                otherAngle = other.transform.rotation.eulerAngles.y;

                isFirstTouch = true;
            }
        }
    }
}
