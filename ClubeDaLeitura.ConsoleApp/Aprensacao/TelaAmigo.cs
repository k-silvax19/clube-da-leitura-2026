using System;
using System.Diagnostics;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaAmigo : TelaBase
{
    private RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo) : base("Amigo", repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    public override void VisualizarTodos(bool deveExbirCabecalho)
    {
        if (deveExbirCabecalho)
            ExibirCabecalho("Visualizar Amigos");

        Console.WriteLine(
                   "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
                   "Id", "Nome", "Responsável", "Telefone"
               );

        EntidadeBase?[] amigos = repositorioAmigo.SelecionarTodos();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo? a = (Amigo?)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine(
              "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
              a.Id, a.Nome, a.NomeResponsavel, a.Telefone
          );
        }

        if (deveExbirCabecalho)
            Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    protected override EntidadeBase ObterDadosCastrais()
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Nome do responsável: ");
        string nomeResponsavel = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o Telefone: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        return new Amigo(nome, nomeResponsavel, telefone);
    }
}
