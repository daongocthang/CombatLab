using System;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private HeroCombat _combat;
    private Character _character;
    private Vector3 _dest;

    public void Start()
    {
        _combat = GetComponent<HeroCombat>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    _combat.forceStop();
                    _combat.character.forceMoving = true;

                    _dest = hit.point;
                    _combat.character.SetDestination(_dest);
                    _combat.character.Steering(_dest);
                }
            }
        }

        if (Vector3.Distance(transform.position, _dest) < 0.1f)
        {
            _combat.character.forceMoving = false;
        }
    }
}