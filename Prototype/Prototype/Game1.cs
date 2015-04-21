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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        VertexPositionColor[] plane = new VertexPositionColor[4];
        BasicEffect effect;
        Matrix view, projection;
        Camera camera;
        Player player;
        List<Coin> coins = new List<Coin>();
        bool coinAlreadyExists = false;
        Enemy[] enemies = new Enemy[4];
        Random rand = new Random();
        int enemyDirection;
        int gameState = 0;
        int score;
        float timeLeft;
        float counter = 0;
        float enemycounter = 0;
        SpriteFont spriteFont;
        SpriteFont spriteFont2;

        float timeSinceLastUpdate;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            plane[0] = new VertexPositionColor(new Vector3(-11.0f, -0.5f, -11.0f), Color.Green);
            plane[1] = new VertexPositionColor(new Vector3(11.0f, -0.5f, -11.0f), Color.GreenYellow);
            plane[2] = new VertexPositionColor(new Vector3(-11.0f, -0.5f, 11.0f), Color.LightGreen);
            plane[3] = new VertexPositionColor(new Vector3(11.0f, -0.5f, 11.0f), Color.LawnGreen);
            player = new Player(new Vector3(0, 0, 0), new Vector2(0, 1));
            camera = new Camera(new Vector3(0, 25, 15), Vector3.Zero, Vector3.Up);
            enemies[0] = new Enemy(new Vector3(3,0,3));
            enemies[1] = new Enemy(new Vector3(-3,0,3));
            enemies[2] = new Enemy(new Vector3(3, 0, -3));
            enemies[3] = new Enemy(new Vector3(-3, 0, -3));
            timeLeft = 30000;
            base.Initialize();
        }

        /*// Set the 3D model to draw.
        Model myModel;

        // The aspect ratio determines how to scale 3d to 2d projection.
        float aspectRatio;*/

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            effect = new BasicEffect(GraphicsDevice);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.5f, 1000.0f);
            spriteFont = Content.Load<SpriteFont>("basic");
            spriteFont2 = Content.Load<SpriteFont>("big");

            /*myModel = Content.Load<Model>("Models\\EAGuy");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;*/
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            switch(gameState)
            {
                case 0:
                    timeSinceLastUpdate = gameTime.ElapsedGameTime.Milliseconds;
                    counter += timeSinceLastUpdate;
                    enemycounter += timeSinceLastUpdate;
                    timeLeft -= timeSinceLastUpdate;

                    view = Matrix.CreateLookAt(camera.getCameraPosition(), camera.getCameraTarget(), camera.getUpVector());

                    //generating coins
                    if (counter > 2000)
                    {
                        int coinposition1 = rand.Next(-5, 6);
                        int coinposition2 = rand.Next(-5, 6);
                        for (int i = 0; i < coins.Count; ++i)
                        {
                            if (coins[i].getPosition() == new Vector3(coinposition1, 0, coinposition2) || player.getPosition() == new Vector3(coinposition1, 0, coinposition2))
                            {
                                coinAlreadyExists = true;
                                break;
                            }
                        }
                        if (coinAlreadyExists == false)
                        {
                            coins.Add(new Coin(new Vector3(coinposition1, 0, coinposition2)));
                            counter = 0;
                        }
                        coinAlreadyExists = false;
                    }

                    //Losescreen
                    for (int i = 0; i < enemies.Length; ++i)
                    {
                        if (enemies[i].getPosition() == player.getPosition())
                        {
                            gameState = 1;
                        }
                    }

                    //collecting coins
                    for (int i = 0; i < coins.Count; ++i)
                    {
                        if (coins[i].getPosition() == player.getPosition())
                        {
                            coins.RemoveAt(i);
                            ++score;
                        }
                    }
                    player.Update(gameTime, timeSinceLastUpdate, camera);

                    //moving of the enemies
                    if (enemycounter > 1000)
                    {
                        for (int i = 0; i < enemies.Length; ++i)
                        {
                            enemyDirection = rand.Next(0, 4);
                            switch (enemyDirection)
                            {
                                case 0:
                                    if (enemies[i].getPosition().X < 5)
                                    { enemies[i] = new Enemy(enemies[i].getPosition() + new Vector3(1, 0, 0)); enemycounter = 0; }
                                    break;
                                case 1:
                                    if (enemies[i].getPosition().X > -5)
                                    { enemies[i] = new Enemy(enemies[i].getPosition() + new Vector3(-1, 0, 0)); enemycounter = 0; }
                                    break;
                                case 2:
                                    if (enemies[i].getPosition().Z > -5)
                                    { enemies[i] = new Enemy(enemies[i].getPosition() + new Vector3(0, 0, 1)); enemycounter = 0; }
                                    break;
                                case 3:
                                    if (enemies[i].getPosition().Z < 5)
                                    { enemies[i] = new Enemy(enemies[i].getPosition() + new Vector3(0, 0, -1)); enemycounter = 0; }
                                    break;
                            }
                        }
                    }

                    //Winscreen
                    if (timeLeft < 0)
                    {
                        gameState = 2;
                    }
                    break;
            }
            /*modelRotation += (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(0.1f);*/
            base.Update(gameTime);
        }

        /*// Set the position of the model in world space, and set the rotation.
        Vector3 modelPosition = Vector3.Zero;
        float modelRotation = 0.0f;

        // Set the position of the camera in world space, for our view matrix.
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);*/
        
        protected override void Draw(GameTime gameTime)
        {
            switch (gameState)
            {
                case 0:
                    graphics.GraphicsDevice.Clear(Color.Black);
                    effect.View = view;
                    effect.Projection = projection;
                    //effect.World = Matrix.CreateScale(2) * Matrix.CreateRotationY(MathHelper.PiOver4) * Matrix.CreateTranslation(2, 0, 0);
                    effect.VertexColorEnabled = true;

                    effect.CurrentTechnique.Passes[0].Apply();
                    GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, plane, 0, 2);
                    player.Draw(gameTime, GraphicsDevice, effect);
                    enemies[0].Draw(gameTime, GraphicsDevice, effect);
                    enemies[1].Draw(gameTime, GraphicsDevice, effect);
                    enemies[2].Draw(gameTime, GraphicsDevice, effect);
                    enemies[3].Draw(gameTime, GraphicsDevice, effect);
                    for (int i = 0; i < coins.Count; ++i)
                    {
                        coins[i].Draw(gameTime, GraphicsDevice, effect);
                    }
                    spriteBatch.Begin();
                    spriteBatch.DrawString(spriteFont, "Remaining Time: " + timeLeft.ToString(), new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(spriteFont, "Score: " + score.ToString(), new Vector2(300, 10), Color.White);
                    spriteBatch.End();
                    break;
                case 1:
                    graphics.GraphicsDevice.Clear(Color.Red);
                    spriteBatch.Begin();
                    spriteBatch.DrawString(spriteFont2, "GAME OVER", new Vector2(300, 200), Color.Black);
                    spriteBatch.End();
                    break;
                case 2:
                    graphics.GraphicsDevice.Clear(Color.Yellow);
                    spriteBatch.Begin();
                    spriteBatch.DrawString(spriteFont2, "Score: " + score.ToString(), new Vector2(300, 200), Color.Black);
                    spriteBatch.End();
                    break;
            }
            

            /*// Copy any parent transforms.
            Matrix[] transforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in myModel.Meshes)
            {
                // This is where the mesh orientation is set, as well 
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateRotationY(modelRotation)
                        * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition,
                        Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45.0f), aspectRatio,
                        1.0f, 10000.0f);
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }*/
            base.Draw(gameTime);
        }
    }
}
