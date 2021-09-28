using System;

[Serializable]
public class GameSettings
{
    private static GameSettings instance;

    public static GameSettings Instiance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameSettings();
            }
            return instance;
        }
    }
    
    public void Initialize(GameSettings targetSettings)
    {
        instance = targetSettings;
    }

    [Serializable]
    public struct PlayerMovementSettings
    {
        public float movementSpeed;
        public float dashSpeed;
    }

    public PlayerMovementSettings playerMovementSettings;
}
