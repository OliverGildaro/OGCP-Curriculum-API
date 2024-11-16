namespace OGCP.Profiles.UnitTests
{
    public class Concepts
    {
        //MS test framework crated for .net framework and open source since .net core2
        //nUnit created for java compatibilities and works in .net
        //xUnit since .net core and new .NET features in mind

        //Fake: A working implementation not suitable for production use
        //Dummy: A test double that's never accessed or used
        //Stub: A test double that provides fake data to the system under test
        //Spy: A test double capable of capturing indirect output and providing indirect input as needed
        //Mock: A test double that implements the expected behavior

        //TEST CONTEXT
        //TO share the setup required to run a unit tests
        //1. Constructor and dispose approach
        //The arrange is a new instance for each test
        //We move the arrange to the construyctor
        //WE can implement IDisposable if necessary
        //2. The class fixture approach
        //when set test context is expensive
        //Reduce the time to run all tests related to this test context
        //Test context is reuse and need to be initialized only once
        //3. The collection fixture approach
        //Share context between different test classes

        //DATA DRIVEN TESTS
        //Reduce the amount of unit test we need to writte
        //Approaches
        //1. Inline data
        //for simple cases
        //2. Member data
        //we can reuser the member data acrross different unit tests methods
        //3. Class data
        //4. Type safe approach
        //5. Data from an external source


        //FACT and THEORY
        //FACT
        //A test which is always true
        //They test invariant conditions
        //THEORY
        //A test that is true only for a partucular set of data

        [Fact]
        public void Test1()
        {

        }
    }
}