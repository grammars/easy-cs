using System;

namespace EasyLib.Learn
{
    public class Animal
    {
        public virtual void Move()
        {
            Console.WriteLine("动物移动");
        }
    }

    public class Dog : Animal
    {
        public override void Move()
        {
            //base.Move();
            Console.WriteLine("狗移动");
        }
    }
}
