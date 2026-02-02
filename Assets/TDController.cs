using UnityEngine;
using UnityEngine.Events;
using SpaceShooter;


namespace TowerDefense
{
    public class TDController : AIController
    {
        private Path m_path;
        private int pathIndex;
        [SerializeField] private UnityEvent OnEndPath;

        protected override void Update()
        {
            base.Update(); // это вызовет Update из AIController
            Debug.Log(gameObject.name + " UPDATE");
        }

        public void SetPath(Path newPath)
        {
            m_path = newPath;
            pathIndex = 0;
            SetPatrolBehaviour(m_path[pathIndex]);
        }
        protected override void GetNewPoint()
        {
            pathIndex += 1;
            if(m_path.Length > pathIndex)
            {
                SetPatrolBehaviour(m_path[pathIndex]);
            }
            else
            {
                OnEndPath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
