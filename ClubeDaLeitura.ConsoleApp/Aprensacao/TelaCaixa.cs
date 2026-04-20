using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaCaixa : TelaBase
{
    public RepositorioCaixa repositorioCaixa;

    public TelaCaixa(RepositorioCaixa rC): base("Caixa", rC)
    {
        repositorioCaixa = rC;
    }

    public override void VisualizarTodos(bool deveExbirCabecalho)
    {
        if (deveExbirCabecalho)
            ExibirCabecalho("Visualizar caixas");

        Console.WriteLine(
                   "{0, -7} | {1, -20} | {2, -10} | {3, -20}",
                   "Id", "Etiqueta", "Cor", "Tempo de Empréstimo"
               );

        EntidadeBase?[] caixas = repositorioCaixa.SelecionarTodos();

        for (int i = 0; i < caixas.Length; i++)
        {
            Caixa? c = (Caixa?)caixas[i];

            if (c == null)
                continue;

            Console.WriteLine(
              "{0, -7} | {1, -20} | {2, -10} | {3, -20}",
              c.Id, c.Equiqueta, c.Cor, c.DiasDeEmprestimo
          );
        }

        if (deveExbirCabecalho)
            Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    protected override EntidadeBase ObterDadosCastrais()
    {
        Console.Write("Informe a etiqueta da caixa: ");
        string? etiqueta = Console.ReadLine();

        Console.WriteLine("---------------------------------");
        Console.WriteLine("Informe uma cor valida");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("1 - Vermelho");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("2 - Verde");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("3 - Azul");
        Console.ResetColor();
        Console.WriteLine("4 - Branco");
        Console.WriteLine("---------------------------------");

        Console.Write("Informe a cor da caixa");
        string? codigoCor = Console.ReadLine();

        string? cor;
        if (codigoCor == "1")
            cor = "Vermelho";
        else if (codigoCor == "2")
            cor = "Verde";
        else if (codigoCor == "3")
            cor = "Azul";
        else
            cor = "Branco";

        Console.Write("Informe o tempo de empréstimo das revistas da caixa: ");
        int diasDeEmprestimo = Convert.ToInt32(Console.ReadLine());

        Caixa novaCaixa = new Caixa(etiqueta, cor, diasDeEmprestimo);

        return novaCaixa;
    }
}
