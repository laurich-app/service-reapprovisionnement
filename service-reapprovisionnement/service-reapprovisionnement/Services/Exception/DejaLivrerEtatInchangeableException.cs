namespace services.Exception;

public class DejaLivrerEtatInchangeableException  : System.Exception
{
    public DejaLivrerEtatInchangeableException(string message) : base(message) { }

    public DejaLivrerEtatInchangeableException() { }
}