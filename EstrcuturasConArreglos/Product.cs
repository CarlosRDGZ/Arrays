using System;
namespace EstrcuturasConArreglos
{
    class Product
    {
		private static readonly Random _random = new Random(DateTime.Now.Millisecond);

		public readonly string Code;

        public string Name { get; set; }

        public string Description { get; set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { if (value >= 0) _quantity = value; }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
            set { if (value >= 0) _price = value; }
        }

        public Product(string name, string description, int quantity, int price)
        {
            DateTime now = DateTime.Now;
            Code = name.Substring(0, 3)
                       + (now.Month < 10 ? _random.Next(10).ToString() + now.Month : now.Month.ToString())
                       + now.Millisecond
                       + (now.Day > 9 ? now.Day.ToString() : _random.Next(10).ToString() + now.Day)
                       + (now.Second > 9 ? now.Second.ToString() : _random.Next(10).ToString() + now.Second)
                       + now.Year
                       + (now.Minute > 9 ? now.Minute.ToString() : _random.Next(10).ToString() + now.Minute);
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString()
        {
            System.Reflection.PropertyInfo[] props =
                      this.GetType().GetProperties();
            string[] strs = new string[props.Length];
            for (int i = 0; i < props.Length; i++)
                strs[i] = props[i].Name + ": " + props[i].GetValue(this, null);

            return "{ " + string.Join(", ", strs) + " }";
        }
    }
}
