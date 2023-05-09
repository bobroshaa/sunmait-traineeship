using ClothingAppDB.Entities;
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
                    .GroupJoin(
                        context.Products,
                        b => b.ID,
                        p => p.BrandID,
                        (b, p) => new { BrandID = b.ID, Count = b.Products.Count() }
                    ).OrderByDescending(group => group.Count);
                var sectionCategoryProducts = context.Products
                    .Include(p => p.SectionCategory)
                    .Where(p => p.SectionCategory.CategoryID == 2 && p.SectionCategory.SectionID == 1);

                var completedOrdersOfProduct = context.OrderProducts
                    .Include(op => op.Order)
                    .ThenInclude(o => o.User)
                    .Include(op => op.Product)
                    .Where(op => op.Order.CurrentStatus == Status.Completed && op.ProductID == 1);

                var allReviewOfProduct = context.Reviews
                    .Where(r => r.ProductID == 1)
                    .Include(r => r.User);

                Console.WriteLine("Eager Loading:\n");
                foreach (var product in allProductsOfBrand)
                {
                    Console.WriteLine(
                        $"id: {product.ID},\nname: {product.Name},\ndescription: {product.Description},\nprice: {product.Price},\nadd_date:{product.AddDate},\nsection_category_id: {product.SectionCategoryID},\nquantity: {product.Quantity},\nimage: {product.ImageURL}\n");
                }

                foreach (var brand in brandsWithNumberProducts)
                {
                    Console.WriteLine($"brand_name: {brand.BrandID}, products_count: {brand.Count}\n");
                }

                foreach (var product in sectionCategoryProducts)
                {
                    Console.WriteLine(
                        $"id: {product.ID},\nname: {product.Name},\ndescription: {product.Description},\nprice: {product.Price},\nadd_date:{product.AddDate},\nsection_category_id: {product.SectionCategoryID},\nquantity: {product.Quantity},\nimage: {product.ImageURL}\n");
                }

                foreach (var op in completedOrdersOfProduct)
                {
                    Console.WriteLine(
                        $"order_id: {op.Order.ID},\nuser_id: {op.Order.User.ID},\nfirst_name: {op.Order.User.FirstName},\nlast_name: {op.Order.User.LastName},\nproduct_name:{op.Product.Name},\ncurrent_status: {op.Order.CurrentStatus},\norder_date: {op.Order.OrderDate}\n");
                }

                foreach (var review in allReviewOfProduct)
                {
                    Console.WriteLine(
                        $"rating: {review.Rating},\ncomment: {review.Comment},\nphone: {review.User.Phone},\nemail: {review.User.Email},\nrole:{review.User.Role},\nfirst_name: {review.User.FirstName},\nlast_name: {review.User.LastName}\n");
                }

                // Lazy loading
                var allProductsOfBrand1 = context.Products.Where(p => p.BrandID == 1);
                var brandsWithNumberProducts1 = context.Brands
                    .GroupJoin(
                        context.Products,
                        b => b.ID,
                        p => p.BrandID,
                        (b, p) => new { BrandID = b.ID, Count = b.Products.Count() }
                    );
                var sectionCategoryProducts1 = context.SectionCategories
                    .Single(sc => sc.CategoryID == 2 && sc.SectionID == 1);
                var completedOrdersOfProduct1 = context.OrderProducts
                    .Where(op => op.Order.CurrentStatus == Status.Completed && op.ProductID == 1);
                var allReviewOfProduct1 = context.Reviews
                    .Where(r => r.ProductID == 1);

                Console.WriteLine("------------------------------------\nLazy Loading:\n");
                foreach (var product in allProductsOfBrand1)
                {
                    Console.WriteLine(
                        $"id: {product.ID},\nname: {product.Name},\ndescription: {product.Description},\nprice: {product.Price},\nadd_date:{product.AddDate},\nsection_category_id: {product.SectionCategoryID},\nquantity: {product.Quantity},\nimage: {product.ImageURL}\n");
                }

                foreach (var brand in brandsWithNumberProducts1)
                {
                    Console.WriteLine($"brand_name: {brand.BrandID}, products_count: {brand.Count}\n");
                }

                foreach (var product in sectionCategoryProducts1.Products)
                {
                    Console.WriteLine(
                        $"id: {product.ID},\nname: {product.Name},\ndescription: {product.Description},\nprice: {product.Price},\nadd_date:{product.AddDate},\nsection_category_id: {product.SectionCategoryID},\nquantity: {product.Quantity},\nimage: {product.ImageURL}\n");
                }

                foreach (var op in completedOrdersOfProduct1)
                {
                    Console.WriteLine(
                        $"order_id: {op.Order.ID},\nuser_id: {op.Order.User.ID},\nfirst_name: {op.Order.User.FirstName},\nlast_name: {op.Order.User.LastName},\nproduct_name:{op.Product.Name},\ncurrent_status: {op.Order.CurrentStatus},\norder_date: {op.Order.OrderDate}\n");
                }

                foreach (var review in allReviewOfProduct1)
                {
                    Console.WriteLine(
                        $"rating: {review.Rating},\ncomment: {review.Comment},\nphone: {review.User.Phone},\nemail: {review.User.Email},\nrole:{review.User.Role},\nfirst_name: {review.User.FirstName},\nlast_name: {review.User.LastName}\n");
                }
            }

            var updatedBrand = new Brand() { ID = 4, Name = "Bershka" };

            using (var context = new Context())
            {
                context.Entry(updatedBrand).State = EntityState.Modified;
                context.SaveChanges();

                Console.WriteLine(context.Brands.Single(p => p.ID == 4).Name);
            }
        }
    }
}