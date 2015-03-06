namespace Ya.Tests.Doubles
{
    public class BaseFoo 
    { 
    }

    public class Foo : BaseFoo, IFoo
    {
        public int Number { get; set; }
        public string Text { get; set; }
    }

    public class FooNumber1 : Foo
    {
        public FooNumber1() 
        {
            Number = 1;
        }
    }

    public class FooWithPrivateConstructor : IFoo
    {
        public int Number { get; set; }
    }

    public interface IFoo<T> 
    {
        T Generic { get; set; }
        int Number { get; set; }
    }

    public class Foo<T> : IFoo<T>
    {
        public T Generic { get; set; }
        public int Number { get; set; }
    }

    public class InheritedFromGenerics : Foo<string>
    { 
    }

    public interface IFoo
    {
        int Number { get; set; }
    }

    public class AlternativeFoo : IFoo
    {
        public int Number { get; set; }
    }

    public class WhereFoo<T> : IGeneric<T> where T : IFoo 
    {
        public T Member { get; set; }
    }

    public class WhereNonFoo<T> : IGeneric<T> where T : NonFoo
    {
        public T Member { get; set; }
    }

    public class NonFoo 
    { 
    }
}
