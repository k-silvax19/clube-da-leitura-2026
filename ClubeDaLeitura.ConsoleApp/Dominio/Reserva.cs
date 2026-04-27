using System;
using System.Runtime.CompilerServices;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public class Reserva
{
    Amigo Amigo { get; set; }
    Revista revista { get; set; }
    DateTime dataReserva { get; set; }
    StatusReserva Status {get; set;}
    public Reserva(Amigo amigo, Revista revista, DateTime dataReserva, StatusReserva status)
    {
        Amigo = amigo;
        this.revista = revista;
        this.dataReserva = dataReserva;
        Status = status;
    }

}
