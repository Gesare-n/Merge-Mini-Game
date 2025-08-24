[System.Serializable]
public class MergeEvent
{
    public string MergedItemType;
    public int MergedItemCount;
    
    public MergeEvent(string itemType, int count)
    {
        MergedItemType = itemType;
        MergedItemCount = count;
    }
}