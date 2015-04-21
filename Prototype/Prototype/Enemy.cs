using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Prototype
{
    class Enemy
    {
        Vector3 position;
        Cube enemycube;
        float counter;

        public Enemy(Vector3 position)
        {
            this.position = position;
            enemycube = new Cube(position, 1.0f, 1.0f, 1.0f, Color.Red);
        }
        public void Update(GameTime gameTime, float timeSinceLastUpdate)
        {
            counter += timeSinceLastUpdate;

        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, BasicEffect effect)
        {
            effect.World = Matrix.CreateTranslation(position);
            effect.CurrentTechnique.Passes[0].Apply();
            enemycube.Draw(gameTime, graphics);
            effect.World = Matrix.Identity;
            effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
