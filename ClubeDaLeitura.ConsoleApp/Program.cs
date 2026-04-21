using ClubeDaLeitura.ConsoleApp.Aprensacao;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
RepositorioRevista repositorioRevista = new RepositorioRevista();
RepositorioAmigo repositorioAmigo = new RepositorioAmigo();

Caixa caixa = new Caixa("Lançamentos", "Vermelho", 3);
repositorioCaixa.Cadastrar(caixa);

TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
TelaRevista telaRevista = new TelaRevista(repositorioCaixa, repositorioRevista);
TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);

EntidadeBase entidade = caixa;

entidade.AtualizarRegistro(new Caixa("Teste", "Azul", 4));

while (true)
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

    if (opcaoMenuPrincipal == "S")
    {
        Console.Clear();
        break;
    }

    while (true)
    {
        string? opcaoMenuInterno = string.Empty;

        if (opcaoMenuPrincipal == "1")
        {
            opcaoMenuInterno = telaCaixa.ObterEscolhaMenuInterno();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
            {
                telaCaixa.Cadastrar();
                break;
            }
            else if (opcaoMenuInterno == "2")
            {
                telaCaixa.Editar();
                break;
            }

            else if (opcaoMenuInterno == "3")
            {
                telaCaixa.Excluir();
                break;
            }

            else if (opcaoMenuInterno == "4")
            {
                telaCaixa.VisualizarTodos(deveExbirCabecalho: true);
                break;
            }
        }

        else if (opcaoMenuPrincipal == "2")
        {
            opcaoMenuInterno = telaRevista.ObterEscolhaMenuInterno();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
            {
                telaRevista.Cadastrar();
                break;
            }
            else if (opcaoMenuInterno == "2")
            {
                telaRevista.Editar();
                break;
            }

            else if (opcaoMenuInterno == "3")
            {
                telaRevista.Excluir();
                break;
            }

            else if (opcaoMenuInterno == "4")
            {
                telaRevista.VisualizarTodos(deveExibirCabecalho: true);
                break;
            }
        }

        else if (opcaoMenuPrincipal == "3")
        {
            opcaoMenuInterno = telaAmigo.ObterEscolhaMenuInterno();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
            {
                telaAmigo.Cadastrar();
                break;
            }
            else if (opcaoMenuInterno == "2")
            {
                telaAmigo.Editar();
                break;
            }

            else if (opcaoMenuInterno == "3")
            {
                telaAmigo.Excluir();
                break;
            }

            else if (opcaoMenuInterno == "4")
            {
                telaAmigo.VisualizarTodos(deveExbirCabecalho: true);
                break;
            }
        }

        else if (opcaoMenuPrincipal == "4")
        {

        }
    }
}