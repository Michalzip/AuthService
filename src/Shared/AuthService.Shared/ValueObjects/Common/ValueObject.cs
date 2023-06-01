namespace AuthService.Shared.Modules.ValueObjects.Common
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public bool Equals(ValueObject? other)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            //if two objects are null return true
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }
            }

            //if equals return true
            return obj1.Equals(obj2);
        }

        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return !(obj1 == obj2);
        }


        public override bool Equals(object obj)
        {
            //extends metod Equals to check type equality
            return obj != null && GetType() == obj.GetType();
        }
    }
}