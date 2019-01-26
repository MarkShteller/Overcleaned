public class Tool
{

    private readonly RoomManager.ToolType _toolType;
    
    public Tool(RoomManager.ToolType toolType)
    {
        _toolType = toolType;
    }
    
    public RoomManager.ToolType GetType()
    {
        return _toolType;
    }
}
