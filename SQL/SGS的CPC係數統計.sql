use ESG
GO
--冷媒
select sum(CC012) from ColdCoal where CC010='南科'and CC007 in ('HFC-134A','R-134A','R-407C','R-410A') GROUP BY CC007
--滅火器
select sum(FE002),FE005,sum(FE002*FE006) from FireExtin group by FE010,FE005
select FE001,FE002,FE006 from FireExtin
--wd40 1067.31
select sum(WD011) from WD40A where WD003 like '2024%'
--電費8411400
select sum(TOTAL_ELECTRICITY) from ELECTRICITY_BILL where FACTORY='南科廠' and FROM_BILLING_PERIOD like '%2024%'
--員工通勤+碳排係數
select 
    g.TRANSPORTATION,
    sum(g.DOUBLE_KM) as TOTAL_KM,
    b.EF_VALUE
from CAT_THREE_EMPLOYEE_COMMUTING c
join GHG_MST_COMMUTE g on c.USER_ID = g.USER_ID
join BRM_MST_EMISSION_FACTOR b on g.TRANSPORTATION = b.EF_NAME
where b.EF_YEAR = 2024 and c.WORK_DATE like '2024%'
group by g.TRANSPORTATION, b.EF_VALUE;
--水費 	25,321.00
select sum(TOTAL_WATER) from WATER_BILL where FROM_BILLING_PERIOD like '%2024%'and FACTORY='南科廠' and METER_DIAMETER=75