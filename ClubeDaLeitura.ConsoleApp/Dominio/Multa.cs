using System;
using System.Data;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Multa : EntidadeBase
{
    public Emprestimo Emprestimo { get; set; }
    public decimal Valor { get; set; }
    public int DiasAtraso { get; set; }
    public StatusMulta Status { get; set; }


    public Multa(Emprestimo emprestimo, int diasAtraso)
    {
        Emprestimo = emprestimo;
        DiasAtraso = diasAtraso;

        Valor = diasAtraso * 2;
        Status = StatusMulta.Pendente;
    }

    public void Quitar()
    {
        Status = StatusMulta.Quitada;
    }

    public override string[] Validar()
    {
        string erros = string.Empty;

        if (Emprestimo == null)
            erros += "O campo \"Empréstimo\" é obrigatório;";

        if (DiasAtraso <= 0)
            erros += "Dias de atraso deve ser maior que zero;";

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }

    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Multa multaAtualizada = (Multa)entidadeAtualizada;

        Emprestimo = multaAtualizada.Emprestimo;
        DiasAtraso = multaAtualizada.DiasAtraso;
        Valor = multaAtualizada.Valor;
        Status = multaAtualizada.Status;
    }
}