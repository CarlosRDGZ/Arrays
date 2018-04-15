namespace EstrcuturasConArreglos
{
    class Catalog
    {
        private Product[] _catalog;
        private readonly int Length;

        public Catalog(int numProducts)
        {
            _catalog = new Product[numProducts];
            Length = numProducts;
        }

        public void Add(Product product)
        {
            for (int i = Length - 1; i > 0; i--)
                _catalog[i] = _catalog[i - 1];
            _catalog[0] = product;
        }

        public Product Search(string code)
        {
            for(int i = 0; i < Length; i++)
                if (_catalog[i] != null)
                    if (_catalog[i].Code == code)
                        return _catalog[i];
            return null;
        }

        public void Delete(string code)
        {
            int index = 0;
            for (;index < Length; index++)
                if (_catalog[index] != null)
                    if (_catalog[index].Code == code)
                        break;

			if (index == Length)
				throw new System.IndexOutOfRangeException();

            for(;index < Length - 1; index++)
                _catalog[index] = _catalog[index + 1];
            _catalog[Length - 1] = null;
        }

        public void Insert(Product product, int pos)
        {
			if (pos < 0 || pos >= Length)
				throw new System.IndexOutOfRangeException();
            
            for (int i = Length - 1; i > pos + 1; i--){
                _catalog[i] = _catalog[i - 1];
                System.Console.WriteLine(i);
                System.Console.WriteLine(_catalog[i]);
            }
            _catalog[pos] = product;
        }

        public string List()
        {
            string str = "[" + System.Environment.NewLine;
            for (int i = 0; i < Length; i++)
                if (_catalog[i] != null)
                    str += (
                        "  "
                        + _catalog[i].ToString()
                        + (i < Length - 1 ? ", " : "")
                        + System.Environment.NewLine
                    );
            return str + "]";
        }
    }
}

/**
private static int IndexOfNull(object[] array)
{
    for (int i = 0; i < array.Length / 2; i++)
        if (array[i] == null)
            return i;
    return -1;
}
 */