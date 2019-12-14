
using UnityEngine;

namespace Platformer
{
    //Interfaces require the class to contain the methods necessary for any of your implementations.
    public interface IController
    {
        Rigidbody2D rigid2D { get; set; }

        bool isAttacking { get; set; }

        void Move();
        void Rotation();
        void Attack();
        void Animation();
        void Death();

    }
}