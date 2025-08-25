public class FruitCollectedEvent : GameEvent
{
    public string FruitType;
    public int Amount;
    
    public FruitCollectedEvent(string fruitType, int amount = 1)
    {
        FruitType = fruitType;
        Amount = amount;
        EventDescription = $"Collected {amount} {fruitType}(s)";
    }
}
