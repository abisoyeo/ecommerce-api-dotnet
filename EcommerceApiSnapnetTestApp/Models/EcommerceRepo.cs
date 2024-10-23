using Microsoft.EntityFrameworkCore;
using System;

namespace EcommerceApiSnapnetTestApp.Models
{
    public class EcommerceRepo:IEcommerceRepo
    {
        private readonly ApplicationDbContext context;

        public EcommerceRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task <ProductsModel> Add(ProductsDTO product)
        {
            ProductsModel productsModel = new ProductsModel()
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl

            };

            context.Products.Add(productsModel);
            context.SaveChanges();

            return productsModel;
        }

        public async Task<ProductsModel> Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public async Task<IEnumerable<ProductsModel>> GetAllProducts()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<ProductsModel> GetProduct(int Id)
        {
            return context.Products.Find(Id);
        }

        public async Task<ProductsModel> Update(ProductsModel productChanges)
        {
            context.Products.Update(productChanges);
            context.SaveChanges();

            return productChanges;
        }
    }
}
