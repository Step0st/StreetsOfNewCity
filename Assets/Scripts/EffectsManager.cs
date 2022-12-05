public static class EffectsManager
{
    public static void TakeEffect(PlayableCard card, CharacterManager targetCharacter)
    {
        switch (card.cardType)
        {
            case CardType.AttackCard:
                targetCharacter.TakeDamage(card.effectValue);
                break;
            case CardType.PoisonCard:
                targetCharacter.Poisoning(card.temporaryEffectValue, card.duration);
                break;
            case CardType.DefenceCard:
                targetCharacter.GetArmor(card.effectValue, card.duration);
                break;
            case CardType.HealCard:
                targetCharacter.Heal(card.effectValue);
                break;
        }
    }
}