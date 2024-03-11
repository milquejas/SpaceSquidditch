using System.Collections.Generic;
using UnityEngine;

public class AiChasePlayerState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        // T‰h‰n lis‰t‰‰n toimintoja, jotka tapahtuvat kun tilaan tullaan.
    }

    public void Update(AiAgent agent)
    {
        // T‰h‰n lis‰t‰‰n toimintoja, jotka tapahtuvat joka p‰ivityksell‰ t‰ss‰ tilassa.
    }

    public void Exit(AiAgent agent)
    {
        // T‰h‰n lis‰t‰‰n toimintoja, jotka tapahtuvat kun tilasta poistutaan.
    }
}
