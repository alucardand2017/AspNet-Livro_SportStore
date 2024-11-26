using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests;

public class HomeControllerTests 
{
   
 [Fact]
 public void Can_Use_Repository() {
 // Arrange
    Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
    mock.Setup(m => m.Products).Returns((new Product[] {
        new Product {ProductID = 1, Name = "P1"},
        new Product {ProductID = 2, Name = "P2"}
    }).AsQueryable<Product>());
    HomeController controller = new HomeController(mock.Object);
    
    // Act
    ProductsListViewModel result =
    controller.Index(null)?.ViewData.Model
    as ProductsListViewModel ?? new();
    
    // Assert
    Product[] prodArray = result.Products.ToArray();
    Assert.True(prodArray.Length == 2);
    Assert.Equal("P1", prodArray[0].Name);
    Assert.Equal("P2", prodArray[1].Name);
 }
    [Fact]
    public void Can_Send_Pagination_View_Model() {
        // Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] 
        {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
        }).AsQueryable<Product>());

        // Arrange
        HomeController controller =
        new HomeController(mock.Object) { PageSize = 3 };

        // Act
         ProductsListViewModel result =
            controller.Index(null, 2)?.ViewData.Model as
            ProductsListViewModel ?? new();

        // Assert
        PagingInfo pageInfo = result.PagingInfo;
        Assert.Equal(2, pageInfo.CurrentPage);
        Assert.Equal(3, pageInfo.ItemsPerPage);
        Assert.Equal(5, pageInfo.TotalItems);
        Assert.Equal(2, pageInfo.TotalPages);
    }
    [Fact]
    public void Can_Paginate() {        //cria um mock com a classe a ser utilizada
        //instancia os objetos que retornarão em forma de resultado
       Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] 
        {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
        }).AsQueryable<Product>());
        
        HomeController controller = new HomeController(mock.Object) { PageSize = 3 };  

        // Act
        // a ação busca um resultado que possui 2 possibilidades:
        // 1 - retorna um enumerável de Product ou
        // 2 - retorna um enumerável de Product vazio.
         ProductsListViewModel result =
           controller.Index(null, 2)?.ViewData.Model
           as ProductsListViewModel ?? new();  

        // Assert
        //3 testes - Array de tamanho 2 - Array[0] P4 - Array[1] P5
        Product[] prodArray = result.Products.ToArray();
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    
    }
    
    /// <summary>
    //  this test creates a mock repository containing Product objects that belong to a range of categories. 
    //one specific category is requested using the action method, and the results are checked to ensure that
    //the results are the right objects in the right order.
    /// </summary>
    [Fact]
    public void Can_Filter_Products(){
    //Arrange
    // - create the mock repository
    Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
    mock.Setup(m => m.Products).Returns((new Product[]{
        new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
        new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
        new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
        new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
        new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
    }).AsQueryable<Product>());

    HomeController controller = new HomeController(mock.Object);
    controller.PageSize = 3;

    //Action
    Product[] result = (controller.Index("Cat2",1)?.ViewData.Model
    as ProductsListViewModel ?? new()).Products.ToArray();

    //Assert
    Assert.Equal(2, result.Length);
    Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
    Assert.True(result[1].Name == "P4" && result[0].Category == "Cat2");
}



}
