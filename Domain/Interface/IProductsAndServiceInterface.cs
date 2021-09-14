using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Helper.Response;

namespace Domain.Interface
{
   public interface IProductsAndServiceInterface    
    {
        Task<GlobalResponse> AddProductAndService(AddProductAndService model);
        Task<GlobalResponse> AddProductCategory(AddProductCategory model);

        Task<IEnumerable<ProductAndService>> GetAllProductSerices();
        Task<ProductAndService> GetSingleProductSerice(int productId);
        Task<IEnumerable<ProductCategory>> GetAllProductCategories();
        Task<ProductCategory> GetSingleProductCategory(int productCategoryId);
        Task<IEnumerable<ProductAndService>> GetProductSericesByCategory(int productCategoryId);
        Task<GlobalResponse> EditProductAndService(EditProductAndService model);
        Task<GlobalResponse> UpdateProductPrice(UpdateProductPrice model);

    }
}
