namespace Tokens
{
    public struct TryCreateTokenResult
    {
        public AbstractToken token;
        public int movedStringPointerBy;
        public string newString;

        public TryCreateTokenResult(AbstractToken token, string s)
        {
            this.token = token;
            
            movedStringPointerBy = token?.rawText.Length ?? 0;
            if (movedStringPointerBy == s.Length)
            {
                newString = "";
            }
            else if (movedStringPointerBy >= 0)
            {
                newString = s.Substring(movedStringPointerBy);
            }
            else
            {
                newString = s;
            }
        }
    }
}
