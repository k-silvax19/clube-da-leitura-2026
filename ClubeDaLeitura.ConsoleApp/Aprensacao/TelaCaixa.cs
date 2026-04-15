using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaCaixa
{
    private RepositorioCaixa repositorioCaixa;

    public TelaCaixa(RepositorioCaixa rC)
    {
        repositorioCaixa = rC;
    }
    public string? ObterEscolhaMenuInterno()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de caixas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar caixa");
        Console.WriteLine("2 - Editar caixa");
        Console.WriteLine("3 - Excluir caixa");
        Console.WriteLine("4 - Visualizar caixas");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();

        return opcaoMenuInterno;
    }

    public void Cadastrar()
    {
        ExibirCabecalho("Cadastrar Caixa");

        Caixa novaCaixa = ObterDadosCastrais();

        repositorioCaixa.Cadastrar(novaCaixa);

        Console.WriteLine("---------------------------------");
        Console.WriteLine($"O registro de \"{novaCaixa.Id}\" foi cadastrado com sucesso.");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();

    }

    public void Editar()
    {
        ExibirCabecalho("Edição de caixa");

        VisualizarTodos(deveExbirCabecalho: false);
        
        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja editar: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        Console.WriteLine("---------------------------------");

        Caixa novaCaixa = ObterDadosCastrais();

        bool conseguiuEditar = repositorioCaixa.Editar(idSelecionado, novaCaixa);

        if (!conseguiuEditar)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi editado com sucesso.");
    }

    public void Excluir()
    {
        ExibirCabecalho("Exclusão de caixas");

        VisualizarTodos(deveExbirCabecalho: false);

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja excluir: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        bool conseguiuExcluir = repositorioCaixa.Excluir(idSelecionado);

        if (!conseguiuExcluir)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi excluido com sucesso.");
    }

    public void VisualizarTodos(bool deveExbirCabecalho)
    {
        if (deveExbirCabecalho)
            ExibirCabecalho("Visualizar caixas");

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
              c.Id, c.Equiqueta, c.Cor, c.DiasDeEmprestimo
          );
        }

        if (deveExbirCabecalho)
            Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();


    }

    public void ExibirCabecalho(string nome)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de caixas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(nome);
        Console.WriteLine("---------------------------------");
    }

    private Caixa ObterDadosCastrais()
    {
        Console.Write("Informe a etiqueta da caixa");
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
    private static void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }
}
