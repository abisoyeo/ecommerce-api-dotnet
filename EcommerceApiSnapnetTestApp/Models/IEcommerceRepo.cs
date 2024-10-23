namespace EcommerceApiSnapnetTestApp.Models
{
    public interface IEcommerceRepo
    {
        public Task<ProductsModel> Update(ProductsModel productChanges); 
        public Task<ProductsModel> GetProduct(int Id);
        public Task<IEnumerable<ProductsModel>> GetAllProducts();
        public Task<ProductsModel> Delete(int id);
        public Task<ProductsModel> Add(ProductsDTO product);

    }
}
