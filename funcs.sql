-- 1
drop Function test_runs_by_systems;
CREATE FUNCTION test_runs_by_systems
(	
	@report_date date,   -- NOT NULL
	@system_type char(2) -- NULL
)
RETURNS TABLE 
AS
RETURN 
(
	
	select system_name,count(test_id) as testCount ,count(run_status) as runStatusCount,run_status 
	from systems 
		join tests on test_system_id = system_id 
		join runs on run_test_id = test_id
			where
			run_time > DATEADD(MONTH, -2 , @report_date) and 
			run_time < @report_date and
			system_type = CASE WHEN @system_type IS NULL THEN system_type ELSE @system_type END
			group by run_status,system_name

);
select * from test_runs_by_systems('2020-04-07',null)
