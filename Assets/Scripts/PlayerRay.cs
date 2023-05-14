using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponsType
{
    Strike,
    Mover,
    Bomb
}

public class PlayerRay : MonoBehaviour
{
    public EnemyManager enemyManager;
    [Tooltip("Арсенал")]
    [SerializeField] private PlayerArmory _playerArmory;
    [Tooltip("Камера игрока")]
    [SerializeField] private Camera _playerCamera;
    [Tooltip("Сила отталкивания")]
    [SerializeField] private float _force;

    public Animator _animatorMover;
    public Animator _animatorStriker;

    private Transform curObj;
    private float mass;
    private WeaponsType _currentWeaponsType;

    void LateUpdate()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition); // Луч из камеры игрока в позицию курсора мыши на экране
        Debug.DrawRay(ray.origin, ray.direction * 60f, Color.red); // Визуализация луча в сцене

        if (Input.GetMouseButton(0) && (_playerArmory.CurrentGunIndex == 1 || _playerArmory.CurrentGunIndex == 2)) // Удерживать левую кнопку мыши
        {
            MouseDrag(ray);
        }
        else if (Input.GetMouseButtonDown(0) && _playerArmory.CurrentGunIndex == 0) // Нажата левая кнопка мыши
        {
            MouseDown(ray);
        }
        else
        {
            MouseUp();
        }

    }
    private void MouseDown(Ray ray)
    {
        Debug.Log("OnMouseDown");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.rigidbody)
            {
                _animatorStriker.SetTrigger("Strike");
                StartCoroutine(Strike(hit));
            }
        }
    }

    private IEnumerator Strike(RaycastHit hit)
    {
        yield return new WaitForSeconds(0.3f);

        GameObject armor = _playerArmory.Guns[_playerArmory.CurrentGunIndex].gameObject; // Рука
        Debug.Log(armor.name);
        
        Rigidbody rigidbody = hit.rigidbody; // Противник
        rigidbody.AddForce(-rigidbody.velocity * _force, ForceMode.Impulse);

        if (rigidbody.GetComponent<EnemyAnimal>()._experienceLoot.GetComponent<ExperienceLoot>() is ExperienceLoot loot)
        {
            if (loot.EmotionType == EmotionType.Negative)
                PositiveCounter.Instance.AddPositiveEmotion(loot.ExperienceValue);
            else
                NegativeCounter.Instance.AddNegativeEmotion(loot.ExperienceValue);
        }

        //enemyManager.RemoveEnemy(rigidbody.GetComponent<EnemyAnimal>());
        //Destroy(rigidbody, 1f);

        //Vector3 toTarget = armor.transform.position - rigidbody.velocity; //Угол между целью и рукой
        //Quaternion targetRotation = Quaternion.LookRotation(toTarget, Vector3.up); // Целевой угол поворота
        //armor.transform.rotation = Quaternion.Lerp(armor.transform.rotation, targetRotation, 5f); // Поворачиваем руку в сторону удара 
    }


    private void MouseDrag(Ray ray)
    {
        Debug.Log("OnMouseDrag");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.rigidbody && !curObj)
            {
                //Animator animator = _playerArmory.Guns[1].transform.GetComponent<Mover>().Animator;
                _animatorMover.SetTrigger("Drag");
                
                curObj = hit.transform;
                curObj.position += new Vector3(0, 0.3f, 0); // немного приподнимаем выбранный объект

                Rigidbody rigidbody = curObj.GetComponent<Rigidbody>();
                mass = rigidbody.mass; // запоминаем массу объекта
                rigidbody.mass = 0.0001f; // убираем массу, чтобы не сбивать другие объекты
                rigidbody.useGravity = false; // убираем гравитацию
                rigidbody.freezeRotation = true; // заморозка вращении 
            }
        }

        if (curObj)
        {
            Debug.Log("Cargo: True");
            _animatorMover.SetBool("Cargo", true);
            Vector3 mousePosition = _playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _playerCamera.transform.position.y));
            curObj.GetComponent<Rigidbody>().MovePosition(new Vector3(mousePosition.x, curObj.position.y + Input.GetAxis("Mouse ScrollWheel") * 5f, mousePosition.z));
        }
        else
        {
            Debug.Log("Cargo: False");
            _animatorMover.SetBool("Cargo", false);
        }
    }

    private void MouseUp()
    {
        Debug.Log("OnMouseUp");
        Debug.Log("Cargo: False");
        _animatorMover.SetBool("Cargo", false);
        if (curObj)
        {
            _animatorMover.SetTrigger("Drop");

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



