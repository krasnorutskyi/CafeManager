using CafeManager.Core.Entities;
using CafeManager.Infrastructure.EF;

namespace CafeManager.Infrastructure.DataInitializer;

public class DbInitializer
{
    public void Initialize(ApplicationContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var salad = new Category() {Name = "Salads"};
        var mainCourse = new Category() {Name = "Main course"};
        var starter = new Category() {Name = "Starters & Appetizers"};
        var firstCourse = new Category() {Name = "First course"};
        var pasta = new Category() {Name = "Pasta"};
        var breakfast =new Category() {Name = "Breakfast"};
        var sideDish = new Category() {Name = "Side dish"};
        var pizza = new Category() {Name = "Pizzas"};
        var dessert = new Category() {Name = "Desserts"};
        var hotBeverages = new Category() {Name = "Hot beverages"};
        var coldBeverages = new Category() {Name = "Cold beverages"};
        var alcohol = new Category() {Name = "Alcohol"};

        var categories = new List<Category>()
        {
            salad, mainCourse, starter, firstCourse, pasta, breakfast, sideDish, pizza, dessert, hotBeverages,
            coldBeverages, alcohol
        };

        foreach (var category in categories)
        {
            context.Categories.Add(category);
        }

        context.SaveChanges();

        var gram = new Unit() {Name = "Grams"};
        var pint = new Unit() {Name = "Pint"};
        var dozen = new Unit() {Name = "Dozens"};
        var kilogram = new Unit() {Name = "Kilograms"};
        var liter = new Unit() {Name = "Liter"};
        var mililiter = new Unit() {Name = "Mililiters"};
        var portion = new Unit() {Name = "Portion"};
        var ounce = new Unit() {Name = "Ounces"};

        var units = new List<Unit>()
        {
            gram, pint, dozen, kilogram, liter, mililiter, portion, ounce
        };

        foreach (var unit in units)
        {
            context.Units.Add(unit);
        }

        context.SaveChanges();

        var cabbage = new Product()
        {
            Name = "Cabbage", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var potato = new Product()
        {
            Name = "Potato", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var tomato = new Product()
        {
            Name = "Tomato", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var apple = new Product()
        {
            Name = "Apple", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var carrot = new Product()
        {
            Name = "Carrot", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var noodles = new Product()
        {
            Name = "Noodles", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var pork = new Product()
        {
            Name = "Pork", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var beef = new Product()
        {
            Name = "Beef", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var mutton = new Product()
        {
            Name = "Mutton", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var chicken = new Product()
        {
            Name = "Chicken", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var redBeet = new Product()
        {
            Name = "Red beet", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var milk = new Product()
        {
            Name = "Milk", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var cream = new Product()
        {
            Name = "Cream", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var fish = new Product()
        {
            Name = "Fish", Price = 0, Quantity = 20,
            BestBefore = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31)
        };

        var products = new List<Product>()
        {
            cabbage, potato, tomato, apple, carrot, noodles, pork, beef,
            mutton, chicken, redBeet, milk, cream, fish
        };

        foreach (var product in products)
        {
            context.Products.Add(product);
        }

        
        context.SaveChanges();


        var w1 = new Waiter() {FirstName = "John", LastName = "Gold"};
        var w2 = new Waiter() {FirstName = "Ein", LastName = "Rend"};
        var w3 = new Waiter() {FirstName = "Jack", LastName = "Sparrow"};
        var w4 = new Waiter() {FirstName = "Maria", LastName = "Remark"};
        var w5 = new Waiter() {FirstName = "Lionel", LastName = "Messi"};

        var waiters = new List<Waiter>()
        {
            w1, w2, w3, w4, w5
        };

        foreach (var waiter in waiters)
        {
            context.Waiters.Add(waiter);
        }

        context.SaveChanges();
        
        var tables = new List<Table>()
        {
            new Table() {PlacesNumber = 2, Waiter = waiters[0]},
            new Table() {PlacesNumber = 2, Waiter = waiters[1]},
            new Table() {PlacesNumber = 2, Waiter = waiters[2]},
            new Table() {PlacesNumber = 4, Waiter = waiters[3]},
            new Table() {PlacesNumber = 4, Waiter = waiters[4]},
            new Table() {PlacesNumber = 5, Waiter = waiters[0]},
            new Table() {PlacesNumber = 5, Waiter = waiters[1]},
            new Table() {PlacesNumber = 3, Waiter = waiters[2]},
            new Table() {PlacesNumber = 3, Waiter = waiters[3]},
            new Table() {PlacesNumber = 3, Waiter = waiters[4]},
            new Table() {PlacesNumber = 3, Waiter = waiters[0]},
            new Table() {PlacesNumber = 6, Waiter = waiters[1]},
            new Table() {PlacesNumber = 7, Waiter = waiters[2]},
            new Table() {PlacesNumber = 7, Waiter = waiters[2]},
            new Table() {PlacesNumber = 5, Waiter = waiters[3]},
            new Table() {PlacesNumber = 5, Waiter = waiters[3]},
            new Table() {PlacesNumber = 4, Waiter = waiters[4]}
        };

        foreach (var table in tables)
        {
            context.Tables.Add(table);
        }

        context.SaveChanges();


        var o1 = new Order()
            {Date = DateTime.Today, Waiter = w1, Price = 100, Tipp = 20, HasClientsSale = true, VAT = 20};


    }
}