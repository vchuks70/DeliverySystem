using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Helper.Request;
using Domain.Helper.Response;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IProductsAndServiceInterface ProductsAndServiceImplementation { get; set; }

        public AdminController(IProductsAndServiceInterface productsAndServiceImplementation)
        {
            ProductsAndServiceImplementation = productsAndServiceImplementation;
        }

        [HttpPost]
        [Route("AddProductAndService")]
        public async Task<IActionResult> AddProductAndService([FromBody] AddProductAndService model)
        {
            var result = await ProductsAndServiceImplementation.AddProductAndService(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("AddProductCategory")]
        public async Task<IActionResult> AddProductCategory([FromBody] AddProductCategory model)
        {
            var result = await ProductsAndServiceImplementation.AddProductCategory(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("GetAllProductsServices")]
        public async Task<ActionResult<IEnumerable<ProductAndService>>> GetAllProductSerices()
        {
            var ListOfProducts = await ProductsAndServiceImplementation.GetAllProductSerices();
            return ListOfProducts.Any() ? Ok(ListOfProducts) : NoContent();
        }


        [HttpGet]
        [Route("GetSingleProductService/{productId}")]
        public async Task<ActionResult<ProductAndService>> GetSingleProductSerice([FromRoute] int productId)
        {
            var SingleProduct = await ProductsAndServiceImplementation.GetSingleProductSerice(productId);
            return SingleProduct != null ? Ok(SingleProduct) : NotFound();
        }

        [HttpGet]
        [Route("GetAllProductCategories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllProductCategories()
        {
            var ListOfProductsCategories = await ProductsAndServiceImplementation.GetAllProductCategories();
            return ListOfProductsCategories.Any() ? Ok(ListOfProductsCategories) : NoContent();
        }

        [HttpGet]
        [Route("GetSingleProductCategory{productCategoryId}")]
        public async Task<ActionResult<ProductCategory>> GetSingleProductCategory([FromRoute] int productCategoryId)
        {
            var SingleProductCategory = await ProductsAndServiceImplementation.GetSingleProductCategory(productCategoryId);
            return SingleProductCategory != null ? Ok(SingleProductCategory) : NotFound();
        }

        [HttpGet]
        [Route("GetProductServiceByCategory{productCategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductAndService>>> GetProductSericesByCategory([FromRoute] int productCategoryId)
        {
            var ProductServiceByCategory = await ProductsAndServiceImplementation.GetProductSericesByCategory(productCategoryId);
            return ProductServiceByCategory.Any() ? Ok(ProductServiceByCategory) : NoContent();
        }

        [HttpPut]
        [Route("EditProductAndService")]
        public async Task<IActionResult> EditProductAndService([FromBody] EditProductAndService model)
        {
            var EditProductAndService = await ProductsAndServiceImplementation.EditProductAndService(model);
            return EditProductAndService.Status == true ? Ok(EditProductAndService) : BadRequest(EditProductAndService);
        }

        [HttpPost]
        [Route("UpadeProductAndServicePrice")]
        public async Task<IActionResult> UpdateProductPrice([FromBody] UpdateProductPrice model)
        {
            var UpdatedResult = await ProductsAndServiceImplementation.UpdateProductPrice(model);
            return UpdatedResult.Status == true ? Ok(UpdatedResult) : BadRequest(UpdatedResult);
        }
    }
}


