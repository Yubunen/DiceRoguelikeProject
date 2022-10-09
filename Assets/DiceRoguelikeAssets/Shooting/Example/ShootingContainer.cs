using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSemiRoguelike.Shooting
{
    [RequireComponent(typeof(Collider))]
    public class ShootingContainer : BaseContainer
    {
        protected void Awake()
        {
            Init();
        }
    }
}