using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace MonoGame.CExt.Input
{
    /// Modified from https://stackoverflow.com/questions/904454/how-to-slow-down-or-stop-key-presses-in-xna
    /// Author: RCIX
    /// Date: Jun 4, 2009
    /// 
    /// <summary>
    /// Input Helper Class
    /// </summary>
    public class InputHelper
    {
        private bool keyboardInitialized = false;
        private bool mouseInitialized = false;
        private bool gamepadInitialized = false;

        /// <summary>
        /// Gamepad states
        /// </summary>
        private GamePadState[] _lastGamepadState;
        private GamePadState[] _currentGamepadState;

        /// <summary>
        /// If on PC, then check the keyboard and mouse states as well
        /// </summary>
#if (!XBOX)
        private KeyboardState _lastKeyboardState;
        private KeyboardState _currentKeyboardState;
        private MouseState _lastMouseState;
        private MouseState _currentMouseState;
#endif
        
        private bool refreshData = false;

        /// <summary>
        /// Fetches the latest input states.
        /// </summary>
        public void Update()
        {
            if (!refreshData)
                refreshData = true;
            if (!gamepadInitialized)
            {
                gamepadInitialized = true;
                _lastGamepadState = new GamePadState[GamePad.MaximumGamePadCount];
                _currentGamepadState = new GamePadState[GamePad.MaximumGamePadCount];
                for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
                {
                    _lastGamepadState[i] = GamePad.GetState(i);
                    _currentGamepadState[i] = GamePad.GetState(i);
                }
            }
            else
            {
                for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
                {
                    _lastGamepadState[i] = _currentGamepadState[i];
                    _currentGamepadState[i] = GamePad.GetState(i);
                }
            }
#if (!XBOX)
            if (!keyboardInitialized)
            {
                keyboardInitialized = true;
                _lastKeyboardState = _currentKeyboardState = Keyboard.GetState();
            }
            else
            {
                _lastKeyboardState = _currentKeyboardState;
                _currentKeyboardState = Keyboard.GetState();
            }
            if (!mouseInitialized)
            {
                mouseInitialized = true;
                _lastMouseState = _currentMouseState = Mouse.GetState();
            }
            else
            {
                _lastMouseState = _currentMouseState;
                _currentMouseState = Mouse.GetState();
            }
#endif
        }

        /// <summary>
        /// Creates an input helper object
        /// </summary>
        public InputHelper()
        {
        }


        /// <summary>
        /// The previous state of the gamepad. 
        /// Exposed only for convenience.
        /// </summary>
        public GamePadState[] LastGamepadState
        {
            get { return _lastGamepadState; }
        }
        /// <summary>
        /// the current state of the gamepad.
        /// Exposed only for convenience.
        /// </summary>
        public GamePadState[] CurrentGamepadState
        {
            get { return _currentGamepadState; }
        }
#if (!XBOX)
        /// <summary>
        /// The previous keyboard state.
        /// Exposed only for convenience.
        /// </summary>
        public KeyboardState LastKeyboardState
        {
            get { return _lastKeyboardState; }
        }
        /// <summary>
        /// The current state of the keyboard.
        /// Exposed only for convenience.
        /// </summary>
        public KeyboardState CurrentKeyboardState
        {
            get { return _currentKeyboardState; }
        }
        /// <summary>
        /// The previous mouse state.
        /// Exposed only for convenience.
        /// </summary>
        public MouseState LastMouseState
        {
            get { return _lastMouseState; }
        }
        /// <summary>
        /// The current state of the mouse.
        /// Exposed only for convenience.
        /// </summary>
        public MouseState CurrentMouseState
        {
            get { return _currentMouseState; }
        }
#endif
        /// <summary>
        /// The current position of the left stick. 
        /// Y is automatically reversed for you.
        /// </summary>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>Vector2 position of i'th player's left stick</returns>
        public Vector2 LeftStickPosition(int gamepad)
        {
            return new Vector2(
                    _currentGamepadState[gamepad].ThumbSticks.Left.X,
                    -CurrentGamepadState[gamepad].ThumbSticks.Left.Y);
        }
        /// <summary>
        /// The current position of the right stick.
        /// Y is automatically reversed for you.
        /// </summary>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>Vector2 position of i'th player's right stick</returns>
        public Vector2 RightStickPosition(int gamepad)
        {
            return new Vector2(
                    _currentGamepadState[gamepad].ThumbSticks.Right.X,
                    -CurrentGamepadState[gamepad].ThumbSticks.Right.Y);
        }
        /// <summary>
        /// The current velocity of the left stick.
        /// Y is automatically reversed for you.
        /// expressed as: 
        /// current stick position - last stick position.
        /// </summary>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>Vector2 velocity of i'th player's left stick</returns>
        public Vector2 LeftStickVelocity(int i)
        {
            Vector2 temp =
                _currentGamepadState[i].ThumbSticks.Left -
                _lastGamepadState[i].ThumbSticks.Left;
            return new Vector2(temp.X, -temp.Y);
        }
        /// <summary>
        /// The current velocity of the right stick.
        /// Y is automatically reversed for you.
        /// expressed as: 
        /// current stick position - last stick position.
        /// </summary>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>Vector2 velocity of i'th player's right stick</returns>
        public Vector2 RightStickVelocity(int i)
        {

            Vector2 temp =
                _currentGamepadState[i].ThumbSticks.Right -
                _lastGamepadState[i].ThumbSticks.Right;
            return new Vector2(temp.X, -temp.Y);

        }
        /// <summary>
        /// the current position of the left trigger.
        /// </summary>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>Position of i'th player's right trigger</returns>
        public float LeftTriggerPosition(int i)
        {
            return _currentGamepadState[i].Triggers.Left;
        }
        /// <summary>
        /// the current position of the right trigger.
        /// </summary>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>Positing of i'th player's right trigger</returns>
        public float RightTriggerPosition(int i)
        {
            return _currentGamepadState[i].Triggers.Right;
        }
        /// <summary>
        /// the velocity of the left trigger.
        /// expressed as: 
        /// current trigger position - last trigger position.
        /// </summary>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>Velocity magnitude of i'th player's left trigger</returns>
        public float LeftTriggerVelocity(int i)
        {
            return
                _currentGamepadState[i].Triggers.Left -
                _lastGamepadState[i].Triggers.Left;

        }
        /// <summary>
        /// the velocity of the right trigger.
        /// expressed as: 
        /// current trigger position - last trigger position.
        /// </summary>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>Velocity magnitude of i'th player's right trigger</returns>
        public float RightTriggerVelocity(int i)
        {

            return _currentGamepadState[i].Triggers.Right -
                _lastGamepadState[i].Triggers.Right;

        }
#if (!XBOX)
        /// <summary>
        /// the current mouse position.
        /// </summary>
        public Vector2 MousePosition
        {
            get { return new Vector2(_currentMouseState.X, _currentMouseState.Y); }
        }
        /// <summary>
        /// the current mouse velocity.
        /// Expressed as: 
        /// current mouse position - last mouse position.
        /// </summary>
        public Vector2 MouseVelocity
        {
            get
            {
                return (
                    new Vector2(_currentMouseState.X, _currentMouseState.Y) -
                    new Vector2(_lastMouseState.X, _lastMouseState.Y)
                    );
            }
        }
        /// <summary>
        /// the current mouse scroll wheel position.
        /// See the Mouse's ScrollWheel property for details.
        /// </summary>
        public float MouseScrollWheelPositionY
        {
            get
            {
                return _currentMouseState.ScrollWheelValue;
            }
        }
        /// <summary>
        /// the mouse scroll wheel velocity.
        /// Expressed as:
        /// current scroll wheel position - 
        /// the last scroll wheel position.
        /// </summary>
        public float MouseScrollWheelVelocityY
        {
            get
            {
                return (_currentMouseState.ScrollWheelValue - _lastMouseState.ScrollWheelValue);
            }
        }
        /// <summary>
        /// the current horizontal mouse scroll wheel position.
        /// See the Mouse's HorizontalScrollWheel property for details.
        /// </summary>
        public float MouseScrollWheelPositionX
        {
            get
            {
                return _currentMouseState.HorizontalScrollWheelValue;
            }
        }
        /// <summary>
        /// the horizontal mouse scroll wheel velocity.
        /// Expressed as:
        /// current horizontal scroll wheel position - 
        /// the last horizontal scroll wheel position.
        /// </summary>
        public float MouseScrollWheelVelocityX
        {
            get
            {
                return (_currentMouseState.HorizontalScrollWheelValue - _lastMouseState.HorizontalScrollWheelValue);
            }
        }

#endif
        /// <summary>
        /// Used for debug purposes.
        /// Indicates if the user wants to exit immediately.
        /// </summary>
        public bool ExitRequested
        {
            get
            {
                for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
                {
                    if (IsCurPress(Buttons.Back, i) && IsCurPress(Buttons.Start, i))
                        return true;
                }
#if (!XBOX)
                return IsCurPress(Keys.Escape);
#endif
#if (XBOX)
                return false;
#endif
            }
        }
        /// <summary>
        /// Checks if the requested button is a new press.
        /// </summary>
        /// <param name="button">
        /// The button to check.
        /// </param>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>
        /// a bool indicating whether the selected button is being 
        /// pressed in the current state but not the last state.
        /// </returns>
        public bool IsNewPress(Buttons button, int i)
        {
            return (
                _lastGamepadState[i].IsButtonUp(button) &&
                _currentGamepadState[i].IsButtonDown(button));
        }
        public bool IsNewPress(Buttons button)
        {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                if (_lastGamepadState[i].IsButtonUp(button) && _currentGamepadState[i].IsButtonDown(button))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the requested button is a current press.
        /// </summary>
        /// <param name="button">
        /// the button to check.
        /// </param>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>
        /// a bool indicating whether the selected button is being 
        /// pressed in the current state and in the last state.
        /// </returns>
        public bool IsCurPress(Buttons button, int i)
        {
            return (
                _lastGamepadState[i].IsButtonDown(button) &&
                _currentGamepadState[i].IsButtonDown(button));
        }
        public bool IsCurPress(Buttons button)
        {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                if (_lastGamepadState[i].IsButtonDown(button) && _currentGamepadState[i].IsButtonDown(button))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if the requested button is an old press.
        /// </summary>
        /// <param name="button">
        /// the button to check.
        /// </param>
        /// <param name="i">
        /// Player index
        /// </param>
        /// <returns>
        /// a bool indicating whether the selected button is not being
        /// pressed in the current state and is being pressed in the last state.
        /// </returns>
        public bool IsOldPress(Buttons button, int i)
        {
            return (
                _lastGamepadState[i].IsButtonDown(button) &&
                _currentGamepadState[i].IsButtonUp(button));
        }
        public bool IsOldPress(Buttons button)
        {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                if (_lastGamepadState[i].IsButtonDown(button) && _currentGamepadState[i].IsButtonUp(button))
                    return true;
            }
            return false;
        }
#if (!XBOX)
        /// <summary>
        /// Checks if the requested key is a new press.
        /// </summary>
        /// <param name="key">
        /// the key to check.
        /// </param>
        /// <returns>
        /// a bool that indicates whether the selected key is being 
        /// pressed in the current state and not in the last state.
        /// </returns>
        public bool IsNewPress(Keys key)
        {
            return (
                _lastKeyboardState.IsKeyUp(key) &&
                _currentKeyboardState.IsKeyDown(key));
        }
        /// <summary>
        /// Checks if the requested key is a current press.
        /// </summary>
        /// <param name="key">
        /// the key to check.
        /// </param>
        /// <returns>
        /// a bool that indicates whether the selected key is being 
        /// pressed in the current state and in the last state.
        /// </returns>
        public bool IsCurPress(Keys key)
        {
            return (
                _lastKeyboardState.IsKeyDown(key) &&
                _currentKeyboardState.IsKeyDown(key));
        }
        /// <summary>
        /// Checks if the requested button is an old press.
        /// </summary>
        /// <param name="key">
        /// the key to check.
        /// </param>
        /// <returns>
        /// a bool indicating whether the selectde button is not being
        /// pressed in the current state and being pressed in the last state.
        /// </returns>
        public bool IsOldPress(Keys key)
        {
            return (
                _lastKeyboardState.IsKeyDown(key) &&
                _currentKeyboardState.IsKeyUp(key));
        }
        /// <summary>
        /// Checks if the requested mosue button is a new press.
        /// </summary>
        /// <param name="button">
        /// teh mouse button to check.
        /// </param>
        /// <returns>
        /// a bool indicating whether the selected mouse button is being
        /// pressed in the current state but not in the last state.
        /// </returns>
        public bool IsNewPress(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return (
                        _lastMouseState.LeftButton == ButtonState.Released &&
                        _currentMouseState.LeftButton == ButtonState.Pressed);
                case MouseButtons.MiddleButton:
                    return (
                        _lastMouseState.MiddleButton == ButtonState.Released &&
                        _currentMouseState.MiddleButton == ButtonState.Pressed);
                case MouseButtons.RightButton:
                    return (
                        _lastMouseState.RightButton == ButtonState.Released &&
                        _currentMouseState.RightButton == ButtonState.Pressed);
                case MouseButtons.ExtraButton1:
                    return (
                        _lastMouseState.XButton1 == ButtonState.Released &&
                        _currentMouseState.XButton1 == ButtonState.Pressed);
                case MouseButtons.ExtraButton2:
                    return (
                        _lastMouseState.XButton2 == ButtonState.Released &&
                        _currentMouseState.XButton2 == ButtonState.Pressed);
                default:
                    return false;
            }
        }
        /// <summary>
        /// Checks if the requested mosue button is a current press.
        /// </summary>
        /// <param name="button">
        /// the mouse button to be checked.
        /// </param>
        /// <returns>
        /// a bool indicating whether the selected mouse button is being 
        /// pressed in the current state and in the last state.
        /// </returns>
        public bool IsCurPress(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return (
                        _lastMouseState.LeftButton == ButtonState.Pressed &&
                        _currentMouseState.LeftButton == ButtonState.Pressed);
                case MouseButtons.MiddleButton:
                    return (
                        _lastMouseState.MiddleButton == ButtonState.Pressed &&
                        _currentMouseState.MiddleButton == ButtonState.Pressed);
                case MouseButtons.RightButton:
                    return (
                        _lastMouseState.RightButton == ButtonState.Pressed &&
                        _currentMouseState.RightButton == ButtonState.Pressed);
                case MouseButtons.ExtraButton1:
                    return (
                        _lastMouseState.XButton1 == ButtonState.Pressed &&
                        _currentMouseState.XButton1 == ButtonState.Pressed);
                case MouseButtons.ExtraButton2:
                    return (
                        _lastMouseState.XButton2 == ButtonState.Pressed &&
                        _currentMouseState.XButton2 == ButtonState.Pressed);
                default:
                    return false;
            }
        }
        /// <summary>
        /// Checks if the requested mosue button is an old press.
        /// </summary>
        /// <param name="button">
        /// the mouse button to check.
        /// </param>
        /// <returns>
        /// a bool indicating whether the selected mouse button is not being 
        /// pressed in the current state and is being pressed in the old state.
        /// </returns>
        public bool IsOldPress(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return (
                        _lastMouseState.LeftButton == ButtonState.Pressed &&
                        _currentMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.MiddleButton:
                    return (
                        _lastMouseState.MiddleButton == ButtonState.Pressed &&
                        _currentMouseState.MiddleButton == ButtonState.Released);
                case MouseButtons.RightButton:
                    return (
                        _lastMouseState.RightButton == ButtonState.Pressed &&
                        _currentMouseState.RightButton == ButtonState.Released);
                case MouseButtons.ExtraButton1:
                    return (
                        _lastMouseState.XButton1 == ButtonState.Pressed &&
                        _currentMouseState.XButton1 == ButtonState.Released);
                case MouseButtons.ExtraButton2:
                    return (
                        _lastMouseState.XButton2 == ButtonState.Pressed &&
                        _currentMouseState.XButton2 == ButtonState.Released);
                default:
                    return false;
            }
        }
#endif

    }
}