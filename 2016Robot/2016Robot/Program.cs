using System.Threading;
using Microsoft.SPOT;

using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

namespace _2016Robot
{
    public class Program
    {
        TalonSRX frontLeftMotor = null;
        TalonSRX frontRightMotor = null;
        TalonSRX backLeftMotor = null;
        TalonSRX backRightMotor = null;

        TalonSRX intakePivot = null;
        TalonSRX intakeRoller = null;

        Tank driveTrain = null;

        Controller controller = null;

        bool disabled;
        bool prevButton;

        public static void Main()
        {
            new Program().Init();
        }

        // Initialize all variables and start the main loop
        // TODO RECHECK FIRMWARE VERSIONS
        // TODO CURRENT VERSION OF CTRE HERO PHOENIX ON TEAM LAPTOP IS NOT UP TO DATE
        public void Init()
        {
            controller = new Controller();

            frontLeftMotor = new TalonSRX(1);
            frontRightMotor = new TalonSRX(2);
            backLeftMotor = new TalonSRX(3);
            backRightMotor = new TalonSRX(4);

            frontLeftMotor.SetNeutralMode(NeutralMode.Brake);
            frontRightMotor.SetNeutralMode(NeutralMode.Brake);
            backLeftMotor.SetNeutralMode(NeutralMode.Brake);
            backRightMotor.SetNeutralMode(NeutralMode.Brake);

            backLeftMotor.Follow(frontLeftMotor);
            backRightMotor.Follow(frontRightMotor);

            intakePivot = new TalonSRX(5);
            intakeRoller = new TalonSRX(6);

            disabled = false;
            prevButton = false;

            InitPeriodic();
        }
        
        // Update the motors
        public void InitPeriodic()
        {
            while(true)
            {
                if(controller.IsConnected() && !disabled)
                {
                    double forward = limit(controller.GetLeftStickY());
                    double turn = limit(controller.GetRightStickX());

                    frontRightMotor.Set(ControlMode.PercentOutput, (float) forward,
                        DemandType.ArbitraryFeedForward, (float) -turn);
                    frontLeftMotor.Set(ControlMode.PercentOutput, (float) forward,
                        DemandType.ArbitraryFeedForward, (float) +turn);

                    //IntakeRoller();
                    //IntakePivot();

                    CTRE.Phoenix.Watchdog.Feed();
                }
                if(!prevButton && controller.GetStartButton())
                {
                    disabled = !disabled;
                }
                prevButton = controller.GetStartButton();
                Thread.Sleep(20);
            }
        }

        public void IntakeRoller()
        {
            if(controller.GetLeftBumper())
            {
                intakeRoller.Set(ControlMode.PercentOutput, 1);
            }
            else if(controller.GetRightBumper())
            {
                intakeRoller.Set(ControlMode.PercentOutput, -1);
            }
            else
            {
                intakeRoller.Set(ControlMode.PercentOutput, 0);
            }
        }

        public void IntakePivot()
        {
            double output = 0;
            if(controller.GetLeftTrigger()) output -= 0.8;
            if(controller.GetRightTrigger()) output += 0.8;
            
            intakePivot.Set(ControlMode.PercentOutput, (float) output);
        }
    }
}
