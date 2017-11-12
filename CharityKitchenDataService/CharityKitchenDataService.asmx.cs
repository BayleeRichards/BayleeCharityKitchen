using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CharityKitchenDataService.Managers;
using CharityKitchenDataService.Models;

namespace CharityKitchenDataService
{
    /// <summary>
    /// Summary description for CharityKitchenDataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CharityKitchenDataService : System.Web.Services.WebService
    {
        #region Account Authentication

        //[WebMethod]
        //public void CreateMasterUser()
        //{
        //    AuthenticationManager.CreateMasterUser();
        //}

        [WebMethod]
        public bool ValidateCredentials(string username, string password)
        {
            try
            {
                return AuthenticationManager.ValidateCredentials(username, password);
            }
            catch
            {
                return false;
            }
        }

        [WebMethod]
        public UserLogin ValidateLoginCredentials(string username, string password)
        {
            try
            {
                return AuthenticationManager.ValidateLoginCredentials(username, password);
            }
            catch
            {
                return new UserLogin();
            }
        }

        public string HashPassword(string password)
        {
            try
            {
                return AuthenticationManager.HashPassword(password);
            }
            catch
            {
                return string.Empty;
            }
        }

        [WebMethod]
        public UserLogin GetLoggedInUserByUsername(string username)
        {
            try
            {
                return AuthenticationManager.GetLoggedInUserByUsername(username);
            }
            catch
            {
                return new UserLogin();
            }
        }

        #endregion Account Authenication

        #region meals

        [WebMethod]
        public List<Meal> GetAllMeals()
        {
            try
            {
                return MealManager.GetAllMeals();
            }
            catch
            {
                return new List<Meal>();
            }
        }

        [WebMethod]
        public Meal GetOneMeal(int id)
        {
            try
            {
                return MealManager.GetOneMeal(id);
            }
            catch
            {
                return new Meal();
            }
        }

        [WebMethod]
        public int InsertNewMeal(Meal meal)
        {
            try
            {
                return MealManager.InsertNewMeal(meal);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public int UpdateExistingMeal(Meal meal)
        {
            try
            {
                return MealManager.UpdateExistingMeal(meal);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public List<MealDisplay> GetMealsToDisplay()
        {
            try
            {
                return MealManager.GetMealsToDisplay();
            }
            catch
            {
                return new List<MealDisplay>();
            }
        }

        #endregion meals

        #region ingredients

        [WebMethod]
        public List<Ingredient> GetAllIngredients()
        {
            try
            {
                return IngredientManager.GetAllIngredients();
            }
            catch
            {
                return new List<Ingredient>();
            }
        }

        [WebMethod]
        public Ingredient GetOneIngredient(int id)
        {
            try
            {
                return IngredientManager.GetOneIngredient(id);
            }
            catch
            {
                return new Ingredient();
            }
        }

        [WebMethod]
        public int InsertNewIngredient(Ingredient ingredient)
        {
            try
            {
                return IngredientManager.InsertNewIngredient(ingredient);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public int UpdateExistingIngredient(Ingredient ingredient)
        {
            try
            {
                return IngredientManager.UpdateExistingIngredient(ingredient);
            }
            catch
            {
                return 0;
            }
        }

        #endregion ingredients

        #region mealIngredients

        [WebMethod]
        public List<MealIngredient> GetIngredientsForMeal(int mealID)
        {
            try
            {
                return MealIngredientManager.GetIngredientsForMeal(mealID);
            }
            catch
            {
                return new List<MealIngredient>();
            }
        }

        [WebMethod]
        public int InsertMealIngredient(int mealID, int IngredientID, int quantity, decimal price)
        {
            try
            {
                return MealIngredientManager.InsertMealIngredient(mealID, IngredientID, quantity, price);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public int DeleteMealIngredient(int mealIngredientID)
        {
            try
            {
                return MealIngredientManager.DeleteMealIngredient(mealIngredientID);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public decimal GetTotalForMealIngredients(int mealID)
        {
            try
            {
                return MealIngredientManager.GetTotalForMeal(mealID);
            }
            catch
            {
                return 0m;
            }
        }

        #endregion mealIngredients

        #region orders

        [WebMethod]
        public List<Order> GetAllOrders()
        {
            try
            {
                return OrderManager.GetAllOrders();
            }
            catch
            {
                return new List<Order>();
            }
        }

        [WebMethod]
        public Order GetOneOrder(int id)
        {
            try
            {
                return OrderManager.GetOneOrder(id);
            }
            catch
            {
                return new Order();
            }
        }

        [WebMethod]
        public int InsertNewOrder(Order order)
        {
            try
            {
                return OrderManager.InsertNewOrder(order);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public int UpdateExistingOrder(Order order)
        {
            try
            {
                return OrderManager.UpdateExistingOrder(order);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public List<OrderDisplay> GetOrdersToDisplay()
        {
            try
            {
                return OrderManager.GetOrdersToDisplay();
            }
            catch
            {
                return new List<OrderDisplay>();
            }
        }

        #endregion orders

        #region orderMeals

        [WebMethod]
        public List<OrderMeal> GetMealsForOrder(int orderID)
        {
            try
            {
                return OrderMealManager.GetMealsForOrder(orderID);
            }
            catch
            {
                return new List<OrderMeal>();
            }
        }

        [WebMethod]
        public int InsertOrderMeal(int orderID, int mealID, int quantity, decimal price)
        {
            try
            {
                return OrderMealManager.InsertOrderMeal(orderID, mealID, quantity, price);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public int DeleteOrderMeal(int orderMealID)
        {
            try
            {
                return OrderMealManager.DeleteOrderMeal(orderMealID);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public decimal GetTotalForOrderMeals(int orderID)
        {
            try
            {
                return OrderMealManager.GetTotalForOrder(orderID);
            }
            catch
            {
                return 0m;
            }
        }

        #endregion orderMeals

        #region orderIngredients

        [WebMethod]
        public List<OrderIngredient> GetIngredientsForOrder(int orderID)
        {
            try
            {
                return OrderIngredientManager.GetIngredientsForOrder(orderID);
            }
            catch(Exception ex)
            {
                return new List<OrderIngredient>();
            }
        }

        #endregion orderIngredients

        #region users

        [WebMethod]
        public List<User> GetAllUsers()
        {
            try
            {
                return UserManager.GetAllUsers();
            }
            catch
            {
                return new List<User>();
            }
        }

        [WebMethod]
        public User GetOneUser(int id)
        {
            try
            {
                return UserManager.GetOneUser(id);
            }
            catch
            {
                return new User();
            }
        }

        [WebMethod]
        public User GetOneUserByUsername(string username)
        {
            try
            {
                return UserManager.GetOneUserByUsername(username);
            }
            catch
            {
                return new User();
            }
        }

        [WebMethod]
        public int InsertNewUser(User user)
        {
            try
            {
                return UserManager.InsertNewUser(user);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public int UpdateExistingUser(User user)
        {
            try
            {
                return UserManager.UpdateExistingUser(user);
            }
            catch
            {
                return 0;
            }
        }

        #endregion users

        #region roles

        [WebMethod]
        public List<Role> GetAllRoles()
        {
            try
            {
                return RoleManager.GetAllRoles();
            }
            catch
            {
                return new List<Role>();
            }
        }

        [WebMethod]
        public Role GetOneRole(int id)
        {
            try
            {
                return RoleManager.GetOneRole(id);
            }
            catch
            {
                return new Role();
            }
        }

        [WebMethod]
        public int InsertNewRole(Role role)
        {
            try
            {
                return RoleManager.InsertNewRole(role);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public int UpdateExistingRole(Role role)
        {
            try
            {
                return RoleManager.UpdateExistingRole(role);
            }
            catch
            {
                return 0;
            }
        }

        #endregion roles

        #region userRoles

        [WebMethod]
        public List<UserRole> GetRolesForDisplay()
        {
            try
            {
                return UserRoleManager.GetRolesForDisplay();
            }
            catch
            {
                return new List<UserRole>();
            }
        }

        [WebMethod]
        public List<UserRole> GetRolesForUser(int userID)
        {
            try
            {
                return UserRoleManager.GetRolesForUser(userID);
            }
            catch
            {
                return new List<UserRole>();
            }
        }

        [WebMethod]
        public int InsertUserRole(int userID, int roleID)
        {
            try
            {
                return UserRoleManager.InsertUserRole(userID, roleID);
            }
            catch
            {
                return 0;
            }
        }

        [WebMethod]
        public int DeleteUserRole(int userRoleID)
        {
            try
            {
                return UserRoleManager.DeleteUserRole(userRoleID);
            }
            catch
            {
                return 0;
            }
        }

        #endregion userRoles

        [WebMethod]
        public bool IsAlphabetical(string text)
        {
            try
            {
                return AuthenticationManager.IsAlphabetical(text);
            }
            catch
            {
                return false;
            }
        }

        [WebMethod]
        public bool IsNumeric(string text)
        {
            try
            {
                return AuthenticationManager.IsNumeric(text);
            }
            catch
            {
                return false;
            }
        }
    }
}
