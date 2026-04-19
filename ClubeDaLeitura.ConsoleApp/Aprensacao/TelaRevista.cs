using System;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaRevista
{
    private RepositorioRevista repositorioRevista;
    private RepositorioCaixa repositorioCaixa;

    public TelaRevista(RepositorioCaixa rC, RepositorioRevista rR)
    {
        repositorioRevista = rR;
        repositorioCaixa = rC;
    }

    public string ObterEscolhaMenuInterno()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de revistas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar revista");
        Console.WriteLine("2 - Editar revista");
        Console.WriteLine("3 - Excluir revista");
        Console.WriteLine("4 - Visualizar revistas");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();

        return opcaoMenuInterno;
    }

    public void Cadastrar()
    {
        ExibirCabecalho("Cadastrar revista");

        Revista novaRevista = ObterDadosCadastrais();

        string[] erros = novaRevista.Validar();

        if (erros.Length > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
            {
                Console.ResetColor();

                string erro = erros[i];

                Console.WriteLine(erro);
            }
            Console.ResetColor();
            ExibirMensagem("Tente Novamente");

            Cadastrar();
            return;

        }

        repositorioRevista.Cadastrar(novaRevista);

        ExibirMensagem($"O registro de \"{novaRevista.Id}\" foi cadastrado com sucesso.");

    }

    public void Editar()
    {
        ExibirCabecalho("Edição de Revista");

        VisualizarTodos(deveExibirCabecalho: false);

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja editar: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        Console.WriteLine("---------------------------------");

        Revista novaRevista = ObterDadosCadastrais();

        string[] erros = novaRevista.Validar();

        if (erros.Length > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
            {
                Console.ResetColor();

                string erro = erros[i];

                Console.WriteLine(erro);
            }
            Console.ResetColor();
            ExibirMensagem("Tente Novamente");

            Editar();
            return;
        }

        bool conseguiuEditar = repositorioRevista.Editar(idSelecionado, novaRevista);

        if (!conseguiuEditar)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }
        ExibirMensagem($"O registro \"{idSelecionado}\" foi editado com sucesso.");
    }
    public void Excluir()
    {
        ExibirCabecalho("Exclusão de revistas");

        VisualizarTodos(deveExibirCabecalho: false);

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja excluir: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        bool conseguiuExcluir = repositorioRevista.Excluir(idSelecionado);

        if (!conseguiuExcluir)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi excluido com sucesso.");
    }

    public void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Revistas");

        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -15}",
            "Id", "Título", "Edição", "Ano", "Revista"
        );

        Revista?[] revistas = repositorioRevista.SelecionarTodas();

        for (int i = 0; i < revistas.Length; i++)
        {
            Revista? r = revistas[i];

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

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    public void ExibirCabecalho(string nome)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Revistas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(nome);
        Console.WriteLine("---------------------------------");
    }

    private static void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    private Revista ObterDadosCadastrais()
    {
        Console.Write("Digite o título da revista: ");
        string? titulo = Console.ReadLine();

        Console.Write("Digite o número da edição: ");
        int numeroEdicao = Convert.ToInt32(Console.ReadLine());

        Console.Write("Digite o ano de publicação: ");
        int anoPublicacao = Convert.ToInt32(Console.ReadLine());

        ExibirCabecalho("Visualizar caixas");

        string idSelecionado = SelecionarCaixa();

        Caixa? caixaSelecionada = repositorioCaixa.SelecionarPorId(idSelecionado);

        Revista novaRevista;

        return new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);
    }

    private string SelecionarCaixa()
    {
        Console.WriteLine(
                  "{0, -7} | {1, -20} | {2, -10} | {3, -20}",
                  "Id", "Etiqueta", "Cor", "Tempo de Empréstimo"
              );

        Caixa?[] caixas = repositorioCaixa.SelecionarTodos();

        for (int i = 0; i < caixas.Length; i++)
        {
            Caixa? c = caixas[i];

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