using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

class program
{
    static string diretorio = "Data Source = BancoDeDados.db";

    static void Main(string[] args)
    {
        int i = 0;
        CriarTabela();

        using var conexao = new SqliteConnection(diretorio);
        conexao.Open();

        while (i == 0)
        {
            Console.Clear();
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1. Cadastrar Filme");
            Console.WriteLine("2. Excluir Filme");
            Console.WriteLine("3. Alterar Filme");
            Console.WriteLine("4. Listar Filmes");
            Console.WriteLine("5. Procurar FIlme");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            int op = int.Parse(Console.ReadLine());
            switch (op)
            {
                case 1:
                    CadastrarFilme();
                    break;
                case 2:
                    ExcluirFilme();
                    break;
                case 3:
                    AlterarFilme();
                    break;
                case 4:
                    ListarFilmes();
                    break;
                case 5:
                    ProcurarFilme();
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    i++;
                    break;
            }
            Console.WriteLine("Digite alguma tecla para continuar");
            Console.ReadLine();
        }
    }

    static void CriarTabela()
    {
        using var conexao = new SqliteConnection(diretorio);
        conexao.Open();
        var comando = conexao.CreateCommand();
        comando.CommandText = @"
        CREATE TABLE IF NOT EXISTS Filmes (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL,
            DataLancamento DATE NOT NULL,
            Descricao TEXT
            );";
        comando.ExecuteNonQuery();
    }

    static void CadastrarFilme()
    {
        Console.WriteLine("Digite o nome do filme");
        string nome = Console.ReadLine();
        Console.WriteLine("Digite a data de lançamento");
        string data = Console.ReadLine();
        Console.WriteLine("Digite uma descrição (opcional)");
        string descricao = Console.ReadLine();
        DateTime dataLancamento;
        try
        {
            dataLancamento = DateTime.Parse(data);
        }
        catch (FormatException)
        {
            Console.WriteLine("Data inválida, seu filme não foi cadastrado!");
            return;
        }
        using var conexao = new SqliteConnection(diretorio);
        conexao.Open();
        var comando = conexao.CreateCommand();
        comando.CommandText = "INSERT INTO Filmes (Nome, DataLancamento, Descricao) VALUES ($nome, $data,$descricao)";
        comando.Parameters.AddWithValue("$nome", nome);
        comando.Parameters.AddWithValue("$data", data);
        comando.Parameters.AddWithValue("$descricao", descricao);
        comando.ExecuteNonQuery();
        Console.WriteLine("Filme cadastrado!");
    }

    static void ExcluirFilme()
    {
        Console.WriteLine("Digite o Id do filme");
        int id;
        try
        {
            id = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Parâmetro inválido passado como Id!");
            return;
        }
        if (id < 0)
        {
            Console.WriteLine("Parâmetro inválido passado como Id!");
            return;
        }
        using var conexao = new SqliteConnection(diretorio);
        conexao.Open();
        var comando = conexao.CreateCommand();
        comando.CommandText = "DELETE FROM Filmes WHERE Id = $id";
        comando.Parameters.AddWithValue("$id", id);
        int linhas = comando.ExecuteNonQuery();
        if (linhas > 0)
        {
            Console.WriteLine("Filme removido!");
        }
        else
        {
            Console.WriteLine("Filme não encontrado!");
        }
    }

    static void ListarFilmes()
    {
        using var conexao = new SqliteConnection(diretorio);
        conexao.Open();
        var comando = conexao.CreateCommand();
        comando.CommandText = "SELECT Id,Nome,DataLancamento,Descricao FROM Filmes";
        var buscar = comando.ExecuteReader();
        Console.WriteLine("=====Lista de filmes=====");
        while (buscar.Read())
        {
            string descricao;
            if (buscar.IsDBNull(3)||buscar.GetString(3)=="")
            {
                descricao = "Sem descrição";
            }
            else
            {
                descricao = buscar.GetString(3);
            }
            Console.WriteLine("\nId: " + buscar.GetInt32(0) + " \nNome: " + buscar.GetString(1) + " \nData: " + buscar.GetString(2) + " \nDescrição: " + (descricao));
        }
    }

    static void AlterarFilme()
    {
        Console.WriteLine("Digite o id do filme a ser alterado");
        int id;
        try
        {
            id = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Parâmetro inválido passado como Id!");
            return;
        }
        if (id < 0)
        {
            Console.WriteLine("Parâmetro inválido passado como Id!");
            return;
        }

        Console.WriteLine("Digite o novo nome do filme");
        string nome = Console.ReadLine();
        Console.WriteLine("Digite a nova data de lançamento");
        string data = Console.ReadLine();
        Console.WriteLine("Digite uma descrição (opcional)");
        string descricao = Console.ReadLine();
        DateTime dataLancamento;
        try
        {
            dataLancamento = DateTime.Parse(data);
        }
        catch (FormatException)
        {
            Console.WriteLine("Data inválida, o filme não foi alterado!");
            return;
        }

        using var conexao = new SqliteConnection(diretorio);
        conexao.Open();
        var comando = conexao.CreateCommand();
        comando.CommandText = "UPDATE Filmes SET Nome = $nome, DataLancamento = $data, Descricao = $descricao WHERE Id = $id";
        comando.Parameters.AddWithValue("$id", id);
        comando.Parameters.AddWithValue("$nome", nome);
        comando.Parameters.AddWithValue("$data", data);
        comando.Parameters.AddWithValue("$descricao", descricao);
        comando.ExecuteNonQuery();
        Console.WriteLine("Filme alterado com sucesso!");
    }

    static void ProcurarFilme()
    {
        Console.WriteLine("Digite o id do filme");
        int id;
        try
        {
            id = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Parâmetro inválido passado como Id!");
            return;
        }
        if (id < 0)
        {
            Console.WriteLine("Parâmetro inválido passado como Id!");
            return;
        }
        using var conexao = new SqliteConnection(diretorio);
        conexao.Open();
        var comando = conexao.CreateCommand();
        comando.CommandText = "SELECT Id, Nome, Datalancamento , Descricao FROM Filmes WHERE Id = $id";
        comando.Parameters.AddWithValue("$id", id);
        var buscar = comando.ExecuteReader();

        if (buscar.Read())
        {
            int filmeId = buscar.GetInt32(0);
            string nome = buscar.GetString(1);
            string data = buscar.GetString(2);
            string descricao;
            if (buscar.IsDBNull(3) || buscar.GetString(3) == "")
            {
                descricao = "Sem descrição";
            }
            else
            {
                descricao = buscar.GetString(3);
            }
            Console.WriteLine($"Id: {filmeId}");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Data de Lançamento: {data}");
            Console.WriteLine($"Descrição: {descricao}");
        }
        else
        {
            Console.WriteLine("Filme não encontrado!");
        }
    }
}