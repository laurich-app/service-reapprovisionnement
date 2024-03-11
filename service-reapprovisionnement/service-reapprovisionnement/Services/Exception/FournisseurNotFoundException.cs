namespace services.Exception;

public class FournisseurNotFoundException : System.Exception
{
    public FournisseurNotFoundException(string message) : base(message) { }

    public FournisseurNotFoundException() { }
}