using System;

namespace Game1
{
    class AABB
    {
        public static bool IsColliding(Body main_body, Body other_body, bool side_by_side = true)
        {
            var p1x = Math.Max(main_body.x, other_body.x);
            var p1y = Math.Max(main_body.y, other_body.y);
            var p2x = Math.Min(main_body.x + main_body.width, other_body.x + other_body.width);
            var p2y = Math.Min(main_body.y + main_body.height, other_body.y + other_body.height);

            if (side_by_side && p2x - p1x >= 0 && p2y - p1y >= 0)
            {
                return true;
            }
            else if (!side_by_side && (p2x - p1x) > 0 && (p2y - p1y) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Direction CollisionDirection(Body main_body, Body other_body)
        {
            var left = (main_body.x + main_body.width) - other_body.x;
            var right = (other_body.x + other_body.width) - main_body.x;
            var bottom = (main_body.y + main_body.height) - other_body.y;
            var top = (other_body.y + other_body.height) - main_body.y;

            if (right < left && right < top && right < bottom)
            {
                return Direction.Right;
            }
            else if (left < top && left < bottom)
            {
                return Direction.Left;
            }
            else if (top < bottom)
            {
                return Direction.Top;
            }
            else
            {
                return Direction.Bottom;
            }
        }
    }
}
