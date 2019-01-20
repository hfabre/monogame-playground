using Microsoft.Xna.Framework;

namespace Game1
{
    class Body
    {
        public float x;
        public float y;
        public float width;
        public float height;

        public float speedX = 0;
        public float speedY = 0;

        public const float moveSpeed = 80;
        public const float friction = .80f;
        public const float gravity = 40;
        public const float jumpSpeed = 550;

        public Body(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void Update(float deltaTime)
        {
            this.ApplySpeed(deltaTime);
        }

        public void CalculateSpeed()
        {
            this.UpdateSpeedFromNaturalForces();
        }

        public Vector2 FuturPosition(float deltaTime)
        {
            return new Vector2(this.x + this.speedX * deltaTime, this.y + this.speedY * deltaTime);
        }

        public void ResetSpeedX()
        {
            this.speedX = 0;
        }

        public void ResetSpeedY()
        {
            this.speedY = 0;
        }

        public void SetY(float value)
        {
            this.y = value - this.height;
        }

        public void MoveLeft()
        {
            this.speedX = this.speedX - moveSpeed;
        }

        public void MoveRight()
        {
            this.speedX = this.speedX + moveSpeed;
        }

        public void Jump()
        {
            this.speedY = this.speedY - jumpSpeed;
        }

        private void ApplySpeed(float deltaTime)
        {
            Vector2 futurPosition = FuturPosition(deltaTime);

            this.x = futurPosition.X;
            this.y = futurPosition.Y;
        }

        private void UpdateSpeedFromNaturalForces()
        {
            this.speedX = this.speedX * friction;
            this.speedY = this.speedY + gravity;
        }
    }
}
