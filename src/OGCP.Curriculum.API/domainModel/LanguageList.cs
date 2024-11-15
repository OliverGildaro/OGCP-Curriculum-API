using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.domainModel
{
    public class LanguageList
    {
        private Language[] _languages;

        public LanguageList()
        {
            this._languages = new Language[10];
        }
        public Language this[int index] => this._languages[index];

        public void Add(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language), "Education cannot be null.");

            for (int i = 0; i < _languages.Length; i++)
            {
                if (_languages[i] == null)
                {
                    _languages[i] = language;
                    return;
                }
            }

            throw new InvalidOperationException("No available space in the array to add a new education.");
        }

    }
}
