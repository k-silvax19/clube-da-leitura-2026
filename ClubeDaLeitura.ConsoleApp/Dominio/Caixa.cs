using System;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Caixa : EntidadeBase
{

    public string Equiqueta { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public int DiasDeEmprestimo { get; set; } = 7;

    public Caixa(string equiqueta, string cor, int diasDeEmprestimo)
    {
        Equiqueta = equiqueta;
        Cor = cor;
        DiasDeEmprestimo = diasDeEmprestimo;
    }

    public override string[] Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrWhiteSpace(Equiqueta))
        {
            erros += "O campo \"Etiqueta\" é obrigatório;";
        }

        else if (Equiqueta.Length > 50)
        {
            erros += "O campo \"Etiqueta\" deve conter no máximo 50 caracteres;";
        }

        if (DiasDeEmprestimo < 1)
        {
            erros += "O campo \"Dias de Empréstimo\" deve conter um valor maior que 0;";
        }

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries); // separar
    }
    public void AtualizarRegistro(Caixa caixaAtualizada)
    {
        Equiqueta = caixaAtualizada.Equiqueta;
        Cor = caixaAtualizada.Cor;
        DiasDeEmprestimo = caixaAtualizada.DiasDeEmprestimo;
    }

    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Caixa caixaAtualizada = (Caixa)entidadeAtualizada;

        Equiqueta = caixaAtualizada.Equiqueta;
        Cor = caixaAtualizada.Cor;
        DiasDeEmprestimo = caixaAtualizada.DiasDeEmprestimo;
    }
}
