namespace Ya.Tests.Doubles
{
    abstract class ParentInjectableObject
    {
        [Inject]
        public Foo ParentMember { get; set; }

        [Inject]
        public static Foo ParentStaticMember { get; set; }

    }

    class InjectableObject : ParentInjectableObject
    {
        [Inject]
        public Foo Member { get; private set; }

        [Inject]
        private Foo PrivateMember { get; set; }

        [Inject]
        public static Foo StaticMember { get; set; }

        public Foo GetPrivateMember()
        {
            return PrivateMember;
        }

        [Inject]
        public Foo Member1 { get; private set; }

        [Inject]
        public Foo Member2 { get; private set; }

    }

    class InjectableObject2
    {
        [Inject]
        public IFoo Member { get; private set; }
    }

    class InjectableObjectOf<T>
    {
        [Inject]
        public T Member { get; set; }
    }

}
