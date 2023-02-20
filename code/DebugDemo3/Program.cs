using Metalama.Documentation.QuickStart;

public class Demo
{
    public static void Main(string[] args)
    {
        string detailXML = GetCustomerDetailsXML("CUST001");
    }

    [Retry]
    [Log]
    public static string GetCustomerDetailsXML(string customerID)
    {
        //TODO
        return string.Empty;
    }
}