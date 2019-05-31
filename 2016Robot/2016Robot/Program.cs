using System.Threading;

using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

namespace _2016Robot
{
    public class Program
    {
        float DRIVE_FORWARD_SPEED = 1.0;
        float DRIVE_TURN_SPEED = 1.0;
        float INTAKE_SPEED = 1.0;
        float EJECT_SPEED = -1.0;
        float CONSTANT_INTAKE_SPEED = 0.0;
        float PIVOT_SPEED = 1.0;

        TalonSRX frontLeftMotor = null;
        TalonSRX frontRightMotor = null;
        TalonSRX backLeftMotor = null;
        TalonSRX backRightMotor = null;

        TalonSRX intakePivot = null;
        TalonSRX intakeRoller = null;

        Controller controller = null;

        bool disabled;
        bool prevButton;

        public static void Main()
        {
            new Program().Init();
        }

        // Initialize all variables and start the main loop
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
                    Drive();
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

        public void Drive()
        {
            double forward = Utilities.Limit(controller.GetLeftStickY()) * DRIVE_FORWARD_SPEED;
            double turn = Utilities.Limit(controller.GetRightStickX()) * DRIVE_TURN_SPEED;

            frontRightMotor.Set(ControlMode.PercentOutput, (float) -forward,
                DemandType.ArbitraryFeedForward, (float) turn);
            frontLeftMotor.Set(ControlMode.PercentOutput, (float) forward,
                DemandType.ArbitraryFeedForward, (float) turn);
        }

        public void IntakeRoller()
        {
            if(controller.GetLeftBumper())
            {
                intakeRoller.Set(ControlMode.PercentOutput, INTAKE_SPEED);
            }
            else if(controller.GetRightBumper())
            {
                intakeRoller.Set(ControlMode.PercentOutput, EJECT_SPEED);
            }
            else
            {
                intakeRoller.Set(ControlMode.PercentOutput, CONSTANT_INTAKE_SPEED);
            }
        }

        public void IntakePivot()
        {
            double controllerOutput = Utilities.Limit(controller.GetRightTrigger() - 
                controller.GetLeftTrigger());
            
            intakePivot.Set(ControlMode.PercentOutput, (float) controllerOutput * PIVOT_SPEED);
        }
    }
}
