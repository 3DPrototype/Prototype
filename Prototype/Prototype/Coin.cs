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
    class Coin
    {
        Vector3 position;
        Cube coincube;

        public Coin(Vector3 position)
        {
            this.position = position;
            coincube = new Cube(position, 0.5f, 0.5f, 0.5f, Color.Yellow);
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, BasicEffect effect)
        {
            effect.World = Matrix.CreateTranslation(position);
            effect.CurrentTechnique.Passes[0].Apply();
            coincube.Draw(gameTime, graphics);
            effect.World = Matrix.Identity;
            effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
