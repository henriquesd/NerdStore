using System;

namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        // isso é necessário porque a gente precisa tratar as nossas entidades como uma coisa única;

        // para uma entidade ser igual a outra, ela vai ter que ser do mesmo tipo e ter o mesmo Id;
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        // pra não ter erro de gerar um HashCode; um dos fatores de comparação de classe é o HashCode, é como se fosse um código
        // exclusivo daquela classe, onde para que não haja chance de errar o HashCode por causa de por caso ser gerado igual, ele está
        // sendo multiplicado por um número aleatório (907) e somar com o HashCode do Id (que seria o Id Guid); então este valor vai ser único
        // para cada entidade, para que não haja chance de ao utilizar o HashCode de comparação, por um acaso conseguir o mesmo valor sem querer;
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            // caso precise de alguma maneira comparar, vai ter esta informação do nome da entidade com o Id;
            return $"{GetType().Name} [Id={Id}]";
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
