using System;
using System.Runtime.CompilerServices;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Reserva : EntidadeBase
{
    public Amigo Amigo { get; set; }
    public Revista Revista { get; set; }
    public DateTime dataReserva { get; set; }
    public StatusReserva Status { get; set; }
    public Reserva(Amigo amigo, Revista revista, DateTime dataReserva, StatusReserva status)
    {
        Amigo = amigo;
        Revista = revista;
        this.dataReserva = dataReserva;
        Status = status;
    }

    public Reserva(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
    }

    public override string[] Validar()
    {
        string erros = string.Empty;

        if (Amigo == null)
            erros += "O campo \"Amigo\" é obrigatório;";

        if (Revista == null)
            erros += "O campo \"Revista\" é obrigatório;";

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }

    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Reserva reservaAtualizada = (Reserva)entidadeAtualizada;

        Amigo = reservaAtualizada.Amigo;
        Revista = reservaAtualizada.Revista;
        dataReserva = reservaAtualizada.dataReserva;
        Status = reservaAtualizada.Status;
    }

    public void Cancelar()
    {
        Status = StatusReserva.Concluida;
    }
}
