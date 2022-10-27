using System.ComponentModel;

namespace Domain.Enums
{
    public enum ECustomerType
    {
        [Description("Affiliate of the store")]
        Affilate = 1,
        [Description("Employee of the store")]
        Employee = 2,
        
    }
    public enum EDiscountType
    {
        [Description("Affiliate of the store")]
        Affilate = 1,
        [Description("Employee of the store")]
        Employee = 2,
        [Description("Long Term Customer(greater than 2 years)")]
        LongTermUser = 3,
        [Description("Discount on all $100 bill)")]
        ProductPrice = 4,

    }
    public enum EPruductType
    {
        [Description("Groceries")]
        Groceries = 1,
        [Description("Others")]
        Others = 2,
       

    }
}
