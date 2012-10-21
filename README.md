This is a generic object builder framework for .NET. It enables you to write code like:

    Person person = EasyBuilder.BuildA<Person>().Set(y => y.FirstName, "John").Set(y => y.LastName, "Doe");

If  you don't want to be implicit of what you do you could use the following syntax: 

    var person = EasyBuilder.BuildA<Person>().Set(y => y.FirstName, "John").Set(y => y.LastName, "Doe").Build();

The second example show more that you are working with a fluent api that is working on a context. All the assignemnts are queued and assigned during the build time of the objects.