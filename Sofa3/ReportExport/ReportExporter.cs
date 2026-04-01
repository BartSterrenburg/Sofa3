using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace Sofa3.Domain.ReportExport;

public interface ReportExporter
{
    ExportedReport Export(SprintReportModel report);
}


