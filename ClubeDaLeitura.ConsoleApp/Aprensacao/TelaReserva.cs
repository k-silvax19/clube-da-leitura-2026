using System;
using ClubeDaLeitura.ConsoleApp.Aprensacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaReserva : ITela
{

    private RepositorioReserva repositorioReserva;
    private RepositorioAmigo repositorioAmigo;
    private RepositorioRevista repositorioRevista;

    public TelaReserva(
        RepositorioReserva repositorioReserva,
        RepositorioAmigo repositorioAmigo,
        RepositorioRevista repositorioRevista)
    {
        this.repositorioReserva = repositorioReserva;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }
    public string? ObterEscolhaMenuInterno()
    {

        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Reservas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Criar Reserva");
        Console.WriteLine("2 - Cancelar Reserva");
        Console.WriteLine("3 - Visualizar Reservas");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        return Console.ReadLine()?.ToUpper();
    }
    public void CriarReserva()
    {
        ExibirCabecalho("Cadastro de Reserva");

        Amigo amigo = SelecionarAmigo();
        Revista revista = SelecionarRevista();

        for (int i = 0; i < amigo.Emprestimos.Length; i++)
        {
            Emprestimo e = amigo.Emprestimos[i];

            if (e == null)
                continue;

            if (e.Multa != null && e.Multa.Status == StatusMulta.Pendente)
            {
                Console.WriteLine("Amigo possui multa pendente.");
                Console.ReadLine();
                return;
            }
        }

        if (revista.Status != StatusRevista.Disponivel)
        {
            Console.WriteLine("Revista não está disponível.");
            Console.ReadLine();
            return;
        }

        Reserva reserva = new Reserva(amigo, revista);

        repositorioReserva.Cadastrar(reserva);

        ExibirMensagem("Reserva criada com sucesso!");
    }

    public void CancelarReserva()
    {
        ExibirCabecalho("Cancelar Reserva");

        VisualizarReservas(false);

        Reserva? reservaSelecionada = null;
        string? id;

        do
        {
            Console.Write("Digite o ID da reserva: ");
            id = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(id) && id.Length == 7)
                reservaSelecionada = (Reserva?)repositorioReserva.SelecionarPorId(id);

        } while (reservaSelecionada == null);

        reservaSelecionada.Cancelar();

        ExibirMensagem("Reserva cancelada!");
    }

    public void VisualizarReservas(bool cabecalho)
    {
        if (cabecalho)
            ExibirCabecalho("Visualizar Reservas");

        EntidadeBase?[] registros = repositorioReserva.SelecionarTodos();

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -15} | {3, -10}",
            "Id", "Amigo", "Revista", "Status"
        );

        for (int i = 0; i < registros.Length; i++)
        {
            Reserva r = (Reserva?)registros[i];

            if (r == null)
                continue;

            Console.Write("{0, -7} | ", r.Id);
            Console.Write("{0, -15} | ", r.Amigo.Nome);
            Console.Write("{0, -15} | ", r.Revista.Titulo);

            string status = r.Status.ToString();

            if (r.Status == StatusReserva.Ativa)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (r.Status == StatusReserva.Concluida)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

            Console.Write("{0, -10}", status);
            Console.ResetColor();
            Console.WriteLine();
        }

        Console.WriteLine("===============================");
        Console.ReadLine();
    }
    private Amigo SelecionarAmigo()
    {
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

        Console.WriteLine("===============================");

        Amigo? amigo = null;
        string? id;

        do
        {
            Console.Write("Digite o ID do amigo: ");
            id = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(id) && id.Length == 7)
                amigo = (Amigo?)repositorioAmigo.SelecionarPorId(id);

        } while (amigo == null);

        return amigo;
    }

    private Revista SelecionarRevista()
    {
        Console.WriteLine(
            "{0, -7} | {1, -25}",
            "Id", "Título"
        );

        EntidadeBase?[] revistas = repositorioRevista.SelecionarTodos();

        for (int i = 0; i < revistas.Length; i++)
        {
            Revista? r = (Revista?)revistas[i];

            if (r == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -25}",
                r.Id, r.Titulo
            );
        }

        Console.WriteLine("===============================");

        Revista? revista = null;
        string? id;

        do
        {
            Console.Write("Digite o ID da revista: ");
            id = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(id) && id.Length == 7)
                revista = (Revista?)repositorioRevista.SelecionarPorId(id);

        } while (revista == null);

        return revista;
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