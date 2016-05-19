#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace _300hoursMARK2
{
    class GameInterface
    {
        public int HealthTotalValue;
        public int HealthValue;
        private Texture2D sprite;
        private static int _healthWidth = 19;
        private static Rectangle _healthFullRect = new Rectangle(64, 0, _healthWidth, 19);
        private static Rectangle _healthBrokeRect = new Rectangle(83, 0, _healthWidth, 19);
        private static Rectangle _healthEmptyRect = new Rectangle(102, 0, _healthWidth, 19);
        private static int _healthPaddingTop = 4;
        private static int _healthPaddingLeft = 4;
        public GameInterface(ContentManager cm)
        {
            HealthTotalValue = 0;
            HealthValue = 0;
            sprite = cm.Load<Texture2D>("sprite/interfaceSprite");
        }
        public void Update(int pHealth, int pMaxHealth)
        {
            HealthValue = pHealth;
            HealthTotalValue = pMaxHealth;
        }
        public void Draw(SpriteBatch sb)
        {
            int corHealthValue;
            if (HealthValue % 2 == 1)
            {
                corHealthValue = HealthValue - 1;
            }
            else
            {
                corHealthValue = HealthValue;
            }
            for (int i = corHealthValue; i < HealthTotalValue; i += 2)
            {
                sb.Draw(sprite, new Vector2(_healthPaddingLeft + (_healthWidth + _healthPaddingLeft) * i / 2, _healthPaddingTop), _healthEmptyRect, Color.White);
            }
            if(HealthValue % 2 == 1)
                sb.Draw(sprite, new Vector2(_healthPaddingLeft + (_healthWidth + _healthPaddingLeft) * corHealthValue / 2, _healthPaddingTop), _healthBrokeRect, Color.White);
            for (int i = 0; i < corHealthValue; i += 2)
            {
                sb.Draw(sprite, new Vector2(_healthPaddingLeft + (_healthWidth + _healthPaddingLeft) * i / 2, _healthPaddingTop), _healthFullRect, Color.White);
            }
            //sb.Draw(sprite, new Vector2(_healthWidth + _healthPaddingLeft, _healthPaddingTop), _healthRect, Color.White);
        }
    }
}
