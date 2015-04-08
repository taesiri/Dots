using UnityEngine;

namespace Assets.Scripts
{
    public class GameLogic : MonoBehaviour
    {
        public static GameLogic Instance;

        public void Awake()
        {
            Instance = this;
        

        }

        public void Start()
        {
            InitializeGrid(transform);
        }

        public void InitializeGrid(Transform parentTransform)
        {
            
        }

    

      
    }

}