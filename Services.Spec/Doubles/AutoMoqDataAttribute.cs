using AutoFixture;
using AutoFixture.AutoMoq;

using AutoFixture.Xunit2;

public class AutoMoqDataAttributeWithPositiveDecimalsAttribute : AutoDataAttribute
{
    public AutoMoqDataAttributeWithPositiveDecimalsAttribute()
        : base(() => new Fixture().Customize(new AutoMoqCustomization()))
    // fixture.Customizations.Add(new PositiveDecimalBuilder());           
    // return fixture;
    // })
    { }
}

// public class PositiveDecimalBuilder : ISpecimenBuilder
// {
//     public object Create(object request, ISpecimenContext context)
//     {
//         if (request is Type type && type == typeof(decimal))
//         {
//             return context.Resolve(typeof(decimal)) switch
//             {
//                 decimal value when value > 0 => value,
//                 _ => context.Resolve(typeof(decimal)),
//             };
//         }

//         return new NoSpecimen();
//     }
// }