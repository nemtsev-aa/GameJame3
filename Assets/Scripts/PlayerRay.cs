using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [Tooltip("Камера игрока")]
    [SerializeField] private Camera _playerCamera;
    private Transform curObj;
    private float mass;

    void LateUpdate()
    {
        RaycastHit hit;
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition); // Луч из камеры игрока в позицию курсора мыши на экране
        Debug.DrawRay(ray.origin, ray.direction * 60f, Color.red); // Визуализация луча в сцене
        
        if (Input.GetMouseButton(0)) // Удерживать левую кнопку мыши
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody && !curObj)
                {
                    curObj = hit.transform;
                    mass = curObj.GetComponent<Rigidbody>().mass; // запоминаем массу объекта
                    curObj.GetComponent<Rigidbody>().mass = 0.0001f; // убираем массу, чтобы не сбивать другие объекты
                    curObj.GetComponent<Rigidbody>().useGravity = false; // убираем гравитацию
                    curObj.GetComponent<Rigidbody>().freezeRotation = true; // заморозка вращения
                    curObj.position += new Vector3(0, 0.5f, 0); // немного приподымаем выбранный объект
                }
            }

            if (curObj)
            {
                Vector3 mousePosition = _playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _playerCamera.transform.position.y));
                curObj.GetComponent<Rigidbody>().MovePosition(new Vector3(mousePosition.x, curObj.position.y + Input.GetAxis("Mouse ScrollWheel") * 5f, mousePosition.z));
            }

        }
        else if (Input.GetMouseButtonDown(1)) // Нажата правая кнопка мыши
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.transform.GetComponent<Explosion>() is Explosion explosion)
                {
                    Debug.Log(explosion.gameObject.name);
                    explosion.Explode();
                }
            }
        }
        else if (curObj)
        {
            if (curObj.GetComponent<Rigidbody>())
            {
                curObj.GetComponent<Rigidbody>().freezeRotation = false;
                curObj.GetComponent<Rigidbody>().useGravity = true;
                curObj.GetComponent<Rigidbody>().mass = mass;
            }
            curObj = null;
        }
    }
}


