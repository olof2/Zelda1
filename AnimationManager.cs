using Microsoft.Xna.Framework;

namespace Zelda1
{
    public class AnimationManager
    {
        enum PlayMode { Play, Pause }

        Rectangle[] sourceRect;
        PlayMode playMode = PlayMode.Play;
        float animTime = 0.0f;
        float speed;

        public AnimationManager(Rectangle[] sourceRect, float speed)
        {
            this.sourceRect = sourceRect;
            this.speed = speed;

        }

        public void setPlay() { playMode = PlayMode.Play; }
        public void setPause() { playMode = PlayMode.Pause; }
        public void setSpeed(float speed) { this.speed = speed; }

        public void Update(float dT)
        {
            if (playMode == PlayMode.Pause) return;
            animTime += dT * speed;
        }

        public Rectangle GetCurrentRect()
        {
            int rectIndex = (int)animTime % sourceRect.Length;
            return sourceRect[rectIndex];
        }


        public static Rectangle[] enemyWalkRects = new Rectangle[]
        {
            new Rectangle(0,0,40,40),
            new Rectangle(40,0,40,40),
            new Rectangle(80,0,40,40),
            new Rectangle(120,0,40,40),
        };


        public static Rectangle[] playerWalkLeft = new Rectangle[]
        {
            new Rectangle(0,0,40,40),
            new Rectangle(40,0,40,40),
            new Rectangle(80,0,40,40),
            new Rectangle(120,0,40,40),
            new Rectangle(160,0,40,40),
            new Rectangle(200,0,40,40),
            new Rectangle(240,0,40,40),
            new Rectangle(280,0,40,40),
        };

        public static Rectangle[] playerWalkRight = new Rectangle[]
        {
            new Rectangle(0,200,40,40),
            new Rectangle(40,200,40,40),
            new Rectangle(80,200,40,40),
            new Rectangle(120,200,40,40),
            new Rectangle(160,200,40,40),
            new Rectangle(200,200,40,40),
            new Rectangle(240,200,40,40),
            new Rectangle(280,200,40,40),
        };

        public static Rectangle[] playerWalkDown = new Rectangle[]
        {
            new Rectangle(0,40,40,40),
            new Rectangle(40,40,40,40),
            new Rectangle(80,40,40,40),
            new Rectangle(120,40,40,40),
            new Rectangle(160,40,40,40),
            new Rectangle(200,40,40,40),
            new Rectangle(240,40,40,40),
            new Rectangle(280,40,40,40),
        };

        public static Rectangle[] playerWalkUp = new Rectangle[]
        {
            new Rectangle(0,80,40,40),
            new Rectangle(40,80,40,40),
            new Rectangle(80,80,40,40),
            new Rectangle(120,80,40,40),
            new Rectangle(160,80,40,40),
            new Rectangle(200,80,40,40),
            new Rectangle(240,80,40,40),
            new Rectangle(280,80,40,40),
        };

        public static Rectangle[] attackLeftAnim = new Rectangle[]
        {
            new Rectangle(0,120,40,40),
            new Rectangle(40,120,40,40),
            new Rectangle(80,120,40,40),
            new Rectangle(120,120,40,40),
            new Rectangle(160,120,40,40),
            new Rectangle(200,120,40,40),
            new Rectangle(240,120,40,40),
            new Rectangle(280,120,40,40),
        };
        public static Rectangle[] attackRightAnim = new Rectangle[]
        {
            new Rectangle(0,240,40,40),
            new Rectangle(40,240,40,40),
            new Rectangle(80,240,40,40),
            new Rectangle(120,240,40,40),
            new Rectangle(160,240,40,40),
            new Rectangle(200,240,40,40),
            new Rectangle(240,240,40,40),
            new Rectangle(280,240,40,40),
        };
        public static Rectangle[] attackDownAnim = new Rectangle[]
        {
            new Rectangle(0,280,40,40),
            new Rectangle(40,280,40,40),
            new Rectangle(80,280,40,40),
            new Rectangle(120,280,40,40),
            new Rectangle(160,280,40,40),
            new Rectangle(200,280,40,40),
            new Rectangle(240,280,40,40),
            new Rectangle(280,280,40,40),
        };
        public static Rectangle[] attackUpAnim = new Rectangle[]
        {
            new Rectangle(0,320,40,40),
            new Rectangle(40,320,40,40),
            new Rectangle(80,320,40,40),
            new Rectangle(120,320,40,40),
            new Rectangle(160,320,40,40),
            new Rectangle(200,320,40,40),
            new Rectangle(240,320,40,40),
            new Rectangle(280,320,40,40),
        };
        public static Rectangle[] deathAnim = new Rectangle[]
        {
            new Rectangle(0,160,40,40),
            new Rectangle(40,160,40,40),
            new Rectangle(80,160,40,40),
            new Rectangle(120,160,40,40),
            new Rectangle(160,160,40,40),
            new Rectangle(200,160,40,40),
        };


    }
}
