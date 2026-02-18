namespace B2B.Domain;
public class ContaBancaria
{
    private double _saldo;

    public Cliente Titular { get; private set; }

    public double Saldo
    {
        get => _saldo;
        private set
        {
            if (value < 0) throw new Exception("Saldo Invalido");
            _saldo = value;
        }
    }

    public ContaBancaria(Cliente titular, double saldo)
    {
        Titular = titular ?? throw new Exception("Titular Invalido");
        Saldo = saldo;
    }

    public void Depositar(double valor)
    {
        if (valor <= 0) throw new Exception("Deposito Invalido");
        Saldo += valor;
    }

    public void Sacar(double valor)
    {
        if (valor <= 0) throw new Exception("Saque invalido");
        if (Saldo < valor) throw new Exception("Saldo Insuficiente");
        Saldo -= valor;
    }
}
