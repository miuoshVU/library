using Bogus;
using Library.API.Interface;
using Library.CodeFirstDatabase.Entities;
using Library.CodeFirstDatabase.Enum;
using Library.Entities;

namespace Library.Service
{
    public class LibrarySeeder : ILibrarySeeder
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly string loremIpsum = "Lorem ipsum dolor sit amet. Nam fuga harum rem illum fugit sed ipsum voluptatem et internos soluta sed voluptatem eveniet qui dignissimos repellendus. Aut dolores deserunt est suscipit obcaecati et dolorem sunt hic deleniti atque. </p><p>At  enim ut velit fugiat ut vitae obcaecati et Quis incidunt aut adipisci iste eum modi  ut suscipit optio. Vel reiciendis atque ab quam quae et neque? Ut sint consectetur sed deserunt sint qui repellendus perferendis. Aut consectetur doloribus cum voluptatibus nostrum sed dolorum exercitationem sed enim voluptate. </p><p>Et voluptas doloribus ab facere internos  quod explicabo quo delectus rerum aut galisum voluptas et tenetur reiciendis aut dolorem possimus. Qui facere autem hic quam aliquid qui officia aspernatur  officiis accusamus et nostrum nihil? Eum iste corrupti et galisum obcaecati ad provident maiores sit labore molestias.";
        public LibrarySeeder(LibraryDbContext dbContext, IUserService userService, IAuthenticationService authenticationService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _authenticationService = authenticationService;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Books.Any())
                {
                    var books = GetBooks();
                    _dbContext.Books.AddRange(books);

                    //_dbContext.SaveChanges();
                }
            }
        }

        public void BasicSeed()
        {
            var categories = new Category[]
            {
                new Category(){Name = "Informatyka", Cover = new Uri("https://bs-uploads.toptal.io/blackfish-uploads/components/blog_post_page/content/cover_image_file/cover_image/907886/retina_1708x683_cover-0219-The10MistakesC-Waldek_img-343f93efd75d26a7d857e374cd8630fd.png")},
                new Category(){Name = "Romans", Cover = new Uri("https://static01.helion.com.pl/global/okladki/vbig/e_12ox.jpg")},
                new Category(){Name = "Fantasy", Cover = new Uri("https://image.ceneostatic.pl/data/products/47722642/i-za-niebieskimi-drzwiami-wydanie-filmowe.jpg")}
            };

            var photos = new Uri[]
            {
                new Uri("https://www.granice.pl/okladka/250/374/e562fc181f5cc61b0049968298f538fd.jpeg"),
                new Uri("https://static01.helion.com.pl/global/okladki/326x466/gimpne.jpg"),
                new Uri("https://www.nowabasn.com/img/Ostatni%20jednoro%C5%BCec%20ok%C5%82adka.jpg"),
                new Uri("https://s-trojmiasto.pl/zdj/c/n/9/1973/719x0/1973367-Okladka-ksiazki.jpg"),
                new Uri("http://www.czytamwszedzie.pl/wp-content/uploads/2016/07/ma-%C3%A9y-ksiaze.jpg"),
                new Uri("https://static.polityka.pl/_resource/res/path/b5/5b/b55b77cb-6caa-4f51-823f-f0724017da03_830x830"),
                new Uri("https://s.znak.com.pl//files/covers/card/f1/Rosenbloom_Mrocznearchiwa_500pcx.jpg"),
                new Uri("https://ocdn.eu/pulscms-transforms/1/nzUk9kpTURBXy83NjBmZDc4OGZkMGI4ZDI0ZmZjMzk5ZDE0MGJiNzU5Mi5qcGealQLNAxQAwsOVAgDNAvjCw5QGzP_M_8z_lAbM_8z_zP-UBsz_zP_M_5QGzP_M_8z_lAbM_8z_zP-UBsz_zP_M_5QGzP_M_8z_lAbM_8z_zP-BoTAB"),
                new Uri("https://wydawnictwo.umcs.eu/uploads/product_images/03/032d8e55c16d00d75187b9aa7166632ac533f3be.jpg"),
            };

            var spots = new Spot[]
            {
                new Spot(){Name = "CBR-parter", Building = "CBR", Floor = 0 },
                new Spot(){Name = "Willa", Building = "Willa Maria", Floor = 1},
                new Spot(){Name = "CBR-kuchnia", Building = "CBR", Floor = 2},
            };

            var userGenerator = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Role, f => "user")
                .RuleFor(u => u.Avatar, f => new Uri("https://www.facebook.com/GooglePolska/"))
                .RuleFor(u => u.RemainingVotes, f => 5)
                .RuleFor(u => u.Password, f => _authenticationService.CreatePassword("1234"));

            var bookGenerator = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Commerce.Product())
                .RuleFor(b => b.Description, f =>
                                        loremIpsum.Substring(new Random().Next(loremIpsum.Length - 401),
                                        new Random().Next(399)))
                .RuleFor(b => b.Cover, f => photos[new Random().Next(photos.Length)]);
         
            var authorGenerator = new Faker<Author>()
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName());

            var bookInstanceGenerator = new Faker<BookInstance>()
                .RuleFor(i => i.Status, f => Status.Available)
                .RuleFor(i => i.OwnerName, f => f.Name.FullName());

            var proposedBookGenerator = new Faker<ProposedBook>()
                .RuleFor(p => p.Cover, f => new Uri("https://lexliber.pl/userdata/public/gfx/15444/default.jpg"))
                .RuleFor(p => p.Title, f => f.Commerce.ProductMaterial())
                .RuleFor(p => p.UrlLink, f => new Uri("https://www.taniaksiazka.pl/ksiazka/outsider-stephen-king"))
                .RuleFor(p => p.Points, f => 1);
           
            if (_dbContext.Database.CanConnect())
            {
                _dbContext.Passwords.RemoveRange(_dbContext.Passwords.ToList());
                _dbContext.Users.RemoveRange(_dbContext.Users.ToList());
                var users = userGenerator.Generate(10);
                var admin = userGenerator.Generate();
                admin.Role = "admin";
                admin.Email = "admin@admin.pl";
                users.Add(admin);
                _dbContext.Users.AddRange(users);

                _dbContext.Authors.RemoveRange(_dbContext.Authors.ToList());
                var authors = authorGenerator.Generate(30);
                _dbContext.Authors.AddRange(authors);

                _dbContext.Books.RemoveRange(_dbContext.Books.ToList());
                var books = bookGenerator.Generate(20);
                foreach(var book in books)
                {
                    var cat = new List<Category>() {categories[new Random().Next(categories.Length)]};
                    book.Categories = cat;

                    var auth = authors[new Random().Next(authors.Count)];
                    book.Authors.Add(auth);
                }
                _dbContext.AddRange(books);

                _dbContext.Categories.RemoveRange(_dbContext.Categories.ToList());
                _dbContext.Categories.AddRange(categories);

                _dbContext.Spots.RemoveRange(_dbContext.Spots.ToList());
                _dbContext.Spots.AddRange(spots);

                _dbContext.Book_Instances.RemoveRange(_dbContext.Book_Instances.ToList());
                var bi = bookInstanceGenerator.Generate(30);
                foreach (var instance in bi)
                {
                    instance.Book = books[new Random().Next(books.Count)];
                    instance.Spot = spots[new Random().Next(spots.Length)];
                }
                _dbContext.Book_Instances.AddRange(bi);

                _dbContext.Proposed_Books.RemoveRange(_dbContext.Proposed_Books.ToList());
                var pb = proposedBookGenerator.Generate(15);
                foreach (var book in pb)
                {
                    book.Authors = "Jakiś Autor, Inny Autor";
                    book.Categories = "Jakaś kategoria, Inna kategoria";
                }
                _dbContext.Proposed_Books.AddRange(pb);

                _dbContext.SaveChanges();
            }
        }

        private IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>()
            {
            new Book()
            {
                Title = "Head First C#. 4th Edition",
                ISBN = "9781491976708",
                Description = "Dive into C# and create apps, user interfaces, games, " +
                              "and more using this fun and highly visual introduction to C#, " +
                              ".NET Core, and Visual Studio. With this completely updated guide, ",
                              //"which covers C# 8.0 and Visual Studio 2019, beginning programmers " +
                              //"like you will build a fully functional game in the opening chapter. " +
                              //"Then you'll learn how to use classes and object-oriented programming, " +
                              //"create 3D games in Unity, and query data with LINQ. " +
                              //"And you'll do it all by solving puzzles, doing hands-on exercises, " +
                              //"and building real-world applications. By the time you're done, you'll be a solid " +
                              //"C# programmer--and you'll have a great time along the way!",
                Publisher = "O'Reilly Media",
                Authors = new List<Author>()
                {
                    new Author()
                    {
                        FirstName = "Andrew",
                        LastName = "Stellman"

                    }
                },
                Cover= new Uri("https://bs-uploads.toptal.io/blackfish-uploads/components/blog_post_page/content/cover_image_file/cover_image/907886/retina_1708x683_cover-0219-The10MistakesC-Waldek_img-343f93efd75d26a7d857e374cd8630fd.png"),
                Categories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Informatyka",
                        Cover = new Uri ("https://bs-uploads.toptal.io/blackfish-uploads/components/blog_post_page/content/cover_image_file/cover_image/907886/retina_1708x683_cover-0219-The10MistakesC-Waldek_img-343f93efd75d26a7d857e374cd8630fd.png")
                    }
                },
                BookInstances = new List<BookInstance>()
                {
                    new BookInstance()
                    {

                        Status = Status.Available,
                        Spot = new Spot()
                        {
                            Building = "Villa",
                            Floor = 1,
                            Name = "CristalShelf",

                        },
                        OwnerName ="Julia Fox",
                        Borrows = new List<Borrow>()
                        {
                            new Borrow()
                            {
                                //BorrowDate = DateTime.Now,
                                //ReturnDate = DateOnly(02,09,2022),
                                User = new User()
                                {
                                    FirstName = "Adam",
                                    LastName = "Kowalski",
                                    Email = "test@test.pl",
                                    Role = "admin",
                                    RemainingVotes = 5,
                                    Avatar = null,
                                    Password = _authenticationService.CreatePassword("1234")
                                }
                            }
                        }

                    }
                }

            }};
            return books;

        }
    }
}
