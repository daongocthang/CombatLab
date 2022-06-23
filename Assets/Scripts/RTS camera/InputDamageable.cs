using UnityEngine;

public class InputDamageable : MonoBehaviour
{
    private GameObject selectedHero;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity))
            {
                var targetable = hit.collider.GetComponent<IDamageable>();
                var heroCombat = selectedHero.GetComponent<HeroCombat>();
                if (targetable == null)
                {
                    heroCombat.enemy = null;
                }
                else
                {
                    heroCombat.enemy = targetable.GetGameObject();
                    heroCombat.movement.forceMoving = false;
                }
            }
        }
    }
}