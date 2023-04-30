using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public MaskableGraphic stampPanel;

    private PlayerCharacter player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerCharacter.Get;
        player.stampStateUpdated += UpdateStampPanel;
    }

    private void UpdateStampPanel(bool value)
    {
        if (stampPanel)
        {
            stampPanel.enabled = value;
        }
    }
}
