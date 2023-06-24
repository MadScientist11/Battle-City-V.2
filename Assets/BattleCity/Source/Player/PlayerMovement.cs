using System.Collections.Generic;
using UnityEngine;

namespace BattleCity.Source.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerConfiguration _playerConfig;

        public float collisionOffset = 0.05f;
        public ContactFilter2D movementFilter;
        private List<RaycastHit2D> castCollisions = new();
        private Rigidbody2D rigidbody2D;
        private float _currentAngle;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 moveInput =
                new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            bool success = MovePlayer(moveInput);

            if (!success)
            {
                success = MovePlayer(new Vector2(moveInput.x, 0));

                if (!success)
                {
                    success = MovePlayer(new Vector2(0, moveInput.y));
                }
            }

            float angle = GetRotationAngle(moveInput.normalized);

            if ((int)angle == (int)_currentAngle || moveInput.sqrMagnitude == 0)
                return;

            _currentAngle = angle;

            rigidbody2D.SetRotation(_currentAngle * 90);
        }

        private bool MovePlayer(Vector2 moveDirection)
        {
            int count = rigidbody2D.Cast(moveDirection,
                movementFilter,
                castCollisions,
                _playerConfig.MoveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                Vector2 moveVector = moveDirection * _playerConfig.MoveSpeed * Time.fixedDeltaTime;
                rigidbody2D.MovePosition(rigidbody2D.position + moveVector);

                return true;
            }

            return false;
        }

        private float GetRotationAngle(Vector2 dir2)
        {
            float rotationAngle = Vector2.SignedAngle(Vector2.up, dir2);
            int clampedAngle = Mathf.RoundToInt(rotationAngle / 90f);

            return clampedAngle;
        }

        private float ClampToNearestAngle(Vector2 direction)
        {
            direction = direction.normalized;
            float angle = (float)Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Mathf.Round(angle / 90) * 90;
        }
    }
}