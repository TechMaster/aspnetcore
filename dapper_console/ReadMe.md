Dapper Console
# Cài đặt Nuget Plugins

1. npgsql
2. dapper


# Chuẩn bị cơ sở dữ liệu

Chạy những lệnh docker sau đây để cài đặt Postgresql và pgadmin
```docker
docker run -d -p 5432:5432 --name postgres -e POSTGRES_PASSWORD=abc postgres:alpine
docker run -p 8001:80 --name=pgadmin -e PGADMIN_DEFAULT_EMAIL=cuong@techmaster.vn  -e PGADMIN_DEFAULT_PASSWORD=abc123 -d dpage/pgadmin4:latest
```


# Kết nối bằng npgsql
Tham khảo connection string ở đây https://www.connectionstrings.com/npgsql/
Tham khảo sử dụng Dapper https://www.c-sharpcorner.com/article/getting-started-with-postgresql-using-dapper-in-net-core/
```
Server=192.168.31.155;Port=5432;Database=myDataBase;User Id=myUsername;
Password=myPassword;
```

Chú ý trên MacOSX, nếu expost port của Postgresql ra host, chúng ta vẫn không thể truy cập bằng localhost

# Truy vấn dữ liệu
```csharp
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
```

