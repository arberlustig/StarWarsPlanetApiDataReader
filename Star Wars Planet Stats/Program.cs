//Person validPerson = new Person("John", 1981);
//Person invalidPerson = new Person("R");

//var validator = new Validator();
//validator.Validate(validPerson);
//Console.WriteLine(validator.Validate(invalidPerson));
//Console.ReadKey();

public class Dog
{

    [StringLengthValidate(2, 25)] public string Name { get; }

    public Dog(string name) => Name = name;

}


public class Person
{
    [StringLengthValidate(2, 10)] public string Name { get; }
    public int YearOfBirth { get; }

    public Person(string name, int yearOfBirth)
    {
        Name = name;
        YearOfBirth = yearOfBirth;
    }
    public Person(string name) => Name = name;
}

public class Validator
{
    public bool Validate(object obj)
    {

        //man schreibt typeof, weil man auf generische Strukturen einer Klasse zugreifen möchte
        Type type = obj.GetType();
        var propertiesToValidate = type
            .GetProperties()
        .Where(property => Attribute.IsDefined(property, typeof(StringLengthValidateAttribute)));


        foreach (var property in propertiesToValidate)
        {
            object? propertyValue = property.GetValue(obj);
            if (propertyValue is not string)
            {
                throw new Exception("Input is not a string");

            }

            var value = (string)propertyValue;
            var attribute = (StringLengthValidateAttribute)property.GetCustomAttributes(typeof(StringLengthValidateAttribute), true).First();


            if (value.Length < attribute.Min || value.Length > attribute.Max)
            {
                Console.WriteLine($"Property {property.Name} is invalid." + " " + $"Value is {value}");
                return false;
            }

        }
        return true;
    }

}


[AttributeUsage(AttributeTargets.Property)]
class StringLengthValidateAttribute : Attribute
{
    public int Min { get; }
    public int Max { get; }
    public StringLengthValidateAttribute(int min, int max)
    {
        Min = min;
        Max = max;
    }
}