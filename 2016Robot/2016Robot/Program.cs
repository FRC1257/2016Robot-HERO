using System;
using System.Threading;
using Microsoft.SPOT;

namespace _2016Robot
{
    public class Program
    {
        Controller controller = null;

        CTRE.TalonSrx frontLeftMotor = null;
        CTRE.TalonSrx frontRightMotor = null;
        CTRE.PWMSpeedController backLeftMotor = null;
        CTRE.PWMSpeedController backRightMotor = null;

        SpeedControllerGroup leftMotors = null;
        SpeedControllerGroup rightMotors = null;

        DriveTrain driveTrain = null;

        CTRE.PWMSpeedController intakePivot = null;
        CTRE.PWMSpeedController intakeSpin = null;

        public static void Main()
        {
            new Program().Init();
        }

        //Initialize all variables and start the main loop
        public void Init()
        {
            controller = new Controller();

            frontLeftMotor = new CTRE.TalonSrx(1);
            frontRightMotor = new CTRE.TalonSrx(2);
            backLeftMotor = new CTRE.PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin4);
            backRightMotor = new CTRE.PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin6);

            leftMotors = new SpeedControllerGroup(frontLeftMotor, backLeftMotor);
            rightMotors = new SpeedControllerGroup(frontRightMotor, backRightMotor);

            driveTrain = new DriveTrain(leftMotors, rightMotors);

            intakePivot = new CTRE.PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin7);
            intakeSpin = new CTRE.PWMSpeedController(CTRE.HERO.IO.Port3.PWM_Pin8);

            Periodic();
        }

        //Update the motors
        public void Periodic()
        {
            while(true)
            {
                if(controller.IsConnected())
                {
                    driveTrain.Drive(controller);
                    CTRE.Watchdog.Feed();
                }

                Thread.Sleep(20);
            }
        }
    }
}
