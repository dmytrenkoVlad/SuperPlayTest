using Assets.Scripts.Ball;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public int Attempts { get; private set; }

        private BallController _ball;
        private float _minBottomPos;

        public event Action LostAttempt = delegate () { };

        public void Init(BallController ball, Vector2 bottomAreaPos)
        {
            Attempts = Constants.Attempts;
            _ball = ball;
            _minBottomPos = bottomAreaPos.y - 2;
        }

        public void ResetAttempts()
        {
            Attempts = Constants.Attempts;
        }

        private void BallOutOfBounds()
        {
            if (Attempts > 0)
            {
                Attempts--;
                LostAttempt();
            }
        }

        private void Update()
        {
            if (_ball != null && _ball.transform.position.y < _minBottomPos)
                BallOutOfBounds();
        }
    }
}
