using System;
using ClubeDaLeitura.ConsoleApp.Aprensacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaMulta : ITela
{
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioAmigo repositorioAmigo;

    public TelaMulta(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
    }

    public string? ObterEscolhaMenuInterno()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Emprestimo");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Visualizar multas em aberto");
        Console.WriteLine($"2 - Quitar Multas");
        Console.WriteLine($"3 - Visualizar multas de um amigo");
        Console.WriteLine($"S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();

        return opcaoMenuInterno;
    }

    public void VisualizarMultasEmAberto()
    {
        ExibirCabecalho("Multas em Aberto");

        Emprestimo?[] emprestimos = repositorioEmprestimo.SelecionarTodos();

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -10} | {3, -10}",
            "Id", "Amigo", "Valor", "Status"
        );

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = emprestimos[i];

            if (e == null)
                continue;

            e.GerarMulta();

            if (e.Multa == null || e.Multa.Status == StatusMulta.Quitada)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -15} | {2, -10} | {3, -10}",
                e.Id,
                e.Amigo.Nome,
                $"R$ {e.Multa.Valor}",
                e.Multa.Status
            );
        }

        Console.WriteLine("===============================");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public void QuitarMulta()
    {
        ExibirCabecalho("Quitar de Multas");

        Emprestimo?[] emprestimos = repositorioEmprestimo.SelecionarTodos();

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -10} | {3, -10}",
            "Id", "Amigo", "Valor", "Status"
        );

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = emprestimos[i];

            if (e == null)
                continue;

            e.GerarMulta();

            if (e.Multa == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -15} | {2, -10} | {3, -10}",
                e.Id,
                e.Amigo.Nome,
                $"R$ {e.Multa.Valor}",
                e.Multa.Status
            );
        }

        Console.WriteLine("===============================");

        Emprestimo? emprestimoSelecionado = null;
        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do empréstimo da multa: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idSelecionado);

        } while (emprestimoSelecionado == null);

        if (emprestimoSelecionado.Multa == null || emprestimoSelecionado.Multa.Status == StatusMulta.Quitada)
        {
            Console.WriteLine("Essa multa não está pendente.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -10} | {3, -10}",
            "Id", "Amigo", "Valor", "Status"
        );

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -10} | {3, -10}",
            emprestimoSelecionado.Id,
            emprestimoSelecionado.Amigo.Nome,
            $"R$ {emprestimoSelecionado.Multa.Valor}",
            emprestimoSelecionado.Multa.Status
            
        );

        Console.WriteLine("===============================");

        Console.WriteLine("Deseja realmente quitar a multa? (s/N)");
        string? opcao = Console.ReadLine()?.ToUpper();

        if (opcao != "S")
            return;

        emprestimoSelecionado.Multa.Quitar();

        ExibirMensagem($"Multa do empréstimo \"{emprestimoSelecionado.Id}\" quitada com sucesso!");
    }

    public void VisualizarMultasDeAmigo()
    {
        ExibirCabecalho("Multas de Amigos");

        Console.WriteLine(
            "{0, -7} | {1, -15}",
            "Id", "Nome"
        );

        EntidadeBase?[] amigos = repositorioAmigo.SelecionarTodos();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo? a = (Amigo?)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -15}",
                a.Id, a.Nome
            );
        }

        Console.WriteLine("===============================");

        Amigo? amigoSelecionado = null;
        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do amigo: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                amigoSelecionado = (Amigo?)repositorioAmigo.SelecionarPorId(idSelecionado);

        } while (amigoSelecionado == null);

        Console.WriteLine(
            "{0, -7} | {1, -10} | {2, -10}",
            "Emp.", "Valor", "Status"
        );

        for (int i = 0; i < amigoSelecionado.Emprestimos.Length; i++)
        {
            Emprestimo e = amigoSelecionado.Emprestimos[i];

            if (e == null)
                continue;

            e.GerarMulta();

            if (e.Multa == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -10} | {2, -10}",
                e.Id,
                $"R$ {e.Multa.Valor}",
                e.Multa.Status
            );
        }

        Console.WriteLine("===============================");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    protected void ExibirCabecalho(string nome)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Emprestimos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(nome);
        Console.WriteLine("---------------------------------");
    }

    protected static void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }
}

