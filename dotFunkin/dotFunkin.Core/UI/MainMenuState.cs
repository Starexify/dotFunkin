using dotFunkin.Core.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dotFunkin.Core.UI;

public class MainMenuState : State
{
    public Texture2D NgLogoOld;

    public override void LoadContent()
    {
        NgLogoOld = Content.Load<Texture2D>("assets/images/newgrounds_logo_classic");
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Core.ChangeState(new TitleState());

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.Black);
        Core.SpriteBatch.Begin(SpriteSortMode.FrontToBack);

        Core.SpriteBatch.Draw(
            NgLogoOld,
            new Vector2(100, 100),
            null,
            Color.White,
            MathHelper.ToRadians(0),
            new Vector2(NgLogoOld.Width, NgLogoOld.Height) * 0.5f,
            new Vector2(1, 1),
            SpriteEffects.None,
            0.001f
        );

        Core.SpriteBatch.End();

        base.Draw(gameTime);
    }
}