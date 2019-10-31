# Áp dụng Template
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
# Đổ dữ liệu vào Razor View
1. Tạo struct lưu dữ liệu ở Models/ServicePrice.cs
    ```csharp
    public struct ServicePrice
    {        
        public string name;
        public int price;
        public ServicePrice(string name, int price)
        {
            this.name = name;
            this.price = price;
        }
    }
    ```
2. Thay đổi Controller/HomeController.cs hàm public IActionResult Index()
    ```csharp
    ViewData["mustache_trimming_prices"] = new[]{
                new ServicePrice("Red Butler trimming", 27),
                new ServicePrice("French trimming", 20),
                new ServicePrice("Vietnam trimming", 25),
                new ServicePrice("Holly Bad Boy", 25),
                new ServicePrice("Vintage Trimming", 23),
                new ServicePrice("1977 Styles", 33)
                };
    ```
3. Ở Views/Home/Index.cshml phần đầu thêm
    ```csharp
    var mustache_trimming_prices = (IEnumerable<ServicePrice>)ViewData["mustache_trimming_prices"];
    ```

    Đoạn dưới sử dụng @for để xuất ra
    ```
    <h3>PRICES FOR MUSTACHE TRIMMING</h3>
    @foreach (var service_item in mustache_trimming_prices)
    {
        <div class="menu-item my-4">
			<div class="row border-dot no-gutters">
				<div class="col-8 menu-item-name">
					<h6>@service_item.name</h6>
				</div>
				<div class="col-4 menu-item-price text-right">
					<h6>@service_item.price</h6>
				</div>
			</div>						
		</div>
    }
    ```

