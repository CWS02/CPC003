--交通類 公務車、高鐵、飛機、計程車、捷運、台灣火車、地鐵、私車公用
DECLARE @transport NVARCHAR(50) = N'私車公用';

SELECT 
    SUM(Total) AS TotalSum
FROM (
    SELECT SUM(UDF06) AS Total
    FROM ACPTB
    WHERE UDF01 LIKE @transport + '%'
      AND TB008 LIKE '2024%'
    UNION ALL
    SELECT SUM(UDF06)
    FROM PCMTG
    WHERE UDF01 LIKE @transport + '%'
      AND TG013 LIKE '2024%'
) AS T;
