using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Shooting
{
    public class ShootingPlayerUnit : ShootingActerUnit
    {
        public new PlayerUnit Unit => base.Unit as PlayerUnit;
        CharacterController controller;

        public override void Init()
        {
            base.Init();
            controller = GetComponent<CharacterController>();
        }

        protected override void MoveUpdate()
        {
            var axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            controller.Move(new Vector3(axisInput.x, 0, axisInput.y) * moveSpeed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var dir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
                    Shoot(dir);
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
    }
}