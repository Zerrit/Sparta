using UnityEngine;
using Zparta.Configs;
using Zparta.Joystick_Pack.Scripts.Base;
using Zparta.Services;
using Object = UnityEngine.Object;

namespace Zparta.Factories.Joysticks
{
    public class JoystickFactory : IJoystickFactory
    {
        private Transform _joystickCanvas;
        private readonly JoystickFactoryConfig _config;

        
        public JoystickFactory(JoystickFactoryConfig config)
        {
            _config = config;
        }

        
        public Joystick CreteJoystick()
        {
            if (_joystickCanvas == null) 
                _joystickCanvas = Object.Instantiate(_config.Canvas);
            
            return Object.Instantiate(_config.FloatingJoystick, _joystickCanvas);
        }
    }
}