using System;
using System.Security.Cryptography;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Emprestimo
{
    public Multa Multa { get; set; }
    public string Id { get; set; } = string.Empty;
    public Revista Revista { get; set; }
    public Amigo Amigo { get; set; }
    public DateTime Abertura { get; set; }
    public DateTime ConclusãoPrevista
    {
        get
        {
            int diasDeEmprestimo = Revista.Caixa.DiasDeEmprestimo;

            DateTime conclusao = Abertura.AddDays(diasDeEmprestimo);

            return conclusao;

        }
    }
    public bool EstaAtrasado
    {
        get
        {
            return Status == StatusEmprestimo.Aberto && DateTime.Now > ConclusãoPrevista;
        }
    }
    public StatusEmprestimo Status { get; set; } = StatusEmprestimo.Indefinido;

    public Emprestimo(Revista revista, Amigo amigo)
    {
        Id = Convert
         .ToHexString(RandomNumberGenerator.GetBytes(20))
         .ToLower()
         .Substring(0, 7);

        Amigo = amigo;
        Revista = revista;
    }

    public string[] Validar()
    {
        string erros = string.Empty;

        if (Revista == null)
            erros = erros += "O campo \"Revista\" deve ser preenchido;";

        if (Amigo == null)
            erros = erros += "O campo \"Amigo\" deve ser preenchido;";

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }

    public void Abrir()
    {
        Abertura = DateTime.Now;

        Status = StatusEmprestimo.Aberto;
        Revista.Emprestar();

        Amigo.AdicionarEmprestimo(this);
    }

    public void Concluir()
    {
        Status = StatusEmprestimo.Concluido;
        Revista.Devolver();
    }

    public int CalcularDiasAtraso()
    {
        if (!EstaAtrasado)
            return 0;

        return (DateTime.Now - ConclusãoPrevista).Days;
    }
    
    public void GerarMulta()
    {
        if (!EstaAtrasado)
            return;

        if (Multa != null)
            return;

        int dias = CalcularDiasAtraso();

        Multa = new Multa(this, dias);
    }
}