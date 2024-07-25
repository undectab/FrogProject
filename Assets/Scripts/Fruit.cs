using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public string furitRole;
    public string fruitColorName;
    public float moveSpeed;
    [SerializeField]private bool isMove;
    [SerializeField]private bool isToungeTouched;
    private Animator fruitAnimator;
    [SerializeField] private float distanceArrow = 0.28f;
    public GameObject cellGameObject;
    [Header("Fruit Material")]
    public Material materialBlue, materialGreen, materialRed, materialYellow;
    private bool isArrowTouch;

    private void Start()
    {
        fruitAnimator = GetComponent<Animator>();
        Renderer renderer = GetComponent<Renderer>();
        switch (fruitColorName)
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
    private void FixedUpdate()
    {
        UpdateMove();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject _other = other.gameObject;
        if (_other.gameObject.CompareTag("Frog"))
        {
            if(furitRole == "Master")
                _other.GetComponent<Frogs>().DestroyFrog();
            cellGameObject.GetComponent<Cell>().DestroyCell();
            StartCoroutine(DestroyIE());
        }else if (_other.gameObject.CompareTag("Fruit") && _other.GetComponent<Fruit>().fruitColorName == fruitColorName)
        {
            _other.GetComponent<Fruit>().UpdateMoveBool();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        GameObject _otherobj = other.gameObject;
        float _distance = Vector3.Distance(transform.position, _otherobj.transform.position);
        if (_otherobj.CompareTag("Arrow") && _distance <= distanceArrow)
        {
            StartCoroutine(TriggerArrow(_otherobj));
        }
    }
    IEnumerator TriggerArrow(GameObject _obj)
    {
        if (!isArrowTouch)
        {
UpdateRotationY(_obj.GetComponent<Arrow>().otherAngle + 180);
            isArrowTouch = true;
        }
        yield return new WaitForSeconds(0.2f);
        isArrowTouch= false;
            
    }
    void UpdateMove()
    {
        if (isMove)
            transform.position += moveSpeed * Time.deltaTime * transform.forward;
    }
    public void UpdateRotationY(float _rotationY)
    {
        transform.rotation = Quaternion.Euler(0, _rotationY, 0);
    }
    public void ToungeTouched(float _moveSpeed,float _rotationY)
    {
        if (!isToungeTouched)
        {
            moveSpeed = _moveSpeed;
            UpdateRotationY(_rotationY);
            InteractionAnim();
            isToungeTouched = true;
        }
    }
    public void UpdateMoveBool()
    {
        isMove = true;
    }
    IEnumerator DestroyIE()
    {
        fruitAnimator.SetBool("destroyBool", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    public void InteractionAnim()
    {
        fruitAnimator.SetBool("interactionBool", true);
    }
}
