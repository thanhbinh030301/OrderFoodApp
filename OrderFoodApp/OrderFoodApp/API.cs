using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderFoodApp
{
    public class API
    {

        FirebaseClient firebase = new FirebaseClient("https://orderfood-database-default-rtdb.asia-southeast1.firebasedatabase.app/");

        public async Task<List<Item>> GetAllItem()
        {

            return (await firebase
              .Child("Food")
              .OnceAsync<Item>()).Select(item => new Item
              {
                  Id=item.Object.Id,
                  Name = item.Object.Name,
                  Img = item.Object.Img,
                  Descr = item.Object.Descr,
                  Price = item.Object.Price,
                  Favourite = item.Object.Favourite,
                  MenuId = item.Object.MenuId
              }).ToList();
        }
        public async Task<List<Item>> GetItems(string category)
        {
            int temp = 0;
            switch (category)
            {
                case "Đồ ăn":
                    temp = 1;
                    break;
                case "Đồ uống":
                    temp = 3;
                    break;
                case "Bánh ngọt":
                    temp = 2;
                    break;
            }

            return (await firebase
              .Child("Food")
              .OnceAsync<Item>()).Where(a => a.Object.MenuId == temp.ToString()).Select(item => new Item
              {
                  Id = item.Object.Id,
                  Name = item.Object.Name,
                  Img = item.Object.Img,
                  Descr = item.Object.Descr,
                  Price = item.Object.Price,
                  Favourite = item.Object.Favourite,
                  MenuId = item.Object.MenuId
              }).ToList();
        }

        public async Task<List<Item>> GetType()
        {
            var Item = (await firebase.Child("Food").OnceAsync<Item>()).Where(a => a.Object.Type == "2").Select(item => new Item
            {
                Name = item.Object.Name,
                Img = item.Object.Img,
                Descr = item.Object.Descr,
                Price = item.Object.Price,
                Favourite = item.Object.Favourite,
                MenuId = item.Object.MenuId
            }).ToList();

            return Item;
        }
        public async Task<List<Item>> GetFavouriteItem()
        {
            var FavouriteItem = (await firebase.Child("Food").OnceAsync<Item>()).Where(a => a.Object.Favourite == true).Select(item => new Item
            {
                Name = item.Object.Name,
                Img = item.Object.Img,
                Descr = item.Object.Descr,
                Price = item.Object.Price,
                Favourite = item.Object.Favourite,
                MenuId = item.Object.MenuId
            }).ToList(); 
 
            return FavouriteItem;
        }
        public async Task<bool> RegisterUser(string email, string username, string password)
        {
            var result = await firebase
                .Child("User")
                .PostAsync(new User() { UserId = Guid.NewGuid(), Email=email, UserName = username, Password = password });

            if (result.Object != null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public async Task<bool> LoginUser(string username, string password)
        {
            var GetPerson = (await firebase
              .Child("User")
              .OnceAsync<User>()).Where(a => a.Object.UserName == username).Where(b => b.Object.Password == password).FirstOrDefault();

            if (GetPerson != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddToCart(Item item, int count)
        {
            var result = await firebase
                .Child("Cart")
                .PostAsync(new Cart() { Id = item.Id, Name = item.Name, Number = count, Price = item.Price*count, Img = item.Img });

            if (result.Object != null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> AddToOrder(Cart cart)
        {
            var result = await firebase
                .Child("Order")
                .PostAsync(new Order() { Name = cart.Name, Number = cart.Number, Price = cart.Price, Img = cart.Img, Date = DateTime.Now });

            if (result.Object != null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public async Task<List<Cart>> GetCartItem()
        {
            return (await firebase
              .Child("Cart")
              .OnceAsync<Cart>()).Select(item => new Cart
              {
                  Name = item.Object.Name,
                  Number = item.Object.Number,
                  Img = item.Object.Img,
                  Price = item.Object.Price
              }).ToList();
        }

        public async Task<List<Order>> GetOrderItem()
        {
            return (await firebase
              .Child("Order")
              .OnceAsync<Order>()).Select(item => new Order
              {
                  Name = item.Object.Name,
                  Number = item.Object.Number,
                  Img = item.Object.Img,
                  Price = item.Object.Price,
                  Date = item.Object.Date
              }).ToList();
        }

        public async Task DeleteCart(string name)
        {
            var toDeleteCart = (await firebase
              .Child("Cart")
              .OnceAsync<Cart>()).Where(a => a.Object.Name == name).FirstOrDefault();
            await firebase.Child("Cart").Child(toDeleteCart.Key).DeleteAsync();
        }
        public async Task UpdateCart(Item item)
        {
            var toUpdateItem = (await firebase
              .Child("Food")
              .OnceAsync<Item>()).Where(a => a.Object.Name == item.Name).FirstOrDefault();

            if (item.Favourite == false)
            {
                await firebase
              .Child("Food")
              .Child(toUpdateItem.Key)
              .PutAsync(new Item() { Descr = item.Descr, Name = item.Name, Img = item.Img, MenuId = item.MenuId, Price = item.Price, Favourite = true });
            }
            else
            {
                if (item.Favourite == true)
                {
                    await firebase
                                 .Child("Food")
                                 .Child(toUpdateItem.Key)
                                 .PutAsync(new Item() { Descr = item.Descr, Name = item.Name, Img = item.Img, MenuId = item.MenuId, Price = item.Price, Favourite = false });
                }
            }
            
        }
    }
 
}

