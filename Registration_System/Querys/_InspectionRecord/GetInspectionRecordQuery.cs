using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._InspectionRecord
{
    public class GetInspectionRecordQuery : IRequest<InspectionRecordDTO>
    {
        public Guid InspectionId { get; set; }
    }

}
