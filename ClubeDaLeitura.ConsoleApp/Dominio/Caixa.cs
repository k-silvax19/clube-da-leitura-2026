using System;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Caixa
{
    public string Id { get; set; } = string.Empty;
    public string Equiqueta { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public int DiasDeEmprestimo { get; set; } = 7;

    public Caixa(string equiqueta, string cor, int diasDeEmprestimo)
    {
        Id = Convert
            .ToHexString(RandomNumberGenerator.GetBytes(20))
            .ToLower()
            .Substring(0, 7);
        Equiqueta = equiqueta;
        Cor = cor;
        DiasDeEmprestimo = diasDeEmprestimo;
    }
    
    public void AtualizarRegistro(Caixa caixaAtualizada)
    {
        Equiqueta = caixaAtualizada.Equiqueta;
        Cor = caixaAtualizada.Cor;
        DiasDeEmprestimo = caixaAtualizada.DiasDeEmprestimo;
    }
}
