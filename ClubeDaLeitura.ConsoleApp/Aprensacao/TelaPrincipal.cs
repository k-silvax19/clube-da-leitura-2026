using System;
using ClubeDaLeitura.ConsoleApp.Aprensacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Aprensacao;

public class TelaPrincipal
{
    private RepositorioCaixa repositorioCaixa;
    private RepositorioRevista repositorioRevista;
    private RepositorioAmigo repositorioAmigo;
    private RepositorioEmprestimo repositorioEmprestimo;

    public TelaPrincipal(RepositorioEmprestimo repositorioEmprestimo,
    RepositorioCaixa repositorioCaixa,
    RepositorioRevista repositorioRevista,
    RepositorioAmigo repositorioAmigo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioCaixa = repositorioCaixa;
        this.repositorioRevista = repositorioRevista;
        this.repositorioAmigo = repositorioAmigo;

        Caixa caixa = new Caixa("Lançamentos Naruto", "Vermelho", 3);
        repositorioCaixa.Cadastrar(caixa);
        Revista revista = new Revista("Panini - Naruto", 38, 2010, caixa);
        repositorioRevista.Cadastrar(revista);
        Amigo amigo = new Amigo("Kauan S", "Dona Deise", "49 98887-9313");
        repositorioAmigo.Cadastrar(amigo);

        Emprestimo emprestimo = new Emprestimo(revista, amigo);
        emprestimo.Abrir();
        repositorioEmprestimo.Cadastrar(emprestimo);
    }

    public ITela? AprensentarMenuOpcoesPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Clube da Leitura");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gerenciar caixas de revistas");
        Console.WriteLine("2 - Gerenciar revistas");
        Console.WriteLine("3 - Gerenciar amigos");
        Console.WriteLine("4 - Gerenciar empréstimos");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaCaixa(repositorioCaixa);

        if (opcaoMenuPrincipal == "2")
            return new TelaRevista(repositorioCaixa, repositorioRevista);

        if (opcaoMenuPrincipal == "3")
            return new TelaAmigo(repositorioAmigo);

        if (opcaoMenuPrincipal == "4")
            return new TelaEmprestimo(repositorioEmprestimo, repositorioRevista, repositorioAmigo);

        return null;
    }
}
