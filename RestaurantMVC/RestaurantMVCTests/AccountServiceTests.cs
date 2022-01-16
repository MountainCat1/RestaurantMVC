using RestaurantMVC.Services;
using RestaurantMVC.Data;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using RestaurantMVC;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantMVC.Models;
using System.Collections.Generic;

namespace RestaurantMVCTests
{
    public class AccountServiceTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("aaavvvaaavvvaaaaavavavavavavavavavavavav")]
        [InlineData("young_leosia")]
        public void UsernameValidation_ForInvalidData_ReturnsFalse(string username)
        {
            // Act
            bool result = AccountService.IsUsernameValid(username);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Andrzej")]
        [InlineData("Kuba")]
        [InlineData("UwU")]
        public void UsernameValidation_ForInvalidData_ReturnsTrue(string username)
        {
            // Act
            bool result = AccountService.IsUsernameValid(username);

            // Assert
            Assert.True(result);
        }
    }

    public class ProductServiceTests
    {
        public static IEnumerable<object[]> TestSearchPersonItemsDataOk =>
        new List<object[]>
        {
            new object[] { new ProductDto { Name = "Og�rki", Price = -22 } },
            new object[] { new ProductDto { Name = "Kie�basa", Price = 0 } },
            new object[] { new ProductDto { Name = "", Price = -22 } }
        };

        [Theory]
        [MemberData(nameof(TestSearchPersonItemsDataOk))]
        public void ProductValidation_ForInvalidData_ReturnFalse(ProductDto productDto)
        {
            // Arrage


            // Act
            bool result = ProductService.IsProductValid(productDto);

            // Assert
            Assert.False(result);
        }


        public static IEnumerable<object[]> TestSearchPersonItemsDataNotOk =>
        new List<object[]>
        {
            new object[] { new ProductDto { Name = "Og�rki", Price = 1055 } },
            new object[] { new ProductDto { Name = "Kie�basa", Price = 32 } },
            new object[] { new ProductDto { Name = "Par�wki", Price = 22 } }
        };

        [Theory]
        [MemberData(nameof(TestSearchPersonItemsDataNotOk))]
        public void ProductValidation_ForInvalidData_ReturnTrue(ProductDto productDto)
        {
            // Arrage

            // Act
            bool result = ProductService.IsProductValid(productDto);

            // Assert
            Assert.True(result);
        }
    }
}

