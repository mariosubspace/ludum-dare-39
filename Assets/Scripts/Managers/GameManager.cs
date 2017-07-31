using UnityEngine;

namespace Mario
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public float powerLevel;

        [HideInInspector]
        public int currentNumberOfPowerOrbs;

        [Tooltip("Maximum number of batteries allowed in houses at any time.")]
        public int maxBatteriesInScene = 5;
    }
}
