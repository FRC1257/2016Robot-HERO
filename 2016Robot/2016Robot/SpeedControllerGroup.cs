using System;
using Microsoft.SPOT;

namespace _2016Robot
{
    class SpeedControllerGroup
    {
        CTRE.PWMSpeedController motor1PWM = null;
        CTRE.TalonSrx motor1SRX = null;

        CTRE.PWMSpeedController motor2PWM = null;
        CTRE.TalonSrx motor2SRX = null;

        public SpeedControllerGroup(CTRE.PWMSpeedController motor1, CTRE.PWMSpeedController motor2)
        {
            motor1PWM = motor1;
            motor2PWM = motor2;
        }

        public SpeedControllerGroup(CTRE.TalonSrx motor1, CTRE.PWMSpeedController motor2)
        {
            motor1SRX = motor1;
            motor2PWM = motor2;
        }

        public SpeedControllerGroup(CTRE.PWMSpeedController motor1, CTRE.TalonSrx motor2)
        {
            motor1PWM = motor1;
            motor2SRX = motor2;
        }

        public SpeedControllerGroup(CTRE.TalonSrx motor1, CTRE.TalonSrx motor2)
        {
            motor1SRX = motor1;
            motor2SRX = motor2;
        }

        public void Set(double value)
        {
            if (motor1PWM != null) motor1PWM.Set((float)value);
            if (motor2PWM != null) motor2PWM.Set((float)value);
            if (motor1SRX != null) motor1SRX.Set((float)value);
            if (motor2SRX != null) motor2SRX.Set((float)value);
        }

        public double Get()
        {
            if (motor1PWM != null) return motor1PWM.Get();
            if (motor2PWM != null) return motor2PWM.Get();
            if (motor1SRX != null) return motor1SRX.Get();
            if (motor2SRX != null) return motor2SRX.Get();

            return 0;
        }
    }
}
