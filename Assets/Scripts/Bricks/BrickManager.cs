using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class BrickManager : MonoBehaviour
    {
        public event Action BrickDestroyed = delegate() { };
        public event Action<BrickType> BrickDamaged = delegate(BrickType brickType) { };

        private List<GameObject> _bricks = new List<GameObject>();

        public void AddBrick(GameObject brick)
        {
            _bricks.Add(brick);
            BrickController brickController = brick.GetComponent<BrickController>();
            brickController.BrickDestroyed += OnBrickDestroyed;
            brickController.BrickDamaged += OnBrickDamaged;
        }

        private void OnBrickDestroyed(BrickController brickController)
        {
            brickController.BrickDestroyed -= OnBrickDestroyed;
            brickController.BrickDamaged -= OnBrickDamaged;
            brickController.gameObject.SetActive(false);
            BrickDestroyed();
        }

        private void OnBrickDamaged(int health, BrickType brickType)
        {
            BrickDamaged(brickType);
        }

        public void ResetBricks()
        {
            foreach (var brick in _bricks)
            {
                if (!brick.activeSelf)
                {
                    brick.SetActive(true);
                    BrickController controller = brick.GetComponent<BrickController>();
                    controller.BrickDestroyed += OnBrickDestroyed;
                    controller.BrickDamaged += OnBrickDamaged;
                }
                brick.GetComponent<HealthComponent>().ResetHealth();
                if (brick.TryGetComponent(out ColorComponent color))
                    color.ResetColor();
            }
        }

        public int NonDestroyedBricksCount()
        {
            int counter = 0;
            foreach (var brick in _bricks)
            {
                if (brick.activeSelf)
                    counter++;
            }

            return counter;
        }
    }
}
