namespace Tokens
{
    /// <summary>
    /// I am quite sure that I can solve this problem with built-in c# language constructs, 
    /// but more visitor practice is good for me.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TokenTypeVisitor<T> : ITokenVisitor where T : AbstractToken
    {
        public static bool IsType<T>(AbstractToken token) where T : AbstractToken
        {
            var visitor = new TokenTypeVisitor<T>();
            token.Accept(visitor);

            // if this errors out we have a concrete type that is not yet added to our visitor interface
            return visitor.result.Value;
        }

        public static T? TryGet<T>(AbstractToken token) where T : AbstractToken
        {
            return IsType<T>(token)? (T) token : null;
        }
        
        public bool? result = null;  
        public void Visit(NumberToken token)
        {
            result = token is T;
        }

        public void Visit(OpToken token)
        {
            result = token is T;
        }

        public void Visit(WhitespaceToken token)
        {
            result = token is T;
        }

        public void Visit(VariableToken token)
        {
            result = token is T;
        }
    }



}
