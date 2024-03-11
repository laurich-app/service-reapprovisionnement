namespace services.Exception;

public class ProduitNotFoundException : System.Exception
{
    public ProduitNotFoundException(string message) : base(message) { }

    public ProduitNotFoundException() { }
}