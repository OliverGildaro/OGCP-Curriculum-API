using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.domainModel;
public class EducationList
{
    private List<Education> _educations = new();

    public Education this[int index]
    {
        get => _educations[index];
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            _educations[index] = value;
        }
    }

    public void Add(Education education)
    {
        if (_educations.Count >= 5)
            throw new InvalidOperationException("Cannot have more than 5 educations");
        _educations.Add(education);
    }

    public void RemoveAt(int index) => _educations.RemoveAt(index);

    public int Count => _educations.Count;
}

