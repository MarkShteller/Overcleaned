public class Tool
{

    private readonly ToolType _toolType;
    
    public Tool(ToolType toolType)
    {
        _toolType = toolType;
    }
    
    public ToolType GetType()
    {
        return _toolType;
    }
}
