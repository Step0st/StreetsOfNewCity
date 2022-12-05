public static class PlayerTargeting
{
    public static CharacterManager ChooseTarget(PlayableCard card)
    {
        if (card.cardType == CardType.AttackCard || card.cardType == CardType.PoisonCard)
        {
            if (MouseData.charMouseIsOver.characterTeam == CharacterTeam.OpponentTeam)
            {
                return MouseData.charMouseIsOver;
            }
            else
            {
                return null;
            }
        }

        if (card.cardType == CardType.HealCard || card.cardType == CardType.DefenceCard)
        {
            if (MouseData.charMouseIsOver.characterTeam == CharacterTeam.PlayerTeam)
            {
                return MouseData.charMouseIsOver;
            }
            else
            {
                return null;
            }
        }

        return null;
    }
}
