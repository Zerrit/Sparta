using UnityEngine;
using Zparta.Joystick_Pack.Scripts.Base;

namespace Zparta.Configs
{
    [CreateAssetMenu(fileName = "JoystickFactoryConfig", menuName = "Configs/JoystickFactory")]
    public class JoystickFactoryConfig : ScriptableObject
    {
        [field:SerializeField] public Transform Canvas { get; private set; }
        [field:SerializeField] public Joystick FloatingJoystick { get; private set; }
    }
}