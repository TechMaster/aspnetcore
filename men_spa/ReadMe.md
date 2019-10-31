# Các bước thực hiện
1. Tải free template ở đây [Men Spa](https://demo.w3layouts.com/demos_new/template_demo/10-08-2019/men_spa-demo_Free/1574744496/web/index.html)
2. Tạo các Views gồm có about, contact, gallery, services, single
    Các view này đều có chung một template do đó hãy chú ý đến 2 file:
        . Views/Shared/_Layouts.cshtml
        . Views/_ViewStart.cshtml
3. Copy các static resources css, fonts, images vào thư mục wwwroot
4. Sửa Controllers/HomeControllers.cs thêm các Action methods
    ```csharp
        public IActionResult About()
        {
            return View();
        }
    ```