using System;
using Microsoft.SPOT;

namespace _2016Robot
{
    class DriveTrain
    {
        SpeedControllerGroup leftMotors;
        SpeedControllerGroup rightMotors;

        public DriveTrain(SpeedControllerGroup leftMotors, SpeedControllerGroup rightMotors)
        {
            this.leftMotors = leftMotors;
            this.rightMotors = rightMotors;
        }

        public void Drive(Controller controller)
        {
            double leftX = controller.GetLeftStickX();
            double leftY = controller.GetLeftStickY();

            double rightX = controller.GetRightStickX();
            double rightY = controller.GetRightStickY();

            Utilities.Deadband(ref leftX);
            Utilities.Deadband(ref leftY);

            Utilities.Deadband(ref rightX);
            Utilities.Deadband(ref rightY);

            double driveSpeed = 0, turnSpeed = 0;

            driveSpeed = rightY;
            turnSpeed = leftX;

            ArcadeDrive(driveSpeed, turnSpeed);
        }

        public void ArcadeDrive(double driveSpeed, double turnSpeed, bool squared = true)
        {
            Utilities.Limit(ref driveSpeed);
            Utilities.Deadband(ref driveSpeed);
            Utilities.Limit(ref turnSpeed);
            Utilities.Deadband(ref turnSpeed);

            if (squared)
            {
                driveSpeed = driveSpeed * System.Math.Abs(driveSpeed);
                turnSpeed = turnSpeed * System.Math.Abs(turnSpeed);
            }

            double leftMotorOutput, rightMotorOutput;

            if (driveSpeed >= 0.0)
            {
                if (turnSpeed >= 0.0)
                {
                    leftMotorOutput = 1.0;
                    rightMotorOutput = driveSpeed - turnSpeed;
                }
                else
                {
                    leftMotorOutput = driveSpeed + turnSpeed;
                    rightMotorOutput = 1.0;
                }
            }
            else
            {
                if (turnSpeed >= 0.0)
                {
                    leftMotorOutput = driveSpeed + turnSpeed;
                    rightMotorOutput = 1.0;
                }
                else
                {
                    leftMotorOutput = 1.0;
                    rightMotorOutput = driveSpeed - turnSpeed;
                }
            }

            Utilities.Limit(ref leftMotorOutput);
            Utilities.Limit(ref rightMotorOutput);

            SetDriveMotors(leftMotorOutput, rightMotorOutput);
        }

        public void SetDriveMotors(double leftMotorOutput, double rightMotorOutput)
        {
            leftMotors.Set(leftMotorOutput);
            rightMotors.Set(rightMotorOutput);
        }
    }
}
