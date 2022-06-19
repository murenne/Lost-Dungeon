using UnityEngine;

//MARKER This Script has attached to EMPTY GAMEOBJECT
public class PlayerAttack : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
            //StartCoroutine(FindObjectOfType<CameraController>().CameraShakeCo(0.2f, 0.2f));
        }
    }

    private void Attack()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        //MARKER Weapon Rotation
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;//Radius -> Degree
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
