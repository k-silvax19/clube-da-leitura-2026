using System;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaRevista : TelaBase
{
    private RepositorioRevista repositorioRevista;
    private RepositorioCaixa repositorioCaixa;

    public TelaRevista(RepositorioCaixa rC, RepositorioRevista rR) : base("Revista", rR)
    {
        repositorioRevista = rR;
        repositorioCaixa = rC;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Revistas");

        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -15} | {5, -10} ",
            "Id", "Título", "Edição", "Ano", "Revista", "Status"
        );

        EntidadeBase?[] revistas = repositorioRevista.SelecionarTodos();

        for (int i = 0; i < revistas.Length; i++)
        {
            Revista? r = (Revista?)revistas[i];

            if (r == null)
                continue;

            Console.Write("{0, -7} | ", r.Id);
            Console.Write("{0, -25} | ", r.Titulo);
            Console.Write("{0, -6} | ", r.NumeroEdicao);
            Console.Write("{0, -4} | ", r.AnoPublicacao);

            string status = r.Status.ToString();

            if (r.Status == StatusRevista.Disponivel)
                status = "Disponível";

            Console.Write("{0, -10} | ", r.Status);

            string corSelecionada = r.Caixa.Cor;

            if (corSelecionada == "Vermelho")
                Console.ForegroundColor = ConsoleColor.Red;

            else if (corSelecionada == "Verde")
                Console.ForegroundColor = ConsoleColor.Green;

            else if (corSelecionada == "Azul")
                Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write("{0, -15}", r.Caixa.Equiqueta);

            Console.ResetColor();
            Console.WriteLine();
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override EntidadeBase ObterDadosCastrais()
    {
        Console.Write("Digite o título da revista: ");
        string? titulo = Console.ReadLine();

        Console.Write("Digite o número da edição: ");
        int numeroEdicao = Convert.ToInt32(Console.ReadLine());

        Console.Write("Digite o ano de publicação: ");
        int anoPublicacao = Convert.ToInt32(Console.ReadLine());

        ExibirCabecalho("Visualizar caixas");

        string idSelecionado = SelecionarCaixa();

        Caixa? caixaSelecionada = (Caixa?)repositorioCaixa.SelecionarPorId(idSelecionado);

        Revista novaRevista;

        return new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);
    }

    private string SelecionarCaixa()
    {
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
              c.Id, c.Equiqueta, c.Cor, c.DiasDeEmprestimo);
        }

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID da caixa em que deseja guardar a revista: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        return idSelecionado;
    }
}