namespace OGCP.Curriculum.API.domainmodel;

public class DetailInfo
{
    public int Id { get; set; }
    //ef will map this to json in database table
    //and map to the list from databse, all out of the box
    public List<string> Emails { get; set; }
    public string Phone { get; set; }
}
