using System;
using Dapper;
using Npgsql;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dapper_console
{

    public class Film
    {
        public int film_id;
        public string title;
        public int release_year;

    }
    class Program
    {
        static string _connStr = "Server=localhost;Port=5432;Database=dvdrental;User Id=user;Password=abc123";
        static async Task Main()
        {
            dynamic rows = await QueryFilmAsync();
            foreach (var item in rows)
            {
                Console.WriteLine($"{item.film_id}, {item.title}, {item.release_year}");
            }
            Console.WriteLine("Done");
        }

        public static IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
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
            using IDbConnection conn = OpenConnection(_connStr);
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
            using IDbConnection conn = OpenConnection(_connStr);
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
            using var conn = new NpgsqlConnection(_connStr);
            await conn.OpenAsync();
            return conn.QueryAsync(@"SELECT film_id, title, release_year FROM public.film").Result;

        }
    }
}