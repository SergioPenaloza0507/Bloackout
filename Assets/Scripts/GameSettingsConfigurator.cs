using UnityEngine;

public class GameSettingsConfigurator : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    private void Awake()
    {
        gameSettings.Initialize(gameSettings);
    }
}
