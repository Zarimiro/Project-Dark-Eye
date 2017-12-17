namespace Assets.Scripts
{
    public static class NumberGenerator
    {
        public static int CurrentPosition;

        public const string Key = "1234232132431232134231134223242331241313142314231421";

        public static int GetNextNumber()
        {
            var currentNum = Key.Substring(CurrentPosition++ % Key.Length, 1);
            return int.Parse(currentNum);
        }
    }
}

