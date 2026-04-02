using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace Sofa3.Domain.ReportExport;

public interface IReportExporter
{
    ExportedReport Export(SprintReportModel report);
}


