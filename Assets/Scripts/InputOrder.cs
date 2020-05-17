/// <summary>
/// Порядок ввода, необходим для добавления точек,
/// если перескочили
/// </summary>
public struct InputOrder 
{
    public int newValue;
    public int needToAddValue;

    public InputOrder(int newValue, int needToAddValue)
    {
        this.newValue = newValue;
        this.needToAddValue = needToAddValue;
    }
}
