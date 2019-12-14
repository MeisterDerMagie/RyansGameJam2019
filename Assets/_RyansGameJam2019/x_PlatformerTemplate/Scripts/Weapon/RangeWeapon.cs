using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class RangeWeapon : MonoBehaviour
    {
        [Header("Variables")]
        public float moveSpeed;
        public float damage;
        public bool fromPlayer;

        private void Start()
        {
            Destroy(gameObject, 1); //Destroyed through Destroy(gameObject, TIME TO DESTROY)
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Player": //if player
                    if (!fromPlayer)
                    {
                        Damage(collision); 
                    }
                    break;
                case "Enemy":
                    if (fromPlayer)
                    {
                        Damage(collision);
                        CameraManager.Instance.cameraShake.Shake(); //Camera shake effect
                    }
                    break;
            }
        }

        void Damage(Collider2D collision)
        {
            Stats collInfo = collision.GetComponent<Stats>();
            collInfo.GetDamage(damage);

            //Hit visual effect
            HitEffect hitEffect = collision.GetComponentInChildren<HitEffect>();
            hitEffect.PlayEffect();

            Destroy(gameObject);
        }

        public void Move()
        {
            //Simple move, you can use Physics(rigidbody2d) but i and rigidbody2d are not best friends :)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }

    }
}