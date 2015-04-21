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
    class Cube
    {
        VertexPositionColor[] vertices = new VertexPositionColor[8];
        VertexPositionColor[] front = new VertexPositionColor[4];
        VertexPositionColor[] back = new VertexPositionColor[4];
        VertexPositionColor[] top = new VertexPositionColor[4];
        VertexPositionColor[] bottom = new VertexPositionColor[4];
        VertexPositionColor[] left = new VertexPositionColor[4];
        VertexPositionColor[] right = new VertexPositionColor[4];

        public Cube(Vector3 position, float width, float height, float depth, Color color)
        {
            vertices[0] = new VertexPositionColor(position + new Vector3(0.5f * -width, 0.5f * height, 0.5f * depth), color);
            vertices[1] = new VertexPositionColor(position + new Vector3(0.5f * width, 0.5f * height, 0.5f * depth), color);
            vertices[2] = new VertexPositionColor(position + new Vector3(0.5f * width, 0.5f * -height, 0.5f * depth), color);
            vertices[3] = new VertexPositionColor(position + new Vector3(0.5f * -width, 0.5f * -height, 0.5f * depth), color);
            vertices[4] = new VertexPositionColor(position + new Vector3(0.5f * -width, 0.5f * height, 0.5f * -depth), color);
            vertices[5] = new VertexPositionColor(position + new Vector3(0.5f * width, 0.5f * height, 0.5f * -depth), color);
            vertices[6] = new VertexPositionColor(position + new Vector3(0.5f * width, 0.5f * -height, 0.5f * -depth), color);
            vertices[7] = new VertexPositionColor(position + new Vector3(0.5f * -width, 0.5f * -height, 0.5f * -depth), color);

            front[0] = vertices[0];
            front[1] = vertices[1];
            front[2] = vertices[3];
            front[3] = vertices[2];

            back[0] = vertices[5];
            back[1] = vertices[4];
            back[2] = vertices[6];
            back[3] = vertices[7];

            top[0] = vertices[4];
            top[1] = vertices[5];
            top[2] = vertices[0];
            top[3] = vertices[1];

            bottom[0] = vertices[3];
            bottom[1] = vertices[2];
            bottom[2] = vertices[7];
            bottom[3] = vertices[6];

            left[0] = vertices[4];
            left[1] = vertices[0];
            left[2] = vertices[7];
            left[3] = vertices[3];

            right[0] = vertices[1];
            right[1] = vertices[5];
            right[2] = vertices[2];
            right[3] = vertices[6];
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics)
        {
            graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, front, 0, 2);
            graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, back, 0, 2);
            graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, top, 0, 2);
            graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, bottom, 0, 2);
            graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, left, 0, 2);
            graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, right, 0, 2);
        }
    }
}
