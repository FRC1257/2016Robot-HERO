using System;
using Microsoft.SPOT;

namespace _2016Robot
{
    public class Controller
    {
        CTRE.Gamepad gamepad = null;

        public const int STICK_LEFT_X = 0;
        public const int STICK_LEFT_Y = 1;
        public const int STICK_RIGHT_X = 2;
        public const int STICK_RIGHT_Y = 3;

        public const int BUTTON_Y = 4;
        public const int BUTTON_B = 3;
        public const int BUTTON_A = 2;
        public const int BUTTON_X = 1;

        public const int TRIGGER_LEFT = 7;
        public const int TRIGGER_RIGHT = 8;

        public const int BUMPER_LEFT = 5;
        public const int BUMPER_RIGHT = 6;

        public const int BUTTON_BACK = 9;
        public const int BUTTON_START = 10;

        public Controller()
        {
            gamepad = new CTRE.Gamepad(CTRE.UsbHostDevice.GetInstance());
        }

        public bool IsConnected()
        {
            return gamepad.GetConnectionStatus() == CTRE.UsbDeviceConnection.Connected;
        }

        public double GetLeftStickX()
        {
            return gamepad.GetAxis(STICK_LEFT_X);
        }

        public double GetLeftStickY()
        {
            return -gamepad.GetAxis(STICK_LEFT_Y);
        }

        public double GetRightStickX()
        {
            return gamepad.GetAxis(STICK_RIGHT_X);
        }

        public double GetRightStickY()
        {
            return -gamepad.GetAxis(STICK_RIGHT_Y);
        }

        public bool GetAButton()
        {
            return gamepad.GetButton(BUTTON_A);
        }

        public bool GetBButton()
        {
            return gamepad.GetButton(BUTTON_B);
        }

        public bool GetXButton()
        {
            return gamepad.GetButton(BUTTON_X);
        }

        public bool GetYButton()
        {
            return gamepad.GetButton(BUTTON_Y);
        }

        public bool GetTriggerLeft()
        {
            return gamepad.GetButton(TRIGGER_LEFT);
        }

        public bool GetTriggerRight()
        {
            return gamepad.GetButton(TRIGGER_RIGHT);
        }

        public bool GetLeftBumper()
        {
            return gamepad.GetButton(BUMPER_LEFT);
        }

        public bool GetRightBumper()
        {
            return gamepad.GetButton(BUMPER_RIGHT);
        }

        public bool GetBackButton()
        {
            return gamepad.GetButton(BUTTON_BACK);
        }

        public bool GetStartButton()
        {
            return gamepad.GetButton(BUTTON_START);
        }
    }
}
