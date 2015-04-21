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
    class Player
    {
        Vector3 position;
        Vector2 direction;
        KeyboardState keyboard = Keyboard.GetState();
        Cube playercube;
        float counter;
        float rotateCounter;

        public Player(Vector3 position, Vector2 direction)
        {
            this.position = position;
            this.direction = direction;
            playercube = new Cube(position, 1.0f, 1.0f, 1.0f, Color.Blue);
        }

        public void Update(GameTime gameTime, float timeSinceLastUpdate, Camera camera)
        {
            counter += timeSinceLastUpdate;
            rotateCounter += timeSinceLastUpdate;
            keyboard = Keyboard.GetState();
            if (counter > 1000)
            {
                if (keyboard.IsKeyDown(Keys.W) && direction.X == 1 && position.X < 5) { position.X++; counter = 0; }
                else if (keyboard.IsKeyDown(Keys.W) && direction.X == -1 && position.X > -5) { position.X--; counter = 0; }
                else if (keyboard.IsKeyDown(Keys.W) && direction.Y == 1 && position.Z > -5) { position.Z--; counter = 0; }
                else if (keyboard.IsKeyDown(Keys.W) && direction.Y == -1 && position.Z < 5) { position.Z++; counter = 0; }

                else if (keyboard.IsKeyDown(Keys.S) && direction.X == 1 && position.X > -5) { position.X--; counter = 0; }
                else if (keyboard.IsKeyDown(Keys.S) && direction.X == -1 && position.X < 5) { position.X++; counter = 0; }
                else if (keyboard.IsKeyDown(Keys.S) && direction.Y == 1 && position.Z < 5) { position.Z++; counter = 0; }
                else if (keyboard.IsKeyDown(Keys.S) && direction.Y == -1 && position.Z > -5) { position.Z--; counter = 0; }
            }
            if (rotateCounter > 1000)
            {
                if (keyboard.IsKeyDown(Keys.A) && direction.X == 1)
                {
                    camera.setCameraPosition(Vector3.Transform(camera.getCameraPosition(), Matrix.CreateRotationY(MathHelper.PiOver2)));
                    direction.X = 0; direction.Y = 1; rotateCounter = 0;
                }
                else if (keyboard.IsKeyDown(Keys.A) && direction.X == -1)
                {
                    camera.setCameraPosition(Vector3.Transform(camera.getCameraPosition(), Matrix.CreateRotationY(MathHelper.PiOver2)));
                    direction.X = 0; direction.Y = -1; rotateCounter = 0;
                }
                else if (keyboard.IsKeyDown(Keys.A) && direction.Y == 1)
                {
                    camera.setCameraPosition(Vector3.Transform(camera.getCameraPosition(), Matrix.CreateRotationY(MathHelper.PiOver2)));
                    direction.X = -1; direction.Y = 0; rotateCounter = 0;
                }
                else if (keyboard.IsKeyDown(Keys.A) && direction.Y == -1)
                {
                    camera.setCameraPosition(Vector3.Transform(camera.getCameraPosition(), Matrix.CreateRotationY(MathHelper.PiOver2)));
                    direction.X = 1; direction.Y = 0; rotateCounter = 0;
                }

                else if (keyboard.IsKeyDown(Keys.D) && direction.X == 1)
                {
                    camera.setCameraPosition(Vector3.Transform(camera.getCameraPosition(), Matrix.CreateRotationY(-MathHelper.PiOver2)));
                    direction.X = 0; direction.Y = -1; rotateCounter = 0;
                }
                else if (keyboard.IsKeyDown(Keys.D) && direction.X == -1)
                {
                    camera.setCameraPosition(Vector3.Transform(camera.getCameraPosition(), Matrix.CreateRotationY(-MathHelper.PiOver2)));
                    direction.X = 0; direction.Y = 1; rotateCounter = 0;
                }
                else if (keyboard.IsKeyDown(Keys.D) && direction.Y == 1)
                {
                    camera.setCameraPosition(Vector3.Transform(camera.getCameraPosition(), Matrix.CreateRotationY(-MathHelper.PiOver2)));
                    direction.X = 1; direction.Y = 0; rotateCounter = 0;
                }
                else if (keyboard.IsKeyDown(Keys.D) && direction.Y == -1)
                {
                    camera.setCameraPosition(Vector3.Transform(camera.getCameraPosition(), Matrix.CreateRotationY(-MathHelper.PiOver2)));
                    direction.X = -1; direction.Y = 0; rotateCounter = 0;
                }
            }
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, BasicEffect effect)
        {
            effect.World = Matrix.CreateTranslation(position*2);
            effect.CurrentTechnique.Passes[0].Apply();
            playercube.Draw(gameTime, graphics);
            effect.World = Matrix.Identity;
            effect.CurrentTechnique.Passes[0].Apply();
        }

        public Vector3 getPosition()
        {
            return position;
        }
    }
}
