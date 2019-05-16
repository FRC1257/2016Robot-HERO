using System;
using Microsoft.SPOT;

namespace _2016Robot
{
    public class Controller
    {
        CTRE.Phoenix.Controller.GameController GameController = null;

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
            GameController = new CTRE.Phoenix.Controller.GameController(CTRE.Phoenix.UsbHostDevice.GetInstance(0));
            //CTRE.Phoenix.UsbHostDevice.GetInstance(0).SetSelectableXInputFilter(
                   //CTRE.Phoenix.UsbHostDevice.SelectableXInputFilter.XInputDevices);
        }

        public void Output()
        {
            Debug.Print("0: " + GameController.GetAxis(0));
            Debug.Print("1: " + GameController.GetAxis(1));
            Debug.Print("2: " + GameController.GetAxis(2));
            Debug.Print("3: " + GameController.GetAxis(3));
            Debug.Print("4: " + GameController.GetAxis(4));
            Debug.Print("5: " + GameController.GetAxis(5));
            Debug.Print("6: " + GameController.GetAxis(6));
            Debug.Print("7: " + GameController.GetAxis(7));
        }

        public bool IsConnected()
        {
            return GameController.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConnection.Connected;
        }

        public double GetLeftStickX()
        {
            return Utilities.Deadband(GameController.GetAxis(STICK_LEFT_X));
        }

        public double GetLeftStickY()
        {
            return Utilities.Deadband(-GameController.GetAxis(STICK_LEFT_Y));
        }

        public double GetRightStickX()
        {
            return Utilities.Deadband(GameController.GetAxis(STICK_RIGHT_X));
        }

        public double GetRightStickY()
        {
            return Utilities.Deadband(-GameController.GetAxis(STICK_RIGHT_Y));
        }

        public bool GetAButton()
        {
            return GameController.GetButton(BUTTON_A);
        }

        public bool GetBButton()
        {
            return GameController.GetButton(BUTTON_B);
        }

        public bool GetXButton()
        {
            return GameController.GetButton(BUTTON_X);
        }

        public bool GetYButton()
        {
            return GameController.GetButton(BUTTON_Y);
        }

        public bool GetLeftTrigger()
        {
            return GameController.GetButton(TRIGGER_LEFT);
        }

        public bool GetRightTrigger()
        {
            return GameController.GetButton(TRIGGER_RIGHT);
        }

        public bool GetLeftBumper()
        {
            return GameController.GetButton(BUMPER_LEFT);
        }

        public bool GetRightBumper()
        {
            return GameController.GetButton(BUMPER_RIGHT);
        }

        public bool GetBackButton()
        {
            return GameController.GetButton(BUTTON_BACK);
        }

        public bool GetStartButton()
        {
            return GameController.GetButton(BUTTON_START);
        }
    }
}
