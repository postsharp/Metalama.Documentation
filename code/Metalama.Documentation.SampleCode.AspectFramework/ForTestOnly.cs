﻿using System;

namespace Doc.ForTestOnly
{

    public class MyService
    {
        // Normal constructor.
        public MyService() : this( DateTime.Now )
        {
        }

        [ForTestOnly]
        internal MyService( DateTime dateTime )
        {
        }
    }

    internal class NormalClass
    {
        // Usage NOT allowed here because we are not in a Tests namespace.
        private MyService service = new(DateTime.Now.AddDays( 1 ));
    }

    namespace Tests
    {

        internal class TestClass
        {
            // Usage allowed here because we are in a Tests namespace.
            private MyService service = new( DateTime.Now.AddDays( 2 ) );
        }
    }
}