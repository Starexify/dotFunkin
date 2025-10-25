using System.Collections.Generic;
using System.Globalization;
using dotFunkin.Core.Localization;
using dotFunkin.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace dotFunkin.Core;

public class Main() : Core("Friday Night Funkin'.NET", 1280, 720, false)
{
    protected override void Initialize()
    {
        // Load supported languages and set the default language.
        var cultures = LocalizationManager.GetSupportedCultures();
        var languages = new List<CultureInfo>();
        foreach (var t in cultures) languages.Add(t);

        // TODO You should load this from a settings file or similar,
        // based on what the user or operating system selected.
        var selectedLanguage = LocalizationManager.DEFAULT_CULTURE_CODE;
        LocalizationManager.SetCulture(selectedLanguage);

        base.Initialize();

        ChangeState(new TitleState());
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

        base.Update(gameTime);
    }
}