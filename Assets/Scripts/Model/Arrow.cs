﻿using UnityEngine;

namespace Geekbrains
{
    public sealed class Arrow : Ammunition
    {
        private void OnCollisionEnter(Collision collision)
        {
            // дописать доп урон
            var tempObj = collision.gameObject.GetComponent<ISetDamage>();

            if (tempObj != null)
            {
                tempObj.SetDamage(new InfoCollision(_curDamage, Rigidbody.velocity));
            }
        }
    }
}
