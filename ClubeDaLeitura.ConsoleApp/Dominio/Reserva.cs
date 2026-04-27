using System;
using System.Runtime.CompilerServices;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public partial class Reserva
{

    Amigo Amigo { get; set; }
    Revista revista { get; set; }
    DateTime dataReserva { get; set; }

    public Reserva(Amigo amigo, Revista revista, DateTime dataReserva)
    {
        Amigo = amigo;
        this.revista = revista;
        this.dataReserva = dataReserva;
    }
}
