using CSWMS.ViewModel;
using QCMS.Services;
using System.Data;
using System.Data.SqlClient;

namespace CSWMS.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DatabaseService _databaseService;

        public DashboardRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // ================= KPI =================
        public async Task<DashboardKpiDto> GetDashboardKpiAsync()
        {
            DashboardKpiDto model = new DashboardKpiDto();

            string query = @"
                      DECLARE @ThisMonthStart DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                      DECLARE @NextMonthStart DATE = DATEADD(MONTH, 1, @ThisMonthStart);

                      DECLARE @LastMonthStart DATE = DATEADD(MONTH, -1, @ThisMonthStart);
                      DECLARE @LastMonthEnd DATE = @ThisMonthStart;

                      DECLARE @TodayStart DATETIME = CAST(GETDATE() AS DATE);
                      DECLARE @TomorrowStart DATETIME = DATEADD(DAY, 1, @TodayStart);

                      SELECT

                      -- ================= TOTAL =================
                      --(SELECT COUNT(*) FROM Complain) AS TotalTickets,

                      --(SELECT COUNT(*) FROM Assign) AS TotalAssign,

                      --(SELECT COUNT(*) FROM Assign WHERE StatusId = 1) AS Solved,

                      --(SELECT COUNT(*) FROM Assign WHERE StatusId = 2) AS Pending,

                      --(SELECT COUNT(*) FROM Assign WHERE StatusId = 3) AS Cancelled,

                      (SELECT COUNT(*) FROM Technician WHERE Active=1) AS TotalTechnician,

                      (SELECT COUNT(*) FROM Supervisor) AS TotalSupervisor,

                      (SELECT COUNT(*) FROM Zone) AS TotalServiceCenter,
 
                      --CAST(
                      --    ROUND(
                      --        (
                      --            (SELECT COUNT(*)
                      --             FROM Assign
                      --             WHERE StatusId = 1) * 100.0
                      --        )
                      --        /
                      --        NULLIF(
                      --            (SELECT COUNT(*)
                      --             FROM Complain),
                      --            0
                      --        ),
                      --        2
                      --    )
                      --AS DECIMAL(10,2)) AS TotalSolvedPercentage,

                      -- ================= THIS MONTH =================
                      (SELECT COUNT(*)
                       FROM Complain
                       WHERE EntryDate >= @ThisMonthStart
                         AND EntryDate < @NextMonthStart
                      ) AS ThisMonthTickets,

                      (SELECT COUNT(*)
                       FROM Assign
                       WHERE StatusId = 1
                         AND EntryDate >= @ThisMonthStart
                         AND EntryDate < @NextMonthStart
                      ) AS ThisMonthSolved,

                      (SELECT COUNT(*)
                       FROM Assign
                       WHERE StatusId = 2
                         AND EntryDate >= @ThisMonthStart
                         AND EntryDate < @NextMonthStart
                      ) AS ThisMonthPending,

                      (SELECT COUNT(*)
                       FROM Assign
                       WHERE StatusId = 3
                         AND EntryDate >= @ThisMonthStart
                         AND EntryDate < @NextMonthStart
                      ) AS ThisMonthCancelled,

                      CAST(
                          ROUND(
                              (
                                  (SELECT COUNT(*)
                                   FROM Assign
                                   WHERE StatusId = 1
                                     AND EntryDate >= @ThisMonthStart
                                     AND EntryDate < @NextMonthStart) * 100.0
                              )
                              /
                              NULLIF(
                                  (SELECT COUNT(*)
                                   FROM Complain
                                   WHERE EntryDate >= @ThisMonthStart
                                     AND EntryDate < @NextMonthStart),
                                  0
                              ),
                              2
                          )
                      AS DECIMAL(10,2)) AS ThisMonthSolvedPercentage,


                      -- ================= LAST MONTH =================
                      (SELECT COUNT(*)
                       FROM Complain
                       WHERE EntryDate >= @LastMonthStart
                         AND EntryDate < @LastMonthEnd
                      ) AS LastMonthTickets,

                      (SELECT COUNT(*)
                       FROM Assign
                       WHERE StatusId = 1
                         AND EntryDate >= @LastMonthStart
                         AND EntryDate < @LastMonthEnd
                      ) AS LastMonthSolved,

                      (SELECT COUNT(*)
                       FROM Assign
                       WHERE StatusId = 2
                         AND EntryDate >= @LastMonthStart
                         AND EntryDate < @LastMonthEnd
                      ) AS LastMonthPending,

                      (SELECT COUNT(*)
                       FROM Assign
                       WHERE StatusId = 3
                         AND EntryDate >= @LastMonthStart
                         AND EntryDate < @LastMonthEnd
                      ) AS LastMonthCancelled,

                      CAST(
                          ROUND(
                              (
                                  (SELECT COUNT(*)
                                   FROM Assign
                                   WHERE StatusId = 1
                                     AND EntryDate >= @LastMonthStart
                                     AND EntryDate < @LastMonthEnd) * 100.0
                              )
                              /
                              NULLIF(
                                  (SELECT COUNT(*)
                                   FROM Complain
                                   WHERE EntryDate >= @LastMonthStart
                                     AND EntryDate < @LastMonthEnd),
                                  0
                              ),
                              2
                          )
                      AS DECIMAL(10,2)) AS LastMonthSolvedPercentage,

                      -- ================= This Vs Last MONTH SOLVE Groth % =================

                      CAST(
                      (
                          (
                              (
                                  (SELECT COUNT(*)
                                   FROM Assign
                                   WHERE StatusId = 1
                                   AND EntryDate >= @ThisMonthStart
                                   AND EntryDate < @NextMonthStart
                                  ) * 100.0
                              )
                              /
                              NULLIF(
                                  (
                                      SELECT COUNT(*)
                                      FROM Complain
                                      WHERE EntryDate >= @ThisMonthStart
                                      AND EntryDate < @NextMonthStart
                                  ),0
                              )
                          )

                          -

                          (
                              (
                                  (SELECT COUNT(*)
                                   FROM Assign
                                   WHERE StatusId = 1
                                   AND EntryDate >= @LastMonthStart
                                   AND EntryDate < @LastMonthEnd
                                  ) * 100.0
                              )
                              /
                              NULLIF(
                                  (
                                      SELECT COUNT(*)
                                      FROM Complain
                                      WHERE EntryDate >= @LastMonthStart
                                      AND EntryDate < @LastMonthEnd
                                  ),0
                              )
                          )
                      )
                      AS DECIMAL(10,2)
                      ) AS SolveGrowthPercentage,

                      -- ================= LAST 3 MONTH AVG SOLVE % =================

                      CAST(ISNULL(
                      (
                          SELECT AVG(MonthSolvePercentage)
                          FROM
                          (
                              SELECT
                                  (SUM(CASE WHEN StatusId = 1 THEN 1 ELSE 0 END) * 100.0)
                                  / NULLIF(COUNT(*),0) AS MonthSolvePercentage
                              FROM Assign
                              WHERE EntryDate >= DATEADD(MONTH,-3,@ThisMonthStart)
                                AND EntryDate < @NextMonthStart
                              GROUP BY YEAR(EntryDate), MONTH(EntryDate)
                          ) X
                      ),0) AS DECIMAL(10,2)) AS Last3MonthAvgSolvePercentage,

                      -- ================= TODAY =================

	                    (SELECT COUNT(*)
	                      FROM Complain
	                      WHERE EntryDate >= @TodayStart
		                    AND EntryDate < @TomorrowStart
	                     ) AS TodayTickets

	                    ,(SELECT COUNT(*)
	                      FROM Assign
	                      WHERE StatusId = 1
		                    AND EntryDate >= @TodayStart
		                    AND EntryDate < @TomorrowStart
	                     ) AS TodaySolved

	                    ,(SELECT COUNT(*)
	                      FROM Assign
	                      WHERE StatusId = 2
		                    AND EntryDate >= @TodayStart
		                    AND EntryDate < @TomorrowStart
	                     ) AS TodayPending

	                    ,(SELECT COUNT(*)
	                      FROM Assign
	                      WHERE StatusId = 3
		                    AND EntryDate >= @TodayStart
		                    AND EntryDate < @TomorrowStart
	                     ) AS TodayCancelled; ";

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                //model.TotalTickets = dr["TotalTickets"] != DBNull.Value
                //    ? Convert.ToInt32(dr["TotalTickets"]) : 0;

                //model.TotalAssign = dr["TotalAssign"] != DBNull.Value
                //   ? Convert.ToInt32(dr["TotalAssign"]) : 0;

                //model.Solved = dr["Solved"] != DBNull.Value
                //    ? Convert.ToInt32(dr["Solved"]) : 0;

                //model.Pending = dr["Pending"] != DBNull.Value
                //    ? Convert.ToInt32(dr["Pending"]) : 0;

                //model.Cancelled = dr["Cancelled"] != DBNull.Value
                //    ? Convert.ToInt32(dr["Cancelled"]) : 0;

                //model.Revenue = dr["Revenue"] != DBNull.Value
                //    ? Convert.ToDecimal(dr["Revenue"]) : 0;

                //model.AvgResolution = dr["AvgResolution"] != DBNull.Value
                //    ? Convert.ToDouble(dr["AvgResolution"]) : 0;

                //model.TotalTickets = Convert.ToInt32(dr["TotalTickets"]);
                //model.TotalAssign = Convert.ToInt32(dr["TotalAssign"]);
                //model.Solved = Convert.ToInt32(dr["Solved"]);
                //model.Pending = Convert.ToInt32(dr["Pending"]);
                //model.Cancelled = Convert.ToInt32(dr["Cancelled"]);

                model.TodayTickets = Convert.ToInt32(dr["TodayTickets"]);
                model.TodaySolved = Convert.ToInt32(dr["TodaySolved"]);
                model.TodayPending = Convert.ToInt32(dr["TodayPending"]);
                model.TodayCancelled = Convert.ToInt32(dr["TodayCancelled"]);

                model.ThisMonthTickets = Convert.ToInt32(dr["ThisMonthTickets"]);
                model.LastMonthTickets = Convert.ToInt32(dr["LastMonthTickets"]);

                model.ThisMonthSolved = Convert.ToInt32(dr["ThisMonthSolved"]);
                model.LastMonthSolved = Convert.ToInt32(dr["LastMonthSolved"]);

                model.ThisMonthCancelled = Convert.ToInt32(dr["ThisMonthCancelled"]);
                model.LastMonthCancelled = Convert.ToInt32(dr["LastMonthCancelled"]);

                model.ThisMonthPending = Convert.ToInt32(dr["ThisMonthPending"]);
                model.LastMonthPending = Convert.ToInt32(dr["LastMonthPending"]);

                model.TotalTechnician = Convert.ToInt32(dr["TotalTechnician"]);
                model.TotalSupervisor = Convert.ToInt32(dr["TotalSupervisor"]);
                model.TotalServiceCenter = Convert.ToInt32(dr["TotalServiceCenter"]);

                model.ThisMonthSolvedPercentage = Convert.ToDecimal(dr["ThisMonthSolvedPercentage"]);
                model.LastMonthSolvedPercentage = Convert.ToDecimal(dr["LastMonthSolvedPercentage"]);
                //model.TotalSolvedPercentage = Convert.ToDecimal(dr["TotalSolvedPercentage"]);

                model.Last3MonthAvgSolvePercentage = Convert.ToDecimal(dr["Last3MonthAvgSolvePercentage"]);
                model.SolveGrowthPercentage = Convert.ToDecimal(dr["SolveGrowthPercentage"]);

                // Ticket Growth %
                model.TicketGrowth =
                    model.LastMonthTickets == 0
                    ? 100
                    : Math.Round(
                        ((decimal)(model.ThisMonthTickets - model.LastMonthTickets)
                        / model.LastMonthTickets) * 100, 2);

                // Solved Growth %
                model.SolvedGrowth =
                    model.LastMonthSolved == 0
                    ? 100
                    : Math.Round(
                        ((decimal)(model.ThisMonthSolved - model.LastMonthSolved)
                        / model.LastMonthSolved) * 100, 2);

                // Pending Growth %
                model.PendingGrowth =
                model.LastMonthPending == 0
                ? 100
                : Math.Round(
                    ((decimal)(model.ThisMonthPending - model.LastMonthPending)
                    / model.LastMonthPending) * 100, 2);

                // Cancel Growth %
                model.CancelGrowth =
                    model.LastMonthCancelled == 0
                    ? 100
                    : Math.Round(
                        ((decimal)(model.ThisMonthCancelled - model.LastMonthCancelled)
                        / model.LastMonthCancelled) * 100, 2);
            }

            return model;
        }

        // ================= DAILY TREND =================
        //public async Task<DashboardChartDto> GetDailyTrendAsync()
        //{
        //    DashboardChartDto model = new DashboardChartDto()
        //    {
        //        Labels = new List<string>(),
        //        Values = new List<decimal>()
        //    };

        //    string query = @"
        //        SELECT 
        //            CAST(C.EntryDate AS DATE) AS ReportDate,
        //            COUNT(*) AS TotalTickets
        //        FROM Complain C
        //        WHERE 
        //           C.EntryDate >= DATEADD(DAY, -29, GETDATE())
        //        GROUP BY CAST(C.EntryDate AS DATE)
        //        ORDER BY ReportDate DESC;
        //    ";

        //    DataTable dt = await _databaseService.ExecuteQueryAsync(query);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        model.Labels.Add(
        //            Convert.ToDateTime(dr["ReportDate"]).ToString("dd-MMM")
        //        );

        //        model.Values.Add(
        //            Convert.ToDecimal(dr["TotalTickets"])
        //        );
        //    }

        //    return model;
        //}

        public async Task<DailyTrendDto> GetDailyTrendAsync()
        {
            DailyTrendDto model = new DailyTrendDto();

            string query = @"
            SELECT
                CAST(C.EntryDate AS DATE) AS ReportDate,

                COUNT(DISTINCT C.TicketCode) AS TotalTickets,

                SUM(
                    CASE
                        WHEN A.StatusId = 1 THEN 1
                        ELSE 0
                    END
                ) AS SolvedTickets

            FROM Complain C

            LEFT JOIN Assign A
                ON A.TicketID = C.TicketCode

            WHERE C.EntryDate >= DATEADD(DAY,-30,GETDATE())

            GROUP BY CAST(C.EntryDate AS DATE)

            ORDER BY ReportDate
          ";

                DataTable dt = await _databaseService.ExecuteQueryAsync(query);

                foreach (DataRow dr in dt.Rows)
                {
                    model.Labels.Add(
                        Convert.ToDateTime(dr["ReportDate"])
                            .ToString("dd-MMM")
                    );

                    model.TotalTickets.Add(
                        Convert.ToInt32(dr["TotalTickets"])
                    );

                    model.SolvedTickets.Add(
                        Convert.ToInt32(dr["SolvedTickets"])
                    );
                }

                return model;
        }

        // ================= STATUS =================
        public async Task<List<int>> GetStatusAsync()
        {
            List<int> data = new List<int>();

            string query = @"
               SELECT
                SUM(CASE WHEN StatusId = 1 THEN 1 ELSE 0 END) AS Solved,
                SUM(CASE WHEN StatusId = 2 THEN 1 ELSE 0 END) AS Pending,
                SUM(CASE WHEN StatusId = 3 THEN 1 ELSE 0 END) AS Cancelled
                FROM Assign A
                WHERE CAST(A.EntryDate AS DATE) = CAST(GETDATE() AS DATE);
            ";

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                data.Add(Convert.ToInt32(dr["Solved"]));
                data.Add(Convert.ToInt32(dr["Pending"]));
                data.Add(Convert.ToInt32(dr["Cancelled"]));
            }

            return data;
        }

        // ================= ZONE =================
        public async Task<ZoneDashboardDto> GetZoneAsync()
        {
            ZoneDashboardDto model = new ZoneDashboardDto();

            string query = @"
            SELECT
                ISNULL(Z.ZoneName,'Unknown') AS ZoneName,

                COUNT(DISTINCT C.TicketCode) AS TotalTickets,

                SUM(CASE WHEN A.StatusId = 1 THEN 1 ELSE 0 END) AS Solved,

                SUM(CASE WHEN A.StatusId = 2 THEN 1 ELSE 0 END) AS Pending,

                SUM(CASE WHEN A.StatusId = 3 THEN 1 ELSE 0 END) AS Cancelled

            FROM Complain C

            LEFT JOIN Assign A
                ON A.TicketID = C.TicketCode

            LEFT JOIN Zone Z
                ON Z.ZoneCode = C.apid

            WHERE
                C.EntryDate >= DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1)
                AND C.EntryDate < DATEADD(MONTH,1,
                    DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1))

            GROUP BY Z.ZoneName

            ORDER BY TotalTickets DESC
            ";

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            foreach (DataRow dr in dt.Rows)
            {
                model.Labels.Add(dr["ZoneName"].ToString());

                model.TotalTickets.Add(
                    Convert.ToDecimal(dr["TotalTickets"])
                );

                model.Solved.Add(
                    Convert.ToDecimal(dr["Solved"])
                );

                model.Pending.Add(
                    Convert.ToDecimal(dr["Pending"])
                );

                model.Cancelled.Add(
                    Convert.ToDecimal(dr["Cancelled"])
                );
            }

            return model;
        }

        // ================= TOP TECHNICIAN =================
        //        public async Task<DashboardChartDto> GetTechnicianAsync()
        //        {
        //            DashboardChartDto model = new DashboardChartDto()
        //            {
        //                Labels = new List<string>(),
        //                Values = new List<decimal>()
        //            };

        //            string query = @"
        //                SELECT TOP (20)
        //                    T.TechnicianName,
        //                    COUNT(A.TicketID) AS TotalTickets
        //                FROM Assign A
        //                LEFT JOIN Technician T 
        //                    ON A.TechnicianId = T.TechnicianId
        //                WHERE A.EntryDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
        //                  AND A.EntryDate < DATEADD(MONTH, 1,
        //                        DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
        //                GROUP BY T.TechnicianName
        //                ORDER BY TotalTickets DESC;
        //";

        //            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                model.Labels.Add(dr["TechnicianName"].ToString());

        //                model.Values.Add(
        //                    Convert.ToDecimal(dr["TotalTickets"])
        //                );
        //            }

        //            return model;
        //        }
        public async Task<TechnicianPerformanceDto> GetTechnicianAsync()
        {
            TechnicianPerformanceDto model = new TechnicianPerformanceDto();

            string query = @"
            SELECT TOP 10

                T.TechnicianName,

                COUNT(A.TicketID) AS TotalTickets,

                SUM(CASE WHEN A.StatusId = 1 THEN 1 ELSE 0 END) AS Solved,

                SUM(CASE WHEN A.StatusId = 2 THEN 1 ELSE 0 END) AS Pending,

                SUM(CASE WHEN A.StatusId = 3 THEN 1 ELSE 0 END) AS Cancelled,

                CAST(
                    (
                        SUM(CASE WHEN A.StatusId = 1 THEN 1 ELSE 0 END) * 100.0
                    ) / NULLIF(COUNT(A.TicketID),0)
                    AS DECIMAL(10,2)
                ) AS SolveRate

            FROM Assign A

            INNER JOIN Technician T
                ON A.TechnicianId = T.TechnicianId

            WHERE A.EntryDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
              AND A.EntryDate < DATEADD(MONTH,1,
                    DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))

            GROUP BY T.TechnicianName

            HAVING COUNT(A.TicketID) > 0

            ORDER BY Solved DESC;
            ";

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            foreach (DataRow dr in dt.Rows)
            {
                model.Labels.Add(dr["TechnicianName"].ToString());

                model.TotalTickets.Add(Convert.ToInt32(dr["TotalTickets"]));

                model.Solved.Add(Convert.ToInt32(dr["Solved"]));

                model.Pending.Add(Convert.ToInt32(dr["Pending"]));

                model.Cancelled.Add(Convert.ToInt32(dr["Cancelled"]));
                model.SolveRate.Add(Convert.ToDecimal(dr["SolveRate"]));
            }

            return model;
        }
        // ================= BOTTOM TECHNICIAN =================
        //public async Task<DashboardChartDto> GetBottomTechnicianAsync()
        //{
        //    DashboardChartDto model = new DashboardChartDto()
        //    {
        //        Labels = new List<string>(),
        //        Values = new List<decimal>()
        //    };

        //    string query = @"
        //        SELECT TOP (20)
        //            T.TechnicianName,
        //            COUNT(A.TicketID) AS TotalTickets
        //        FROM Assign A
        //        LEFT JOIN Technician T
        //            ON A.TechnicianId = T.TechnicianId
        //        WHERE A.EntryDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
        //          AND A.EntryDate < DATEADD(MONTH,1,
        //                DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
        //        GROUP BY T.TechnicianName
        //        ORDER BY TotalTickets ASC
        //    ";

        //    DataTable dt = await _databaseService.ExecuteQueryAsync(query);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        model.Labels.Add(dr["TechnicianName"].ToString());
        //        model.Values.Add(Convert.ToDecimal(dr["TotalTickets"]));
        //    }

        //    return model;
        //}
        public async Task<TechnicianPerformanceDto> GetBottomTechnicianAsync()
        {
            TechnicianPerformanceDto model = new TechnicianPerformanceDto();

            string query = @"
            SELECT TOP 10

                T.TechnicianName,

                COUNT(A.TicketID) AS TotalTickets,

                SUM(CASE WHEN A.StatusId = 1 THEN 1 ELSE 0 END) AS Solved,

                SUM(CASE WHEN A.StatusId = 2 THEN 1 ELSE 0 END) AS Pending,

                SUM(CASE WHEN A.StatusId = 3 THEN 1 ELSE 0 END) AS Cancelled,

                CAST(
                    (
                        SUM(CASE WHEN A.StatusId = 1 THEN 1 ELSE 0 END) * 100.0
                    ) / NULLIF(COUNT(A.TicketID),0)
                    AS DECIMAL(10,2)
                ) AS SolveRate

            FROM Assign A

            INNER JOIN Technician T
                ON A.TechnicianId = T.TechnicianId

            WHERE A.EntryDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
              AND A.EntryDate < DATEADD(MONTH,1,
                    DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))

            GROUP BY T.TechnicianName

            HAVING COUNT(A.TicketID) > 0

            ORDER BY Solved ASC;
            ";

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            foreach (DataRow dr in dt.Rows)
            {
                model.Labels.Add(dr["TechnicianName"].ToString());

                model.TotalTickets.Add(Convert.ToInt32(dr["TotalTickets"]));

                model.Solved.Add(Convert.ToInt32(dr["Solved"]));

                model.Pending.Add(Convert.ToInt32(dr["Pending"]));

                model.Cancelled.Add(Convert.ToInt32(dr["Cancelled"]));
                model.SolveRate.Add(Convert.ToDecimal(dr["SolveRate"]));
            }

            return model;
        }
        // ================= Product-Wise Service Analysis =================
        public async Task<List<ProductWiseServiceAnalysisDto>> GetProductWiseServiceAnalysisAsync()
        {
            List<ProductWiseServiceAnalysisDto> list = new();

            string query = @"

            SELECT TOP 7

            ISNULL(I.ItemName,'Unknown') ItemName,

            COUNT(DISTINCT C.TicketCode) TotalTickets,

            SUM(CASE
                    WHEN A.StatusId = 1
                    THEN 1
                    ELSE 0
                END) SolvedTickets,

            SUM(CASE
                    WHEN A.StatusId = 2
                    THEN 1
                    ELSE 0
                END) PendingTickets,

            SUM(CASE
                    WHEN A.StatusId NOT IN (1,2)
                         OR A.StatusId IS NULL
                    THEN 1
                    ELSE 0
                END) ClosedOrCancelledTickets,

            AVG(
                CASE
                    WHEN F.WorkingDate IS NOT NULL
                    THEN DATEDIFF(DAY,C.EntryDate,F.WorkingDate)
                END
            ) AvgResolutionDays,
            CAST(
			(
				SUM(CASE WHEN A.StatusId=1 THEN 1 ELSE 0 END) * 100.0
			)
			/
			NULLIF(COUNT(DISTINCT C.TicketCode),0)
			AS DECIMAL(10,2)
			) AS SolvedPercentage

            FROM Complain C

            LEFT JOIN Assign A
                ON A.TicketID = C.TicketCode

            LEFT JOIN Feedback F
                ON F.TicketID = C.TicketCode

            LEFT JOIN Item I
                ON F.ItemId = I.ItemId
            
            WHERE C.EntryDate >= DATEADD(DAY, -30, GETDATE())
            AND C.EntryDate < GETDATE()

            GROUP BY I.ItemName

            ORDER BY TotalTickets DESC";

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ProductWiseServiceAnalysisDto
                {
                    ItemName = dr["ItemName"].ToString(),

                    TotalTickets =
                        Convert.ToInt32(dr["TotalTickets"]),

                    SolvedTickets =
                        Convert.ToInt32(dr["SolvedTickets"]),

                    PendingTickets =
                        Convert.ToInt32(dr["PendingTickets"]),

                    ClosedOrCancelledTickets =
                        Convert.ToInt32(dr["ClosedOrCancelledTickets"]),

                    AvgResolutionDays =
                        dr["AvgResolutionDays"] == DBNull.Value
                        ? 0
                        : Convert.ToDouble(dr["AvgResolutionDays"]),
                    SolvedPercentage =
                        dr["SolvedPercentage"] == DBNull.Value
                        ? 0
                        : Convert.ToDouble(dr["SolvedPercentage"])
                });
            }

            return list;
        }
        public async Task<List<CancelledTicketDto>> GetCancelledDetailsAsync(string type)
        {
            string whereClause = "";

            if (type == "thismonth")
            {
                whereClause = @"
           AND A.EntryDate >= DATEFROMPARTS(
            YEAR(GETDATE()),
            MONTH(GETDATE()),
            1
           )";
            }
            else if (type == "lastmonth")
            {
                whereClause = @"
            AND A.EntryDate >= DATEADD(MONTH,-1,
            DATEFROMPARTS(
                YEAR(GETDATE()),
                MONTH(GETDATE()),
                1
            )
            )
            AND A.EntryDate <
                DATEFROMPARTS(
                    YEAR(GETDATE()),
                    MONTH(GETDATE()),
                    1
                )";
                }
            if (type == "today")
            {
                whereClause = @"
            AND A.EntryDate >= CAST(GETDATE() AS DATE)
            AND A.EntryDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))";
            }

                string query = $@"

            SELECT TOP 500

                C.TicketCode,

                C.CustomerName,

                C.ContactNo,

                ISNULL(I.ItemName,'N/A') ItemName,

                C.ProblemName,

                A.EntryDate,

                'Cancelled' StatusName

            FROM Assign A

            INNER JOIN Complain C
                ON C.TicketCode = A.TicketID

            LEFT JOIN Feedback F
                ON F.TicketID = C.TicketCode

            LEFT JOIN Item I
                ON I.ItemId = F.ItemId

            WHERE A.StatusId = 3
            {whereClause}

            ORDER BY A.EntryDate DESC";

            DataTable dt =
                await _databaseService.ExecuteQueryAsync(query);

            List<CancelledTicketDto> list = new();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new CancelledTicketDto
                {
                    TicketCode = dr["TicketCode"].ToString(),
                    CustomerName = dr["CustomerName"].ToString(),
                    ContactNo = dr["ContactNo"].ToString(),
                    ItemName = dr["ItemName"].ToString(),
                    ProblemName = dr["ProblemName"].ToString(),
                    StatusName = dr["StatusName"].ToString(),
                    EntryDate = Convert.ToDateTime(dr["EntryDate"])
                });
            }

            return list;
        }
        // ================= Warranty Dashboard Data =================
        public async Task<WarrantyDashboardDto> GetWarrantyDashboardAsync()
        {
            string query = @"
            SELECT
                COUNT(DISTINCT C.TicketCode) TotalTickets,

                SUM(
                    CASE
                        WHEN FG.ServiceWarDate >= GETDATE()
                        THEN 1
                        ELSE 0
                    END
                ) InWarranty,

                SUM(
                    CASE
                        WHEN FG.ServiceWarDate < GETDATE()
                        THEN 1
                        ELSE 0
                    END
                ) OutWarranty,

                SUM(
                    CASE
                        WHEN FG.DBarcode IS NULL
                        THEN 1
                        ELSE 0
                    END
                ) WarrantyNotFound,

                NULLIF(
                    SUM(
                        CASE
                            WHEN FG.DBarcode IS NOT NULL
                            THEN 1
                            ELSE 0
                        END
                    ),
                    0
                ) WarrantyFound,

                ROUND(
                    (
                        SUM(
                            CASE
                                WHEN FG.ServiceWarDate >= GETDATE()
                                THEN 1
                                ELSE 0
                            END
                        ) * 100.0
                    )
                    /
                    NULLIF(COUNT(*), 0),
                    2
                ) WarrantyPercentage

            FROM Complain C

            LEFT JOIN Feedback F
                ON F.TicketID = C.TicketCode

            LEFT JOIN VW_WarrantyInfo FG
                ON FG.DBarcode = F.SerialNo

            WHERE MONTH(C.EntryDate) = MONTH(GETDATE())
            AND YEAR(C.EntryDate) = YEAR(GETDATE());";

            WarrantyDashboardDto model = new();

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                model.TotalTickets =
                    Convert.ToInt32(dr["TotalTickets"]);

                model.InWarranty =
                    dr["InWarranty"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr["InWarranty"]);

                model.OutWarranty =
                    dr["OutWarranty"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr["OutWarranty"]);

                model.WarrantyNotFound =
                    dr["WarrantyPercentage"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr["WarrantyNotFound"]);

                model.WarrantyFound =
                    dr["WarrantyFound"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr["WarrantyFound"]);

                model.WarrantyPercentage =
                    dr["WarrantyPercentage"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr["WarrantyPercentage"]);
            }

            return model;
        }
        // ================= Warranty Chart Data =================
        public async Task<WarrantyDashboardDto> GetWarrantyChartAsync()
        {
            string query = @"
                SELECT
                    COUNT(DISTINCT C.TicketCode) TotalTickets,

                    SUM(
                        CASE
                            WHEN FG.ServiceWarDate >= GETDATE()
                            THEN 1
                            ELSE 0
                        END
                    ) InWarranty,

                    SUM(
                        CASE
                            WHEN FG.ServiceWarDate < GETDATE()
                            THEN 1
                            ELSE 0
                        END
                    ) OutWarranty

                FROM Complain C

                LEFT JOIN Feedback F
                    ON F.TicketID = C.TicketCode

                LEFT JOIN VW_WarrantyInfo FG
                    ON FG.DBarcode = F.SerialNo

                WHERE YEAR(C.EntryDate) = YEAR(GETDATE());";

            WarrantyDashboardDto model = new();

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                model.TotalTickets =
                    Convert.ToInt32(dr["TotalTickets"]);

                model.InWarranty =
                    dr["InWarranty"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr["InWarranty"]);

                model.OutWarranty =
                    dr["OutWarranty"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(dr["OutWarranty"]);

              
            }

            return model;
        }
        // ================= Service Operation Data =================
        public async Task<List<ServiceOperationPerformanceDto>> GetServiceOperationPerformanceAsync()
        {
            List<ServiceOperationPerformanceDto> model =
                new List<ServiceOperationPerformanceDto>();

            string query = @"
                SELECT
                    SO.ServiceOperationName,

                    COUNT(A.TicketID) AS TotalTickets,

                    SUM(CASE WHEN A.StatusId = 1 THEN 1 ELSE 0 END) AS Solved,

                    SUM(CASE WHEN A.StatusId = 2 THEN 1 ELSE 0 END) AS Pending,

                    SUM(CASE WHEN A.StatusId = 3 THEN 1 ELSE 0 END) AS Cancelled,

                    CAST(
                        SUM(CASE WHEN A.StatusId = 1 THEN 1 ELSE 0 END) * 100.0
                        / NULLIF(COUNT(A.TicketID),0)
                        AS DECIMAL(10,2)
                    ) AS SolveRate,

                    CAST(
                        SUM(CASE WHEN A.StatusId = 2 THEN 1 ELSE 0 END) * 100.0
                        / NULLIF(COUNT(A.TicketID),0)
                        AS DECIMAL(10,2)
                    ) AS PendingRate,

                    CAST(
                        SUM(CASE WHEN A.StatusId = 3 THEN 1 ELSE 0 END) * 100.0
                        / NULLIF(COUNT(A.TicketID),0)
                        AS DECIMAL(10,2)
                    ) AS CancelledRate,

                    COUNT(DISTINCT T.TechnicianId) AS ActiveTechnicians,

                    CAST(
                        COUNT(A.TicketID) * 1.0
                        / NULLIF(COUNT(DISTINCT T.TechnicianId),0)
                        AS DECIMAL(10,2)
                    ) AS AvgTicketsPerTechnician,

                    SUM(
                        CASE
                            WHEN A.StatusId = 2
                             AND A.EntryDate < DATEADD(DAY,-7,GETDATE())
                            THEN 1
                            ELSE 0
                        END
                    ) AS PendingOver7Days,

                    SUM(
                        CASE
                            WHEN A.StatusId = 2
                             AND A.EntryDate < DATEADD(DAY,-30,GETDATE())
                            THEN 1
                            ELSE 0
                        END
                    ) AS PendingOver30Days

                FROM Assign A

                INNER JOIN Technician T
                    ON A.TechnicianId = T.TechnicianId

                LEFT JOIN Supervisor S
                    ON S.SupervisorId = T.SupervisorId

                LEFT JOIN ServiceOperation SO
                    ON SO.ServiceOperationId = S.ServiceOperationId

                WHERE A.EntryDate >= DATEADD(DAY, -30, CAST(GETDATE() AS DATE))
                   AND A.EntryDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))

                GROUP BY SO.ServiceOperationName

                HAVING COUNT(A.TicketID) > 0

                ORDER BY SolveRate DESC
                ";

            DataTable dt = await _databaseService.ExecuteQueryAsync(query);

            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new ServiceOperationPerformanceDto
                {
                    ServiceOperationName = dr["ServiceOperationName"].ToString(),

                    TotalTickets = Convert.ToInt32(dr["TotalTickets"]),

                    Solved = Convert.ToInt32(dr["Solved"]),

                    Pending = Convert.ToInt32(dr["Pending"]),

                    Cancelled = Convert.ToInt32(dr["Cancelled"]),

                    SolveRate = Convert.ToDecimal(dr["SolveRate"]),

                    PendingRate = Convert.ToDecimal(dr["PendingRate"]),

                    CancelledRate = Convert.ToDecimal(dr["CancelledRate"]),

                    ActiveTechnicians = Convert.ToInt32(dr["ActiveTechnicians"]),

                    AvgTicketsPerTechnician =
                        Convert.ToDecimal(dr["AvgTicketsPerTechnician"]),

                    PendingOver7Days = Convert.ToInt32(dr["PendingOver7Days"]),

                    PendingOver30Days = Convert.ToInt32(dr["PendingOver30Days"]),
                });
            }

            return model;
        }

        //public async Task<WarrantyDashboardDto> GetWarrantyDashboardAsync()
        //{
        //    string query = @"
        //SELECT
        //    SUM(
        //        CASE
        //            WHEN FG.ServiceWarDate >= GETDATE()
        //            THEN 1
        //            ELSE 0
        //        END
        //    ) AS InWarranty,

        //    SUM(
        //        CASE
        //            WHEN FG.ServiceWarDate < GETDATE()
        //            THEN 1
        //            ELSE 0
        //        END
        //    ) AS OutWarranty

        //FROM Complain C

        //INNER JOIN Feedback F
        //    ON F.TicketID = C.TicketCode

        //INNER JOIN FgProductionDetails FG
        //    ON FG.DBarcode = F.SerialNo";

        //    WarrantyDashboardDto model = new();

        //    DataTable dt = await _databaseService.ExecuteQueryAsync(query);

        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];

        //        model.InWarranty =
        //            dr["InWarranty"] == DBNull.Value
        //                ? 0
        //                : Convert.ToInt32(dr["InWarranty"]);

        //        model.OutWarranty =
        //            dr["OutWarranty"] == DBNull.Value
        //                ? 0
        //                : Convert.ToInt32(dr["OutWarranty"]);
        //    }

        //    return model;
        //}
    }
}