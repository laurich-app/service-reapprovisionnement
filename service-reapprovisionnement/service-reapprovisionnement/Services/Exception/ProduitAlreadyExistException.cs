namespace services.Exception;

public class ProduitAlreadyExistException : System.Exception
{
    public ProduitAlreadyExistException(string message) : base(message) { }

    public ProduitAlreadyExistException() { }
}