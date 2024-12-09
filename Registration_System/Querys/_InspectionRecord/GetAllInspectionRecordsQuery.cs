﻿using MediatR;
using Registration_System.DTOs;

namespace Registration_System.Querys._InspectionRecord
{
    public class GetAllInspectionRecordsQuery : IRequest<List<InspectionRecordDTO>>
    {
    }

}