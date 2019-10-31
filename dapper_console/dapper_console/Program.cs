using System;
using Dapper;
using Npgsql;
using System.Data;
using System.Collections.Generic;

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
        static string _connStr = "Server=192.168.31.155;Port=5432;Database=dvdrental;User Id=user;Password = abc123";
        static void Main(string[] args)
        {
            PrintData2();
        }

        public static IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public static void PrintData()
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

        public static void PrintData2()
        {
            using IDbConnection conn = OpenConnection(_connStr);
            var querySQL = @"SELECT film_id, title, release_year FROM public.film;";
            dynamic rows = conn.Query(querySQL);
            foreach (var item in rows)
            {
                Console.WriteLine($"{item.film_id}, {item.title}, {item.release_year}");
            }

        }
    }
}