namespace ExamProjectWeb.Views.Shared
{
    public class GlobalMethods
    {
        public static string PriceCheck(decimal? price)
        {
            string result = string.Empty;

            if (price >= 1000 || price <= -1000)
                result = $"{price:0,0}";
            else if (price >= 100 && price < 1000 || price <= -100 && price > -1000)
                result = $"{price:0,0.0}";
            else if ((price >= 10 && price < 100) || (price <= -10 && price > -100))
                result = $"{price:0.00}";
            else if ((price > 1 && price < 10) || (price < -1 && price > -10))
                result = $"{price:0.000}";
            else if ((price < 1 && price >= (decimal)0.001) || (price > -1 && price <= (decimal)-0.001))
                result = $"{price:0.000}";
            else if (price < (decimal)0.001 && price > 0)
                result = price.ToString();
            else
                result = price.ToString();

            return result;
        }
    }
}
