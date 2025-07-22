using testing.Application.DTO;
using testing.ViewModels;

namespace testing.ViewModels
{
    public class GeneralAllModel
    {
        public InformationViewModel information { get; set; }
        public List<InformationDTO> infoAllRecords { get; set; }


        public HobbyViewModel hobby { get; set; }
        public List<HobbyDTO> HobbyAllRecords { get; set; }
    }
}
