using System;
using ClubeDaLeitura.ConsoleApp.Aprensacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public abstract class TelaBase : ITela
{
    private string nomeEntidade = string.Empty;
    private RepositorioBase repositorio;

    public string? ObterEscolhaMenuInterno()
    {
        string nomeMinusculo = nomeEntidade.ToLower();

        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar {nomeMinusculo}");
        Console.WriteLine($"2 - Editar {nomeMinusculo}");
        Console.WriteLine($"3 - Excluir {nomeMinusculo}");
        Console.WriteLine($"4 - Visualizar {nomeMinusculo}s");
        Console.WriteLine($"S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();

        return opcaoMenuInterno;
    }

    protected TelaBase(string nomeEntidade, RepositorioBase repositorio)
    {
        this.nomeEntidade = nomeEntidade;
    }

    public void Cadastrar()
    {
        ExibirCabecalho($"Cadastro de {nomeEntidade}");

        EntidadeBase novaEntidade = ObterDadosCastrais();

        string[] erros = novaEntidade.Validar();

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

            Cadastrar();
            return;
        }

        repositorio.Cadastrar(novaEntidade);

        ExibirMensagem($"O registro \"{novaEntidade.Id}\" foi cadastrado com sucesso!");
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

        EntidadeBase novaEntidade = ObterDadosCastrais();

        string[] erros = novaEntidade.Validar();

        if (erros.Length < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
            {
                Console.ResetColor();

                string erro = erros[i];

                Console.WriteLine(erro);
            }
            Console.ResetColor();

            Editar();
            return;

        }

        bool conseguiuEditar = repositorio.Editar(idSelecionado, novaEntidade);

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

        bool conseguiuExcluir = repositorio.Excluir(idSelecionado);

        if (!conseguiuExcluir)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi excluido com sucesso.");
    }
    public abstract void VisualizarTodos(bool deveExbirCabecalho);
    protected abstract EntidadeBase ObterDadosCastrais();

    protected void ExibirCabecalho(string nome)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
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
