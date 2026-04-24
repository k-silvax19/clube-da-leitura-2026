using System;
using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioEmprestimo
{
    private Emprestimo[] emprestimos = new Emprestimo[100];
    public void Cadastrar(Emprestimo emprestimo)
    {
        for (int i = 0; i < emprestimos.Length; i++)
        {
            if (emprestimos[i] == null)
            {
                emprestimos[i] = emprestimo;
                break;
            }
        }
    }

    public Emprestimo?[] SelecionarTodos()
    {
        return emprestimos;
    }

    public Emprestimo? SelecionarPorId(string idSelecionado)
    {
        Emprestimo? EmprestimoSelecionado = null;

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = emprestimos[i];

            if (e == null)
                continue;
            if (e.Id == idSelecionado)
            {
                EmprestimoSelecionado = e;
                break;
            }
        }

        return EmprestimoSelecionado;
    }

    
}
