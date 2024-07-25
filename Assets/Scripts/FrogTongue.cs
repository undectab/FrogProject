using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongue : MonoBehaviour
{

    [SerializeField] private bool isMove;
    [SerializeField] private bool isMoveBackFrog;
    [SerializeField] private bool isMasterFruitTouched;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float rotationY;
    [SerializeField] private float distanceArrow = 0.1f;
    [SerializeField] private float distanceFrog = 0.05f;
    [SerializeField] private Vector3 baseRotation;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private bool isArrowTouch;
    public string frogColorName;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        baseRotation = transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        UpdateTransform();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject _otherobj = other.gameObject;
        if (_otherobj.CompareTag("Fruit"))
        {
            TriggerFruit(_otherobj.GetComponent<Fruit>());
        }
        else if (_otherobj.CompareTag("Frog"))
        {
            if (_otherobj.GetComponent<Frogs>().frogColorName != frogColorName)
                MoveBackFrog();
        }



    }
    private void OnTriggerStay(Collider other)
    {
        GameObject _otherobj = other.gameObject;
        if (_otherobj.CompareTag("Arrow"))
        {
            if (_otherobj.GetComponent<Arrow>().arrowColor == frogColorName)
            {
                if (DistanceCalc_float(_otherobj) <= distanceArrow)
                {
                    StartCoroutine(TriggerArrow(_otherobj));
                    
                }
            }
            else
            {
                MoveBackFrog();
            }

        }
        else if (_otherobj.CompareTag("Frog") && DistanceCalc_float(_otherobj) <= distanceFrog)
        {
            if (_otherobj.GetComponent<Frogs>().frogColorName == frogColorName)
                TriggerFrog();
            else
                MoveBackFrog();
        }
    }
    float DistanceCalc_float(GameObject _object)
    {
        return Vector3.Distance(transform.position, _object.transform.position); ;
    }
    public void OnClickedFrog()
    {
        isMove = true;
    }
    void UpdateTransform()
    {
        if (isMove)
        {
            transform.position += moveSpeed * Time.deltaTime * transform.forward;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
        }
    }
    void TriggerFruit(Fruit _fruit)
    {
        if (_fruit.fruitColorName == frogColorName)
        {

            if (_fruit.furitRole == "Master")
            {
                rotationY = transform.rotation.eulerAngles.y + 180;
                _fruit.UpdateMoveBool();
                _fruit.UpdateRotationY(rotationY);
                _fruit.moveSpeed = moveSpeed;
                _fruit.InteractionAnim();
                isMasterFruitTouched = true;
                UpdateRotationY(rotationY);
                isMoveBackFrog = true;
            }
            else
            {
                _fruit.ToungeTouched(moveSpeed, transform.rotation.eulerAngles.y + 180);
            }

        }
        else
        {
            MoveBackFrog();
        }
    }
    void MoveBackFrog()
    {
        rotationY = transform.rotation.eulerAngles.y + 180;

        isMoveBackFrog = true;
        UpdateRotationY(rotationY);
    }

    IEnumerator TriggerArrow(GameObject _arrow)
    {
        float _rotY = 0;
        if (!isArrowTouch)
        {
            if (isMoveBackFrog)
            {
                _rotY = _arrow.GetComponent<Arrow>().otherAngle + 180;
                lineRenderer.positionCount--;
            }
            else
            {

                _rotY = _arrow.GetComponent<RectTransform>().rotation.eulerAngles.y+180;
                lineRenderer.positionCount++;
            }
            isArrowTouch = true;
        }
        
        print("last" + _rotY);
        UpdateRotationY(_rotY);
        if (isMasterFruitTouched)
            _arrow.GetComponent<Arrow>().DestroyArrow();
        yield return new WaitForSeconds(0.2f);
        isArrowTouch = false;
    }
    public void TriggerFrog()
    {
        if (isMove)
        {
            if (isMasterFruitTouched)
            {
                Destroy(this.gameObject);
            }
            transform.rotation = Quaternion.Euler(baseRotation);
            isMoveBackFrog = false;
            isMove = false;
        }

    }
    public void UpdateRotationY(float _rotationY)
    {
        if (isMoveBackFrog)
        {
            //_rotationY += 90;
        }

        transform.rotation = Quaternion.Euler(0, _rotationY, 0);
    }

}
