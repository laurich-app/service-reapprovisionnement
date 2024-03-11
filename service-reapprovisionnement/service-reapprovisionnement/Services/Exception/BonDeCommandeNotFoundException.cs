namespace services.Exception;

public class BonDeCommandeNotFoundException  : System.Exception
{
    public BonDeCommandeNotFoundException(string message) : base(message) { }

    public BonDeCommandeNotFoundException() { }
}