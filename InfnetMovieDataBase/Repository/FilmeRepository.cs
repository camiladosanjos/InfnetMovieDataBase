using InfnetMovieDataBase.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InfnetMovieDataBase.Repository
{
    public class FilmeRepository
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InfnetMovieDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Listar Filmes
        public IEnumerable<Filme> ListarFilmes()
        {
            var filmes = new List<Filme>();

            using var connection = new SqlConnection(connectionString);

            var cmdText = "SELECT * FROM Filme";
            var select = new SqlCommand(cmdText, connection);

            try
            {
                connection.Open();

                using var reader = select.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    var filme = new Filme();

                    filme.Id = (int)reader["Id"];
                    filme.Titulo = reader["Titulo"].ToString();
                    filme.TituloOriginal = reader["TituloOriginal"].ToString();
                    filme.Ano = (int)reader["Ano"];

                    filmes.Add(filme);
                }

            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {

            }

            return filmes;
        }

        //Criar Filme
        public void CriarFilme(Filme filme)
        {
            using var connection = new SqlConnection(connectionString);

            var sp = "CriarFilme";
            var insert = new SqlCommand(sp, connection);
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("@Titulo", filme.Titulo);
            insert.Parameters.AddWithValue("@TituloOriginal", filme.TituloOriginal);
            insert.Parameters.AddWithValue("@Ano", filme.Ano);

            try
            {
                connection.Open();
                insert.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {

            }
        }

        //Atualizar Filme
        public void AtualizarFilme(Filme filme)
        {
            using var connection = new SqlConnection(connectionString);

            var cmdText = "UPDATE Filme SET Titulo=@Titulo, TituloOriginal=@TituloOriginal, Ano=@Ano WHERE Id=@Id";
            var update = new SqlCommand(cmdText, connection);

            update.Parameters.AddWithValue("@Id", filme.Id);
            update.Parameters.AddWithValue("@Titulo", filme.Titulo);
            update.Parameters.AddWithValue("@TituloOriginal", filme.TituloOriginal);
            update.Parameters.AddWithValue("@Ano", filme.Ano);

            try
            {
                connection.Open();
                update.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {

            }
        }

        //Detalhamento 
        public Filme DetalharFilme(int id)
        {
            using var connection = new SqlConnection(connectionString);
            string sql = "SELECT Id, Titulo, TituloOriginal, Ano FROM Filme WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            Filme filme = null;

            try
            {
                connection.Open();

                using var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    filme = new Filme();
                    filme.Id = (int)reader["Id"];
                    filme.Titulo = reader["Titulo"].ToString();
                    filme.TituloOriginal = reader["TituloOriginal"].ToString();
                    filme.Ano = (int)reader["Ano"];

                }

            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {

            }

            return filme;

        }

        //Excluir
        public void ExcluirFilme(int id)
        {
            using var connection = new SqlConnection(connectionString);

            var cmdText = "DELETE Filme WHERE Id=@Id";
            var delete = new SqlCommand(cmdText, connection);

            delete.Parameters.AddWithValue("@Id", id);
           
            try
            {
                connection.Open();
                delete.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {

            }
        }

        public IEnumerable<Pessoa> ListarElenco(int id)
        {
            var elenco = new List<Pessoa>();

            using var connection = new SqlConnection(connectionString);
            var sp = "ListarAtoresPorFilme";
            
            var sqlCommand = new SqlCommand(sp, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@FilmeId", id);

            try
            {
                connection.Open();
                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var pessoa = new Pessoa()
                    {
                        Nome = reader["Nome"].ToString(),
                        Sobrenome = reader["Sobrenome"].ToString()
                    };
                    elenco.Add(pessoa);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }
            return elenco;
        }
    }
}
