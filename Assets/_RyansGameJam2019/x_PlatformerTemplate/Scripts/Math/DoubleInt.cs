using System;

namespace Platformer
{
    [Serializable]
    public class DoubleInt
    {
        public int min;
        public int max;

        //Constructor for initialization in other scripts
        public DoubleInt(int minValue, int maxValue)
        {
            min = minValue;
            max = maxValue;
        }

        public int Random() //Random value of variables
        {
            int result = UnityEngine.Random.Range(min, max);
            return result;
        }
    }
}