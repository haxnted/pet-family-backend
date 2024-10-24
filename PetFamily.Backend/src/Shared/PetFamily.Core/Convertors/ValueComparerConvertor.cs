using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PetFamily.Core.Convertors;

public static class ValueComparerConvertor
{
    public static ValueComparer<List<T>> CreateValueComparer<T>() =>
        new(
            (c1, c2) => c1!.SequenceEqual(c2!), 
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())), 
            c => c.ToList()); 
}
