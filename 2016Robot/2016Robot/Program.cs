using System;
using System.Threading;
using Microsoft.SPOT;

namespace _2016Robot
{
    public class Program
    {
        Controller Controller = null;

        CTRE.TalonSrx FrontLeftMotor = null;
        CTRE.TalonSrx FrontRightMotor = null;
        CTRE.PWMSpeedController BackLeftMotor = null;
        CTRE.PWMSpeedController BackRightMotor = null;

        SpeedControllerGroup LeftMotors = null;
        SpeedControllerGroup RightMotors = null;

        DriveTrain DriveTrain = null;

        CTRE.PWMSpeedController IntakePivot = null;
        CTRE.PWMSpeedController IntakeSpin = null;

        bool EStopped;

        public static void Main()
        {
            new Program().Init();
        }

        //Initialize all variables and start the main loop
        public void Init()
        {
            Controller = new Controller();

            FrontLeftMotor = new CTRE.TalonSrx(1);
            FrontRightMotor = new CTRE.TalonSrx(2);
            BackLeftMotor = new CTRE.PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin4);
            BackRightMotor = new CTRE.PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin6);

            LeftMotors = new SpeedControllerGroup(FrontLeftMotor, BackLeftMotor);
            RightMotors = new SpeedControllerGroup(FrontRightMotor, BackRightMotor);

            DriveTrain = new DriveTrain(LeftMotors, RightMotors);

            IntakePivot = new CTRE.PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin7);
            IntakeSpin = new CTRE.PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin8);

            EStopped = false;

            Periodic();
        }

        //Update the motors
        public void Periodic()
        {
            while(true)
            {
                if(Controller.IsConnected() && !EStopped)
                {
                    DriveTrain.Drive(Controller);
                    RunIntakeSpin();
                    RunIntakePivot();

                    if(Controller.GetYButton()) EStopped = true;

                    if(!EStopped) CTRE.Watchdog.Feed();
                }

                Thread.Sleep(20);
            }
        }

        public void RunIntakeSpin()
        {
            if(Controller.GetLeftBumper())
            {
                IntakeSpin.Set(1);
            }
            else if(Controller.GetRightBumper())
            {
                IntakeSpin.Set(-1);
            }
            else
            {
                IntakeSpin.Set(0);
            }
        }

        public void RunIntakePivot()
        {
            double left = Controller.GetLeftTriggerAxis();
            double right = Controller.GetRightTriggerAxis();

            Utilities.Deadband(ref left);
            Utilities.Deadband(ref right);

            IntakePivot.Set((float) (right - left));
        }
    }
}
