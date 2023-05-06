using ClothingAppDB.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace ClothingAppDB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                // Eager Loading
                var allProductsOfBrand = context.Products.Where(p => p.BrandID == 1);

                var brandsWithNumberProducts = context.Brands
                    .Include(b => b.Products).GroupBy(b => b.Name)
                    .Select(group => new { BrandName = group.Key, Count = group.Count() })
                    .OrderByDescending(group => group.Count);

                var sectionCategoryProducts = context.SectionCategories
                    .Where(sc => sc.CategoryID == 2 && sc.SectionID == 1)
                    .Include(sc => sc.Products)
                    .Single();

                var completedOrdersOfProduct = context.CustomerOrders
                    .Where(co => co.CurrentStatus == Status.Completed)
                    .Include(co => co.OrderProducts
                        .Where(op => op.ProductID == 1))
                    .ThenInclude(op => op.Product)
                    .Include(co => co.User);

                var allReviewOfProduct = context.Reviews
                    .Where(r => r.ProductID == 1)
                    .Include(r => r.User);
                
                foreach (var product in allProductsOfBrand)
                {
                    Console.WriteLine(
                        $"id: {product.ID},\nname: {product.Name},\ndescription: {product.Description},\nprice: {product.Price},\nadd_date:{product.AddDate},\nsection_category_id: {product.SectionCategoryID},\nquantity: {product.Quantity},\nimage: {product.ImageURL}\n");
                }
                foreach (var brand in brandsWithNumberProducts)
                {
                    Console.WriteLine($"brand_name: {brand.BrandName}, products_count: {brand.Count}\n");
                }
                foreach (var product in sectionCategoryProducts.Products)
                {
                    Console.WriteLine(
                            $"id: {product.ID},\nname: {product.Name},\ndescription: {product.Description},\nprice: {product.Price},\nadd_date:{product.AddDate},\nsection_category_id: {product.SectionCategoryID},\nquantity: {product.Quantity},\nimage: {product.ImageURL}\n");
                }
                foreach (var order in completedOrdersOfProduct)
                {
                    foreach (var prod in order.OrderProducts)
                    {
                        Console.WriteLine(
                            $"order_id: {order.ID},\nuser_id: {order.User.ID},\nfirst_name: {order.User.FirstName},\nlast_name: {order.User.LastName},\nproduct_name:{prod.Product.Name},\ncurrent_status: {order.CurrentStatus},\norder_date: {order.OrderDate}\n");
                    }
                }
                
                foreach (var review in allReviewOfProduct)
                {
                    Console.WriteLine(
                        $"rating: {review.Rating},\ncomment: {review.Comment},\nphone: {review.User.Phone},\nemail: {review.User.Email},\nrole:{review.User.Role},\nfirst_name: {review.User.FirstName},\nlast_name: {review.User.LastName}\n");
                }
            }
        }
    }
}