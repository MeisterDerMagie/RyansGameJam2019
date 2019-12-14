using System;

namespace Platformer
{
    [Serializable]
    public class DoubleFloat
    {
        public float min;
        public float max;

        //Constructor for initialization in other scripts
        public DoubleFloat(float minValue, float maxValue)
        {
            min = minValue;
            max = maxValue;
        }

        public float Random() //Random value of variables
        {
            float result = UnityEngine.Random.Range(min, max);
            return result;
        }
    }
}