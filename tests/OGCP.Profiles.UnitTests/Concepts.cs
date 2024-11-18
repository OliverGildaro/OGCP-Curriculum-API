namespace OGCP.Profiles.UnitTests;

public class Concepts
{
    //MS test framework crated for .net framework and open source since .net core2
    //nUnit created for java compatibilities and works in .net
    //xUnit since .net core and new .NET features in mind

    //Fake: A working implementation not suitable for production use
    //Dummy: A test double that's never accessed or used
    //Stub: A test double that provides fake data to the system under test
    //Mock: A test double that implements the expected behavior

    //TEST CONTEXT
    //TO share the setup required to run a unit tests
    //1. Constructor and dispose approach
    //The arrange is a new instance for each test
    //We move the arrange to the construyctor
    //WE can implement IDisposable if necessary
    //2. The class fixture approach
    //Defined by implementing a class
    //Each test within the same test class will reuse the same fixture instance.
    //Scoped to a single test class.
    //Requires the test class to implement the IClassFixture<T> interface to use the fixture.
    //3. The collection fixture approach
    //Defined by creating a CollectionDefinition with ICollectionFixture<T>.
    //Allows sharing the same fixture instance across multiple test classes.
    //Scoped to multiple test classes within the same collection.
    //Test classes in the same collection must use the [Collection] attribute.

    //DATA DRIVEN TESTS
    //Reduce the amount of unit test we need to writte
    //Approaches
    //1. Inline data
    //for simple cases
    //2. Member data
    //we can reuser the member data acrross different unit tests methods
    //3. Class data
    //we can reuse the data across different test classes
    //4. Type safe approach
    //5. Data from an external source


    //FACT and THEORY
    //FACT
    //A test which is always true
    //They test invariant conditions
    //THEORY
    //A test that is true only for a partucular set of data

    //TEST DOUBLES
    //1. Fake: A working implementation not suitable for production
    //SqlLite in memory database library for testing
    //2. Dummy: A test double thats never accessed or used
    //En example can be used to add a dependency to factory class that is never used, is just added because the service contract requires
    //3. Stub: A test double that provides fake data to the system uder test
    //4. Mock: Is an object that implements the behaviour we expect and allow us to verify whatever we are testing
    //in our test without having to use the actual object


    //TEST ISOLATION APPROACHES
    //1. Manually creating the test doubles
    //2. Using a mocking framework
    //3. Using EF: In memory database provider
    //FOr simple scenarios
    //not the best to use
}