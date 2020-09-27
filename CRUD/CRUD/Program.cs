using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace CRUD
{
    internal static class Program
    {
        private static async Task Main()
        {
            await using (ApplicationContext db = new ApplicationContext())
            {
                User user1 = new User {Name = "Angarsky", Age = 23};
                User user2 = new User {Name = "Alice", Age = 26};
                // int n = 10000;
                // while (n != 0)
                // {
                //     User user1 = new User {Name = "Angarsky", Age = 23};
                //     await db.Users.AddAsync(user1);
                //     await db.SaveChangesAsync();
                //     
                //     n -= 1;
                //     Console.WriteLine($"{user1.Id}\n");
                // }
            
                await db.Users.AddAsync(user1);
                await db.Users.AddAsync(user2);
                await db.SaveChangesAsync();
            }
            
            await using (ApplicationContext db = new ApplicationContext())
            {
                List<User> users = await db.Users.ToListAsync();
                Console.WriteLine("Данные после добавления:");
            
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
                }
            }
            
            await using (ApplicationContext db = new ApplicationContext())
            {
                var user = await db.Users.FirstOrDefaultAsync();
                
                if (user != null)
                {
                    user.Name = "Bob";
                    user.Age = 44;
                    await db.SaveChangesAsync();
                }
                
                Console.WriteLine("\nДанные после редактирования:");
            
                List<User> users = await db.Users.ToListAsync();
                foreach (var u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
            
            await using (ApplicationContext db = new ApplicationContext())
            {
                User user = await db.Users.FirstOrDefaultAsync();
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                }
                
                Console.WriteLine("\nДанные после удаления:");
                
                List<User> users = await db.Users.ToListAsync();
                foreach (var u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }

            Console.ReadKey();
        }
    }
}