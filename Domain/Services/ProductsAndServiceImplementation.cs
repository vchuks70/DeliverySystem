using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Helper.Response;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class ProductsAndServiceImplementation : IProductsAndServiceInterface
    {
        private readonly ApplicationDbContext _db;

        public ProductsAndServiceImplementation(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GlobalResponse> AddProductAndService(AddProductAndService model)
        {
            var Product = await _db.ProductAndServices.FirstOrDefaultAsync(x => x.Name == model.Name);
                if (Product != null) 
            {
                return new GlobalResponse { Status = false, Message = "Name Already Exist" };
            }
            var Category = await _db.ProductCategories.FirstOrDefaultAsync(x => x.Id == model.ProductCategoryId);
            if (Category == null)
            {
                return new GlobalResponse { Status = false, Message = "Category does not exist" };
            }
            var NewProduct = new ProductAndService
            {
                Name = model.Name,
                Price = model.Price,
                DiscountPercentage = model.DiscountPercentage,
                ProductCategoryName = Category,
                InventoryCount = model.InventoryCount,
            };
            await _db.ProductAndServices.AddAsync(NewProduct);
            return new GlobalResponse { Status = true, Message = "New Product Created" };
        }

        public async Task<GlobalResponse> AddProductCategory(AddProductCategory model)
        {
            var Category = await _db.ProductCategories.FirstOrDefaultAsync(x => x.CategoryName==model.CategoryName);
            if (Category != null)
                return new GlobalResponse { Status = false, Message = "Category Already Exist" };
            var NewCategoryName = new ProductCategory{
            CategoryName = model.CategoryName
            };
            await _db.ProductCategories.AddAsync(NewCategoryName);
            await _db.SaveChangesAsync();
            return new GlobalResponse { Status = true, Message = "New Category Created" };
        }

        public async Task<GlobalResponse> EditProductAndService(EditProductAndService model)
        {
            var product = await _db.ProductAndServices.FirstOrDefaultAsync(x => x.Id == model.ProductAndServiceId);
            if (product == null)
                return new GlobalResponse { Status = false, Message = "Product does not exist" };
            var category = await _db.ProductCategories.FirstOrDefaultAsync(x => x.Id == model.ProductCategoryId);
            if (product == null)
                 return new GlobalResponse { Status = false, Message = "Category does not exist" };
            product.Name = model.Name;
            product.Price = model.Price;
            product.DiscountPercentage = model.DiscountPercentage;
            product.ProductCategoryName = category;
            product.InventoryCount = model.InventoryCount;
            await _db.SaveChangesAsync();
            return new GlobalResponse { Status = true, Message = "Product updated" };

        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategories()
        {
            var ListOfProductCategory = await _db.ProductCategories.ToListAsync();
            return ListOfProductCategory;
        }

        public async Task<IEnumerable<ProductAndService>> GetAllProductSerices()
        {
            var ListOfProducts = await _db.ProductAndServices.Include(x=>x.ProductCategoryName).ToListAsync();
            return ListOfProducts;
        }

        public async Task<IEnumerable<ProductAndService>> GetProductSericesByCategory(int productCategoryId)
        {
            var products = await _db.ProductAndServices.Where(x => x.ProductCategoryName.Id == productCategoryId).ToListAsync();
            return products;
        }

        public async Task<ProductCategory> GetSingleProductCategory(int productCategoryId)
        {
            var SingleProduct = await _db.ProductCategories.SingleOrDefaultAsync(x=>x.Id == productCategoryId);

            return SingleProduct;
        }

        public async Task<ProductAndService> GetSingleProductSerice(int productId)
        {
            var SingleProduct = await _db.ProductAndServices.SingleOrDefaultAsync(x => x.Id == productId);
            return SingleProduct;
        }

        public async Task<GlobalResponse> UpdateProductPrice(UpdateProductPrice model)
        {
            var product = await _db.ProductAndServices.FirstOrDefaultAsync(x => x.Id == model.ProductId);
            if (product == null)
            {
                return new GlobalResponse { Status = false, Message = "Product not found" };
            }
            product.Price = model.Price;
            await _db.SaveChangesAsync();
            return new GlobalResponse { Status = true, Message = "Product updated" };
        }
    }
        
}
