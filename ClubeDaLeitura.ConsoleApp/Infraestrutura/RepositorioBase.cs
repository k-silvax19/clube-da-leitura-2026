using System;
using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public abstract class RepositorioBase
{
    protected EntidadeBase?[] registoros = new EntidadeBase[100];

    public void Cadastrar(EntidadeBase entidade)
    {
        for (int i = 0; i < registoros.Length; i++)
        {
            if (registoros[i] == null)
            {
                registoros[i] = entidade;
                break;
            }
        }
    }

    public bool Editar(string idSelecionado, EntidadeBase entidade)
    {
        EntidadeBase? entidadeSelecionada = SelecionarPorId(idSelecionado);

        if (entidadeSelecionada == null)
            return false;

        entidadeSelecionada.AtualizarRegistro(entidade);

        return true;
    }

    public EntidadeBase?[] SelecionarTodos()
    {
        return registoros;
    }


    public bool Excluir(string idSelecionado)
    {

        for (int i = 0; i < registoros.Length; i++)
        {
            EntidadeBase? c = registoros[i];

            if (c == null)
                continue;
            if (c.Id == idSelecionado)
            {
                registoros[i] = null;
                return true;
            }
        }
        return false;
    }

    public EntidadeBase? SelecionarPorId(string idSelecionado)
    {
        EntidadeBase? EntidadeBaseSelecionada = null;

        for (int i = 0; i < registoros.Length; i++)
        {
            EntidadeBase? c = registoros[i];

            if (c == null)
                continue;
            if (c.Id == idSelecionado)
            {
                EntidadeBaseSelecionada = c;
                break;
            }
        }

        return EntidadeBaseSelecionada;
    }

}