using System.Threading;
using Microsoft.SPOT;

using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using CTRE.Phoenix.Drive;

namespace _2016Robot
{
    public class Program
    {
        TalonSRX FrontLeftMotor = null;
        TalonSRX FrontRightMotor = null;
        TalonSRX BackLeftMotor = null;
        TalonSRX BackRightMotor = null;

        TalonSRX IntakePivot = null;
        TalonSRX IntakeSpin = null;

        Tank DriveTrain = null;

        Controller Controller = null;

        bool EStopped;

        public static void Main()
        {
            new Program().Init();
        }

        // Initialize all variables and start the main loop
        // TODO RECHECK FIRMWARE VERSIONS
        public void Init()
        {
            Controller = new Controller();

            FrontLeftMotor = new TalonSRX(1);
            FrontRightMotor = new TalonSRX(2);
            BackLeftMotor = new TalonSRX(3);
            BackRightMotor = new TalonSRX(4);

            BackLeftMotor.Follow(FrontLeftMotor);
            BackRightMotor.Follow(FrontRightMotor);

            //BackRightMotor.SetInverted(true);

            DriveTrain = new Tank(FrontLeftMotor, FrontRightMotor, false, false);

            IntakePivot = new TalonSRX(5);
            IntakeSpin = new TalonSRX(6);

            EStopped = false;

            Periodic();
        }
        
        // Update the motors
        public void Periodic()
        {
            while(true)
            {
                if(Controller.IsConnected() && !EStopped)
                {
                    //DriveTrain.Set(Styles.BasicStyle.PercentOutput, 
                    //    (float) Controller.GetLeftStickY(), (float) Controller.GetRightStickX());
                    //RunIntakeSpin();
                    //RunIntakePivot();

                    FrontRightMotor.Set(ControlMode.PercentOutput, Controller.GetLeftStickY());
                    //F.Set(ControlMode.PercentOutput, Controller.GetLeftStickY());

                    if (Controller.GetYButton()) EStopped = true;
                    CTRE.Phoenix.Watchdog.Feed();
                }
                Controller.Output();
                if(EStopped)
                {
                    Debug.Print("ROBOT ESTOPPED");
                }
                Thread.Sleep(20);
            }
        }

        public void RunIntakeSpin()
        {
            if(Controller.GetLeftBumper())
            {
                IntakeSpin.Set(ControlMode.PercentOutput, 1);
            }
            else if(Controller.GetRightBumper())
            {
                IntakeSpin.Set(ControlMode.PercentOutput, -1);
            }
            else
            {
                IntakeSpin.Set(ControlMode.PercentOutput, 0);
            }
        }

        public void RunIntakePivot()
        {
            double output = 0;
            if(Controller.GetLeftTrigger()) output -= 0.8;
            if(Controller.GetRightTrigger()) output += 0.8;
            
            IntakePivot.Set(ControlMode.PercentOutput, (float) output);
        }
    }
}
