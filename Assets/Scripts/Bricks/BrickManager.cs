using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class BrickManager
    {
        public event Action BrickDestroyed = delegate() { };

        private List<GameObject> _bricks;

        public BrickManager()
        {
            _bricks = new List<GameObject>();
        }

        public void AddBrick(GameObject brick)
        {
            _bricks.Add(brick);
            BrickController brickController = brick.GetComponent<BrickController>();
            brickController.BrickDestroyed += OnBrickDestroyed;
        }

        private void OnBrickDestroyed(BrickController brickController)
        {
            brickController.BrickDestroyed -= OnBrickDestroyed;
            brickController.gameObject.SetActive(false);
            BrickDestroyed();
        }

        public void ResetBricks()
        {
            foreach (var brick in _bricks)
            {
                if (!brick.activeSelf)
                {
                    brick.SetActive(true);
                    brick.GetComponent<BrickController>().BrickDestroyed += OnBrickDestroyed;
                }
                brick.GetComponent<HealthComponent>().ResetHealth();
            }
        }
    }
}
