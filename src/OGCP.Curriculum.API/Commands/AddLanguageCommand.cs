using OGCP.Curriculum.API.dtos;

namespace OGCP.Curriculum.API.Commands
{
    public class AddLanguageCommand : ICommand
    {
        public LanguageEnum Name { get; set; }
        public LevelEnum Level { get; set; }
    }
}
