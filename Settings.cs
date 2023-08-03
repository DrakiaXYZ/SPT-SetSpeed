using BepInEx.Configuration;
using UnityEngine;

namespace DrakiaXYZ.SetSpeed
{
    internal class Settings
    {
        private const string KeybindsSectionTitle = "Keybinds";
        private const string SpeedsSectionTitle = "Speeds";

        public static ConfigEntry<KeyboardShortcut> SetSpeed1Key;
        public static ConfigEntry<KeyboardShortcut> SetSpeed2Key;
        public static ConfigEntry<KeyboardShortcut> SetSpeed3Key;
        public static ConfigEntry<KeyboardShortcut> SetSpeed4Key;
        public static ConfigEntry<KeyboardShortcut> SetSpeed5Key;

        public static ConfigEntry<int> Speed1;
        public static ConfigEntry<int> Speed2;
        public static ConfigEntry<int> Speed3;
        public static ConfigEntry<int> Speed4;
        public static ConfigEntry<int> Speed5;

        public static void Init(ConfigFile Config)
        {
            SetSpeed1Key = Config.Bind(
                KeybindsSectionTitle,
                "Set Speed 1 Key",
                new KeyboardShortcut(KeyCode.Keypad1),
                "Set Speed to configured Speed 1");

            SetSpeed2Key = Config.Bind(
                KeybindsSectionTitle,
                "Set Speed 2 Key",
                new KeyboardShortcut(KeyCode.Keypad2),
                "Set Speed to configured Speed 2");

            SetSpeed3Key = Config.Bind(
                KeybindsSectionTitle,
                "Set Speed 3 Key",
                new KeyboardShortcut(KeyCode.Keypad3),
                "Set Speed to configured Speed 3");

            SetSpeed4Key = Config.Bind(
                KeybindsSectionTitle,
                "Set Speed 4 Key",
                new KeyboardShortcut(KeyCode.Keypad4),
                "Set Speed to configured Speed 4");

            SetSpeed5Key = Config.Bind(
                KeybindsSectionTitle,
                "Set Speed 5 Key",
                new KeyboardShortcut(KeyCode.Keypad5),
                "Set Speed to configured Speed 5");

            Speed1 = Config.Bind(
                SpeedsSectionTitle,
                "Speed 1",
                0,
                new ConfigDescription(
                    "The speed that Speed Key 1 sets movement to",
                    new AcceptableValueRange<int>(0, 100)
                ));

            Speed2 = Config.Bind(
                SpeedsSectionTitle,
                "Speed 2",
                25,
                new ConfigDescription(
                    "The speed that Speed Key 2 sets movement to",
                    new AcceptableValueRange<int>(0, 100)
                ));

            Speed3 = Config.Bind(
                SpeedsSectionTitle,
                "Speed 3",
                50,
                new ConfigDescription(
                    "The speed that Speed Key 3 sets movement to",
                    new AcceptableValueRange<int>(0, 100)
                ));

            Speed4 = Config.Bind(
                SpeedsSectionTitle,
                "Speed 4",
                75,
                new ConfigDescription(
                    "The speed that Speed Key 4 sets movement to",
                    new AcceptableValueRange<int>(0, 100)
                ));

            Speed5 = Config.Bind(
                SpeedsSectionTitle,
                "Speed 5",
                100,
                new ConfigDescription(
                    "The speed that Speed Key 5 sets movement to",
                    new AcceptableValueRange<int>(0, 100)
                ));
        }
    }
}
