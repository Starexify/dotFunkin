using System;
using dotFunkin.Core.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dotFunkin.Core.UI;

public class TitleState : State
{
    public Texture2D NgSprite;

    public override void LoadContent()
    {
        NgSprite = Content.Load<Texture2D>("assets/images/newgrounds_logo_animated");
    }

    private float progress;
    private Rectangle[] frames = [new(0, 0, 600, 600), new(600, 0, 600, 600)];

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter)) Core.ChangeState(new MainMenuState());

        //if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Core.Instance.Exit();

        progress += (float)gameTime.ElapsedGameTime.TotalSeconds;
        progress %= 1f;

        base.Update(gameTime);
    }


    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.Black);
        Core.SpriteBatch.Begin(SpriteSortMode.FrontToBack);
    
        var frameIndex = (int)(progress * frames.Length);
        var frame = frames[frameIndex];
        
        Core.SpriteBatch.Draw(
            NgSprite,
            new Vector2(Core.Instance.Window.ClientBounds.Width * 0.5f,
                Core.Instance.Window.ClientBounds.Height * 0.52f),
            frame,
            Color.White,
            0,
            new Vector2(frame.Width, frame.Height) * 0.5f,
            0.8F,
            SpriteEffects.None,
            0
        );

        Core.SpriteBatch.End();

        base.Draw(gameTime);
    }
}