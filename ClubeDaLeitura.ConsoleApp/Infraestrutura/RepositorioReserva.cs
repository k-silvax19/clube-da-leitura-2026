using System;
using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioReserva : RepositorioBase
{
    public Reserva[] SelecionarReservas()
    {
        Reserva[] reservas = new Reserva[100];

        for (int i = 0; i < registoros.Length; i++)
        {
            reservas[i] = (Reserva)registoros[i];
        }
        return reservas;
    }
}