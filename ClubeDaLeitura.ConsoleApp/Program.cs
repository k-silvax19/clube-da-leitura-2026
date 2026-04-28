using ClubeDaLeitura.ConsoleApp.Aprensacao;
using ClubeDaLeitura.ConsoleApp.Aprensacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
RepositorioRevista repositorioRevista = new RepositorioRevista();
RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
RepositorioReserva repositorioReserva = new RepositorioReserva();

TelaPrincipal telaPrincipal = new TelaPrincipal(repositorioEmprestimo, repositorioCaixa, repositorioRevista, repositorioAmigo, repositorioReserva);

while (true)
{
    ITela? telaSelecionada = telaPrincipal.AprensentarMenuOpcoesPrincipal();

    if (telaSelecionada == null)
    {
        Console.Clear();
        break;
    }

    while (true)
    {
        if (telaSelecionada is TelaBase)
        {
            string? opcaoMenuInterno = telaSelecionada.ObterEscolhaMenuInterno();
            TelaBase telaBase = (TelaBase)telaSelecionada;

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
            {
                telaBase.Cadastrar();
                break;
            }
            else if (opcaoMenuInterno == "2")
            {
                telaBase.Editar();
                break;
            }

            else if (opcaoMenuInterno == "3")
            {
                telaBase.Excluir();
                break;
            }

            else if (opcaoMenuInterno == "4")
            {
                telaBase.VisualizarTodos(deveExbirCabecalho: true);
                break;
            }
        }

        else if (telaSelecionada is TelaEmprestimo)
        {
            string? opcaoMenuInterno;
            TelaEmprestimo telaEmprestimo = (TelaEmprestimo)telaSelecionada;

            opcaoMenuInterno = telaEmprestimo.ObterEscolhaMenuInterno();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
            {
                telaEmprestimo.Abrir();
                break;
            }
            else if (opcaoMenuInterno == "2")
            {
                telaEmprestimo.Concluir();
                break;
            }
            else if (opcaoMenuInterno == "3")
            {
                telaEmprestimo.VisualizarTodos(deveExbirCabecalho: true);
                break;
            }
        }
        else if (telaSelecionada is TelaMulta)
        {
            string? opcaoMenuInterno;
            TelaMulta telaMulta = (TelaMulta)telaSelecionada;

            opcaoMenuInterno = telaMulta.ObterEscolhaMenuInterno();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
            {
                telaMulta.VisualizarMultasEmAberto();
                break;
            }
            else if (opcaoMenuInterno == "2")
            {
                telaMulta.QuitarMulta();
                break;
            }
            else if (opcaoMenuInterno == "3")
            {
                telaMulta.VisualizarMultasDeAmigo();
                break;
            }
        }
        else if (telaSelecionada is TelaReserva)
        {
            string? opcaoMenuInterno;
            TelaReserva telaReserva = (TelaReserva)telaSelecionada;

            opcaoMenuInterno = telaReserva.ObterEscolhaMenuInterno();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }
            if (opcaoMenuInterno == "1")
            {
                telaReserva.CriarReserva();
                break;
            }
            else if (opcaoMenuInterno == "2")
            {
                telaReserva.CancelarReserva();
                break;
            }
            else if (opcaoMenuInterno == "3")
            {
                telaReserva.VisualizarReservas(cabecalho: true);
                break;
            }
        }
    }
}