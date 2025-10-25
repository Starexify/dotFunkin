using dotFunkin.Core.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dotFunkin.Core.UI;

public class TitleState : State
{
    public Texture2D NgLogo;

    public override void LoadContent()
    {
        NgLogo = Content.Load<Texture2D>("assets/images/newgrounds_logo");
    }

    public override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Enter)) Core.ChangeState(new MainMenuState());

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.Black);
        Core.SpriteBatch.Begin(SpriteSortMode.FrontToBack);

        Core.SpriteBatch.Draw(
            NgLogo,
            new Vector2(0, 0),
            null,
            Color.White,
            MathHelper.ToRadians(0),
            new Vector2(0, 0),
            new Vector2(1f, 1f),
            SpriteEffects.None,
            0.002f
        );

        Core.SpriteBatch.End();

        base.Draw(gameTime);
    }
}