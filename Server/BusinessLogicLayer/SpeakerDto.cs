using DataLayer.Model;

namespace BusinessLogicLayer
{
    public class SpeakerDto
    {
        public SpeakerDto(Speaker speaker)
        {
            Name = speaker.Name;
            Company = speaker.Company;
        }

        public string Name { get; set; }
        public string Company { get; set; }
    }
}
