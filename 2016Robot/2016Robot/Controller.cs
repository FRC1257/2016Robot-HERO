using System;
using Microsoft.SPOT;

namespace _2016Robot
{
    public class Controller
    {
        CTRE.Phoenix.Controller.GameController gameController = null;

        public const int STICK_LEFT_X = 0;
        public const int STICK_LEFT_Y = 1;
        public const int STICK_RIGHT_X = 2;
        public const int STICK_RIGHT_Y = 3;

        public const int BUTTON_Y = 4;
        public const int BUTTON_B = 2;
        public const int BUTTON_A = 1;
        public const int BUTTON_X = 3;

        public const int TRIGGER_LEFT = 4;
        public const int TRIGGER_RIGHT = 5;

        public const int BUMPER_LEFT = 5;
        public const int BUMPER_RIGHT = 6;

        public const int BUTTON_BACK = 7;
        public const int BUTTON_START = 8;

        public const int STICK_LEFT = 9;
        public const int STICK_RIGHT = 10;

        public Controller()
        {
            gameController = new CTRE.Phoenix.Controller.GameController(CTRE.Phoenix.UsbHostDevice.GetInstance(0));
            CTRE.Phoenix.UsbHostDevice.GetInstance(0).SetSelectableXInputFilter(
                   CTRE.Phoenix.UsbHostDevice.SelectableXInputFilter.XInputDevices);
        }

        public void OutputAxisValues()
        {
            Debug.Print("0: " + gameController.GetAxis(0));
            Debug.Print("1: " + gameController.GetAxis(1));
            Debug.Print("2: " + gameController.GetAxis(2));
            Debug.Print("3: " + gameController.GetAxis(3));
            Debug.Print("4: " + gameController.GetAxis(4));
            Debug.Print("5: " + gameController.GetAxis(5));
            Debug.Print("6: " + gameController.GetAxis(6));
            Debug.Print("7: " + gameController.GetAxis(7));
        }

        public void PrintActive()
        {
            for(int i = 1; i <= 15; i++)
            {
                if(gameController.GetButton((uint) i) == true)
                {
                    Debug.Print("" + i);
                }
            }
        }

        public bool IsConnected()
        {
            return gameController.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConnection.Connected;
        }

        public double GetLeftStickX()
        {
            return Utilities.Deadband(gameController.GetAxis(STICK_LEFT_X));
        }

        public double GetLeftStickY()
        {
            return Utilities.Deadband(gameController.GetAxis(STICK_LEFT_Y));
        }

        public double GetRightStickX()
        {
            return Utilities.Deadband(gameController.GetAxis(STICK_RIGHT_X));
        }

        public double GetRightStickY()
        {
            return Utilities.Deadband(gameController.GetAxis(STICK_RIGHT_Y));
        }

        public bool GetAButton()
        {
            return gameController.GetButton(BUTTON_A);
        }

        public bool GetBButton()
        {
            return gameController.GetButton(BUTTON_B);
        }

        public bool GetXButton()
        {
            return gameController.GetButton(BUTTON_X);
        }

        public bool GetYButton()
        {
            return gameController.GetButton(BUTTON_Y);
        }

        public double GetLeftTrigger()
        {
            return Utilities.Deadband((gameController.GetAxis(TRIGGER_LEFT) + 1.0) / 2.0);
        }

        public double GetRightTrigger()
        {
            return Utilities.Deadband((gameController.GetAxis(TRIGGER_RIGHT) + 1.0) / 2.0);
        }

        public bool GetLeftBumper()
        {
            return gameController.GetButton(BUMPER_LEFT);
        }

        public bool GetRightBumper()
        {
            return gameController.GetButton(BUMPER_RIGHT);
        }

        public bool GetBackButton()
        {
            return gameController.GetButton(BUTTON_BACK);
        }

        public bool GetStartButton()
        {
            return gameController.GetButton(BUTTON_START);
        }

        public bool GetLeftStick()
        {
            return gameController.GetButton(STICK_LEFT);
        }

        public bool GetRightStick()
        {
            return gameController.GetButton(STICK_RIGHT);
        }
    }
}
