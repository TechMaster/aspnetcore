using System;
using Dapper;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace dapper_console
{
    [Table("film")]
    public class Film
    {
        [Key]
        public int film_id { get; set; }
        public string title { get; set; }
        public int release_year { get; set; }

    }


    [Table("movie")]
    public class Movie
    {
        [Key]
        [IgnoreInsert]
        public int id { get; set; }
        public string name { get; set; }
        public int release_year { get; set; }
    }

    class Program
    {
        static readonly string _pgconnStr = "Server=localhost;Port=5432;Database=dvdrental;User Id=user;Password=abc123";
        static readonly string _sqlconnStr = "Server=localhost;Database=dvdrental;User Id=sa;Password=abc`123-";

        static void Main()
        {
            /*
            dynamic rows = await QueryFilmAsync();
            foreach (var item in rows)
            {
                Console.WriteLine($"{item.film_id}, {item.title}, {item.release_year}");
            }
            Console.WriteLine("Done");*/
            QueryFilmsSimpleCRUD();
            //QueryFimsContrib();



        }
        public static IDbConnection OpenPGConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public static IDbConnection OpenMSSQLConnection(string connStr)
        {
            var conn = new SqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public static async Task<IDbConnection> OpenConnectionAsync(string connStr)
        {
            using var conn = new NpgsqlConnection(connStr);
            await conn.OpenAsync();
            return conn;

        }

        public static void QueryFilm()
        {
            IList<Film> list;
            using IDbConnection conn = OpenPGConnection(_pgconnStr);
            var querySQL = @"SELECT film_id, title, release_year FROM public.film;";
            list = conn.Query<Film>(querySQL).AsList();

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"{item.film_id}, {item.title}, {item.release_year}");
                }
            }
            else
            {
                Console.WriteLine("the table is empty!");
            }
        }

        public static void QueryFilmDynamic()
        {
            using IDbConnection conn = OpenPGConnection(_pgconnStr);
            var querySQL = @"SELECT film_id, title, release_year FROM public.film;";
            dynamic rows = conn.Query(querySQL);
            foreach (var item in rows)
            {
                Console.WriteLine($"{item.film_id}, {item.title}, {item.release_year}");
            }

        }

        //Viết theo phong cách async - await
        public static async Task<dynamic> QueryFilmAsync()
        {
            using var conn = new NpgsqlConnection(_pgconnStr);
            await conn.OpenAsync();
            return conn.QueryAsync(@"SELECT film_id, title, release_year FROM public.film").Result;
        }
        //SimpleCRUD Postgresql
        public static void QueryFilmsSimpleCRUD()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL); // Đoạn lệnh này rất quan trọng
            using IDbConnection conn = OpenPGConnection(_pgconnStr);

            var films = conn.GetList<Film>("where title like '%heart%'");

            foreach (var item in films)
            {
                Console.WriteLine($"{item.film_id}, {item.title}, {item.release_year}");
            }
        }

        //SimpleCRUD SQLServer
        public static void QueryMovies()
        {

            using IDbConnection conn = OpenMSSQLConnection(_sqlconnStr);

            dynamic movies = conn.GetList<Movie>();
            foreach (var item in movies)
            {
                Console.WriteLine($"{item.id}, {item.name}, {item.release_year}");
            }
        }

        public static void QueryFimsContrib()
        {

            using var conn = OpenPGConnection(_pgconnStr);
            dynamic films = conn.GetAll<Film>();
            foreach (var item in films)
            {
                Console.WriteLine($"{item.film_id}, {item.title}, {item.release_year}");
            }

        }

    }
}