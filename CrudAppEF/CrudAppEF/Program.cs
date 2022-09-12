using CrudAppEF.DAL;
using CrudAppEF.RepositoryPattern;
using System.Linq.Expressions;

#region MyRegion
Repository<Contact, CrudAppDbContext> repository = new(new CrudAppDbContext());

AdoNetRepository<Contact> netRepository = new AdoNetRepository<Contact>(new Microsoft.Data.SqlClient.SqlConnection("server=CATALYST\\SQLEXPRESS;Database=CrudApp;Trusted_Connection=True"));

//Contact contact = new Contact()
//{
//    FirstName = "Resul",
//    LastName = "Rustemov",
//    Phone = "0704574746",
//    Email = "yashar@gmail.com"
//};

//netRepository.Add(contact);

IEnumerable<Contact> contacts = netRepository.Search(c => c.LastName.Contains("yash"));

foreach (Contact con in contacts)
{
    Console.WriteLine($"{con.FirstName}");
}

#endregion


//Category<Contact> _category = new Category<Contact>();
//_category.Test(x => x.FirstName != "Yashar");
//class Category<T>
//{
//    public void Test(Expression<Func<T, bool>> func)
//    {
//        if (func.Body is BinaryExpression ex)
//        {
//            Console.WriteLine("Parameter class: " + ((MemberExpression)ex.Left).Member.Name);
//            Console.WriteLine("Value: " + ex.Right);
//        }
//        else if (func.Body is MethodCallExpression methodEx)
//        {
//            Console.WriteLine("Value class: " + methodEx.Arguments[0].ToString().Trim('"'));
//            Console.WriteLine(((MemberExpression)methodEx.Object).Member.Name);
//        }
//    }
//}




