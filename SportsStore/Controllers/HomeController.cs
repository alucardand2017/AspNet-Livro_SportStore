using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;
public class HomeController: Controller 
{
    private IStoreRepository repository;
    public int PageSize = 3; //instancias por página

    public HomeController(IStoreRepository repository)
    {
        this.repository = repository;
    }
    public ViewResult Index(string? category, int productPage = 1)
        => View(new ProductsListViewModel 
        {
            Products = repository.Products
                .Where(p=> category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            
            PagingInfo = new PagingInfo {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = repository.Products.Count()
                },
            CurrentCategory = category
        }
    );
}

          