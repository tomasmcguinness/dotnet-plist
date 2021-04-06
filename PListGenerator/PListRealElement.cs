namespace PListFormatter
{
    public class PListRealElement : PListElement
    {
        public PListRealElement(string key, float value)
            : base(key, "real", value)
        { }
    }
}
