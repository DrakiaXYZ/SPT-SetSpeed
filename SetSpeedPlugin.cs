using BepInEx;
using BepInEx.Configuration;
using Comfort.Common;
using EFT;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace DrakiaXYZ.SetSpeed
{
    [BepInPlugin("xyz.drakia.setspeed", "DrakiaXYZ-SetSpeed", "1.0.0")]
    public class SetSpeedPlugin : BaseUnityPlugin
    {
        Type _MovementStateType;
        Type _MovementContextType;

        FieldInfo _MovementContextField;

        MethodInfo _CurrentManagedStateGetter;
        MethodInfo _SetCharacterMovementSpeedMethod;
        MethodInfo _RaiseChangeSpeedEvent;
        MethodInfo _MaxSpeedGetter;

        private void Awake()
        {
            Settings.Init(Config);
            _CurrentManagedStateGetter = AccessTools.PropertyGetter(typeof(Player), "CurrentManagedState");

            // Support 3.5.8 and earlier
            if (_CurrentManagedStateGetter == null)
            {
                _CurrentManagedStateGetter = AccessTools.PropertyGetter(typeof(Player), "CurrentState");
            }

            _MovementStateType = _CurrentManagedStateGetter.ReturnType;

            _MovementContextField = AccessTools.Field(_MovementStateType, "MovementContext");
            _MovementContextType = _MovementContextField.FieldType;

            _SetCharacterMovementSpeedMethod = AccessTools.Method(_MovementContextType, "SetCharacterMovementSpeed");
            _RaiseChangeSpeedEvent = AccessTools.Method(_MovementContextType, "RaiseChangeSpeedEvent");
            _MaxSpeedGetter = AccessTools.PropertyGetter(_MovementContextType, "MaxSpeed");
        }

        public void Update()
        {
            if (!IsGameActive)
            {
                return;
            }

            if (IsKeyPressed(Settings.SetSpeed1Key.Value))
            {
                SetPlayerSpeed(Settings.Speed1.Value);   
            }
            if (IsKeyPressed(Settings.SetSpeed2Key.Value))
            {
                SetPlayerSpeed(Settings.Speed2.Value);
            }
            if (IsKeyPressed(Settings.SetSpeed3Key.Value))
            {
                SetPlayerSpeed(Settings.Speed3.Value);
            }
            if (IsKeyPressed(Settings.SetSpeed4Key.Value))
            {
                SetPlayerSpeed(Settings.Speed4.Value);
            }
            if (IsKeyPressed(Settings.SetSpeed5Key.Value))
            {
                SetPlayerSpeed(Settings.Speed5.Value);
            }
        }

        private bool IsGameActive => Singleton<IBotGame>.Instantiated;

        private void SetPlayerSpeed(int speed)
        {
            Player mainPlayer = Singleton<GameWorld>.Instance.MainPlayer;
            object currentManagedState = _CurrentManagedStateGetter.Invoke(mainPlayer, new object[] { });
            object movementContext = _MovementContextField.GetValue(currentManagedState);

            // Get the max speed, so we can make a range of 0 - MaxSpeed
            float maxSpeed = (float)_MaxSpeedGetter.Invoke(movementContext, new object[] { });
            float finalSpeed = (speed / 100f) * maxSpeed;

            _SetCharacterMovementSpeedMethod.Invoke(movementContext, new object[] { finalSpeed, false });
            _RaiseChangeSpeedEvent.Invoke(movementContext, new object[] { });
        }

        // Custom KeyPressed check that handles modifiers, but also lets you hit more than one key at a time
        private bool IsKeyPressed(KeyboardShortcut key)
        {
            if (!Input.GetKeyDown(key.MainKey))
            {
                return false;
            }

            foreach (var modifier in key.Modifiers)
            {
                if (!Input.GetKey(modifier))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
