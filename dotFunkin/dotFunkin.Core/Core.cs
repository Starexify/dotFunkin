using System;
using dotFunkin.Core.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace dotFunkin.Core;

/// <summary>
///     The main class for the game, responsible for managing game components, settings,
///     and platform-specific configurations.
/// </summary>
public class Core : Game
{
    public static Core Instance { get; private set; }
    
    private static State _sActiveState;
    private static State _sNextState;
    
    /// <summary>
    ///     Indicates if the game is running on a mobile platform.
    /// </summary>
    public static readonly bool IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();

    /// <summary>
    ///     Indicates if the game is running on a desktop platform.
    /// </summary>
    public static readonly bool IsDesktop = OperatingSystem.IsMacOS() || OperatingSystem.IsLinux() || OperatingSystem.IsWindows();

    public static GraphicsDeviceManager Graphics { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }
    public static new ContentManager Content { get; private set; }

    /// <summary>
    ///     Initializes a new instance of the game. Configures platform-specific settings,
    ///     initializes services like settings and leaderboard managers, and sets up the
    ///     screen manager for screen transitions.
    /// </summary>
    public Core(string title, int width, int height, bool fullScreen)
    {
        if (Instance != null) throw new InvalidOperationException($"Only a single Main instance can be created");
        Instance = this;
        
        Graphics = new GraphicsDeviceManager(this);
        
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;
        
        Graphics.ApplyChanges();
        
        Window.Title = title;
        Window.AllowUserResizing = true;
        
        Content = base.Content;
        Content.RootDirectory = "Content";

        // Share GraphicsDeviceManager as a service.
        Services.AddService(Graphics);

        IsMouseVisible = true;

        // Configure screen orientations.
        Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
    }

    /// <summary>
    ///     Initializes the game, including setting up localization and adding the
    ///     initial screens to the ScreenManager.
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
        
        GraphicsDevice = base.GraphicsDevice;

        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (_sNextState != null)
        {
            TransitionState();
        }

        if (_sActiveState != null)
        {
            _sActiveState.Update(gameTime);
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_sActiveState != null)
        {
            _sActiveState.Draw(gameTime);
        }
        
        base.Draw(gameTime);
    }
    
    public static void ChangeState(State next)
    {
        if (_sActiveState != next)
        {
            _sNextState = next;
        }
    }
    
    private static void TransitionState()
    {
        // If there is an active state, dispose of it.
        if (_sActiveState != null)
        {
            _sActiveState.Dispose();
        }

        // Force the garbage collector to collect to ensure memory is cleared.
        GC.Collect();

        // Change the currently active state to the new state.
        _sActiveState = _sNextState;

        // Null out the next state value so it does not trigger a change over and over.
        _sNextState = null;

        // If the active state now is not null, initialize it.
        // Remember, just like with Game, the Initialize call also calls the
        // State.LoadContent
        if (_sActiveState != null)
        {
            _sActiveState.Initialize();
        }
    }
}