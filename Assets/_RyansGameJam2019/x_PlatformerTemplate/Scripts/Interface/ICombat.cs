using UnityEngine;

namespace Platformer
{
    //Interfaces require the class to contain the methods necessary for any of your implementations.
    public interface ICombat
    {
        void HitDetected();
        void AttackForce(float forcePower);
        void RangeAttack();
        void MeleeAttack(Stats targetStats, float damage);
    }
}