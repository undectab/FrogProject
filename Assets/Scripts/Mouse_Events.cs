using UnityEngine;

public class MouseEvents : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private UIController _UIController;
    private void Start()
    {
        _UIController = GameObject.Find("Manager").GetComponent<UIController>();
    }
    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (hit.collider.gameObject.CompareTag("Frog"))
                {
                    hit.collider.gameObject.GetComponent<Frogs>().MouseOnClicked();
                }
                _UIController.MoveLeftController();
            }
        }
    }
}
