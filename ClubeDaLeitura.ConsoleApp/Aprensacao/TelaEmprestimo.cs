using System;
using System.Security.Cryptography.X509Certificates;
using ClubeDaLeitura.ConsoleApp.Aprensacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaEmprestimo : ITela
{
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioRevista repositorioRevista;
    private RepositorioAmigo repositorioAmigo;
    
    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }

    public string? ObterEscolhaMenuInterno()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Emprestimo");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Abrir Emprestimo");
        Console.WriteLine($"2 - Concluir Emprestimo");
        Console.WriteLine($"3 - Visualizar Emprestimos");
        Console.WriteLine($"S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();

        return opcaoMenuInterno;
    }

    public void Abrir()
    {
        Emprestimo emprestimo = ObterDadosCasdastrais();

        if (emprestimo.Amigo.Emprestimos == null)
            return;

        for (int i = 0; i < emprestimo.Amigo.Emprestimos.Length; i++)
        {
            Emprestimo e = emprestimo.Amigo.Emprestimos[i];

            if (e == null)
                continue;

            if (e.Multa != null && e.Multa.Status == StatusMulta.Pendente)
            {
                Console.WriteLine("Esse amigo tem uma multa pendente.");
                Console.ReadLine();
                return;
            }
        }

        string[] erros = emprestimo.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine("---------------------------------");

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
            {
                string erro = erros[i];

                Console.WriteLine(erro);
            }

            Console.ResetColor();
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();

            Abrir();
            return;
        }

        emprestimo.Abrir();

        repositorioEmprestimo.Cadastrar(emprestimo);

        ExibirMensagem($"O \"{emprestimo.Id}\" foi aberto com sucesso!");
    }

    public void Concluir()
    {
        ExibirCabecalho("Conclusão de Empréstimos");

        VisualizarTodos(deveExbirCabecalho: false);

        Console.WriteLine("===============================");

        Emprestimo? emprestimoSelecionado = null;

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID da da revista que deseja emprestar: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idSelecionado);

        } while (emprestimoSelecionado == null);

        Console.WriteLine(
              "{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -15}",
              "Id", "Revista", "Amigo", "Abertura", "Conclusão Prev."
          );
        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -15}",
            emprestimoSelecionado.Id, emprestimoSelecionado.Revista.Titulo, emprestimoSelecionado.Amigo.Nome, emprestimoSelecionado.Abertura.ToShortDateString(), emprestimoSelecionado.ConclusãoPrevista.ToShortDateString()
        );
        Console.WriteLine("===============================");

        Console.WriteLine("Deseja realmente concluir o empréstimo selecionado? (s/N)");
        string? opcaoContinuar = Console.ReadLine()?.ToUpper();

        if (opcaoContinuar != "S")
        {
            return;
        }

        emprestimoSelecionado.Concluir();

        ExibirMensagem($"O \"{emprestimoSelecionado.Id}\" foi concluído com sucesso!");

    }

    public void VisualizarTodos(bool deveExbirCabecalho)
    {
        if (deveExbirCabecalho)
        {
            ExibirCabecalho("Visualizar Emprestimos");
        }

        Console.WriteLine("{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -15} | {5, -10}",
      "Id", "Revista", "Amigo", "Inicio", "Conclusão Prev", "Status");


        Emprestimo?[] emprestimos = repositorioEmprestimo.SelecionarTodos();

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = emprestimos[i];

            if (e == null)
                continue;

            
            Console.Write("{0, -7} | ", e.Id);
            Console.Write("{0, -15} | ", e.Revista.Titulo);
            Console.Write("{0, -10} | ", e.Amigo.Nome);
            Console.Write("{0, -10} | ", e.Abertura.ToShortDateString());
            Console.Write("{0, -15} | ", e.ConclusãoPrevista.ToShortDateString());

            string status = e.Status.ToString();
            if (e.EstaAtrasado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                status = "Atrasado";
            }
            else if (e.Status == StatusEmprestimo.Indefinido)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (e.Status == StatusEmprestimo.Aberto)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (e.Status == StatusEmprestimo.Concluido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.Write("{0, -10}", status);
            Console.ResetColor();
            Console.WriteLine();
        }

        if (deveExbirCabecalho)
            Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    private Emprestimo ObterDadosCasdastrais()
    {
        VisualizarRevistas();

        string? idSelecionado;
        Revista? revista = null;

        do
        {
            Console.Write("Digite o ID da da revista que deseja emprestar: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                revista = (Revista?)repositorioRevista.SelecionarPorId(idSelecionado);

        } while (revista == null);

        Console.WriteLine("===============================");

        VisualizarAmigos();

        Amigo? amigo = null;

        do
        {
            Console.Write("Digite o ID do amigo que receberá a revista: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                amigo = (Amigo?)repositorioAmigo.SelecionarPorId(idSelecionado);

        } while (amigo == null);

        return new Emprestimo(revista, amigo);
    }

    private void VisualizarRevistas()
    {
        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -15}",
            "Id", "Título", "Edição", "Ano", "Revista"
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
        Console.WriteLine("===============================");
    }

    private void VisualizarAmigos()
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
