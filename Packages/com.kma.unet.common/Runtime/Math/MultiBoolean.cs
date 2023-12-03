namespace UNet
{
    public readonly struct MultiBoolean
	{
        private readonly int Value;

		private MultiBoolean(int value)
		{
			Value = value < 0 ? 0 : value;
		}

        public static MultiBoolean operator ++(MultiBoolean a)
        {
            return new MultiBoolean(a.Value + 1);
        }

        public static MultiBoolean operator --(MultiBoolean a)
        {
            return new MultiBoolean(a.Value - 1);
        }

        public static implicit operator MultiBoolean(bool param)
        {
            return new MultiBoolean(param ? 1 : 0);
        }

        public static implicit operator bool(MultiBoolean param)
        {
            return param.Value > 0;
        }
        
        public override string ToString()
        {
            return $"{Value > 0}:{Value}";
        }
    }
}