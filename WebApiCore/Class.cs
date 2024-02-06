using System;

namespace Bridge.Structural
{
    /// <summary>
    /// Bridge Design Pattern
    /// </summary>

    public class Program
    {
        public static void Main(string[] args)
        {
            Abstraction ab = new RefinedAbstraction();  //instanciamos la capa de abstracción del objeto para poder instanciar diferentes tipos
            
            // Set implementation and call

            ab.Implementor = new ConcreteImplementorA();// instanciamos el A
            ab.Operation();                             //ejecutamos el codigo asociado a A

            // Change implemention and call

            ab.Implementor = new ConcreteImplementorB();// instaciamos el B
            ab.Operation();                             // ejecutamos el codigo asociado al B
           

            // Wait for user

            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Abstraction' class
    /// </summary>

    public class Abstraction
    {
        protected Implementor implementor; //la abstracción debe contener el tipo implementor para ejecutar los diferentes codigos A/B

        public Implementor Implementor
        {
            set { implementor = value; }
        }

        public virtual void Operation()   //virtual permite realizar override del metodo o propiedad en la nueva instancia
        {
            implementor.Operation();
        }
    }

    /// <summary>
    /// The 'Implementor' abstract class
    /// </summary>

    public abstract class Implementor    //clase abstracta para hacer de base en la definición de otras clases
    {
        public abstract void Operation(); //abstract en una propiedad o metodo permite una redefinición del mismo
    }

    /// <summary>
    /// The 'RefinedAbstraction' class
    /// </summary>

    public class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction() {
            //podriamos ejecutar codigo diferente a la capa de abtracción
            }
        public override void Operation()
        {
            implementor.Operation();
        }
    }

    /// <summary>
    /// The 'ConcreteImplementorA' class
    /// </summary>

    public class ConcreteImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("Ejecuta codigo A");
        }
    }

    /// <summary>
    /// The 'ConcreteImplementorB' class
    /// </summary>

    public class ConcreteImplementorB : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("Ejecuta codigo B");
        }

    }
}
