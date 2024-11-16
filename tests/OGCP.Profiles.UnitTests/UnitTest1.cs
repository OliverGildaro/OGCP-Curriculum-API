namespace OGCP.Profiles.UnitTests
{
    public class UnitTest1
    {
        //test context
        //1. Constructor and dispose approach
        //The arrange is a new instance for each test
        //We move the arrange to the construyctor
        //WE can implement IDisposable if necessary
        //2. The class fixture approach
        //when set test context is expensive
        //Reduce the time to run all tests related to this test context
        //Test context is reuse and need to be initialized only once
        //The collection fixture approach
        [Fact]
        public void Test1()
        {

        }
    }
}