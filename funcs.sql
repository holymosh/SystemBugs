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

--2
CREATE FUNCTION test_status_by_systems
(
	@system_id char(2) -- NOT NULL
)
RETURNS TABLE 
AS
RETURN 
(
select testMain.test_id,testMain.test_name,
testDuration.totalDuration,testLastStatus.run_status,
lastPass.run_time as lastPassRun,lastFail.run_time as lastFailRun,
passRate.passRate

from (select t.test_id,t.test_name from tests t where t.test_system_id = @system_id ) testMain 
join (select distinct r.run_test_id, SUM(r.run_duration) over (partition by r.run_test_id) as totalDuration from runs r) testDuration
	on testMain.test_id = testDuration.run_test_id
join (select distinct testStatus.run_test_id,testStatus.run_status from (select distinct lastrun.run_test_id,MAX(lastrun.run_time) over (partition by lastrun.run_test_id) as lastrun from runs lastrun) lastTime
	  join (select distinct testStatus.run_test_id , testStatus.run_status,testStatus.run_time from runs testStatus) testStatus
	  on testStatus.run_test_id = lastTime.run_test_id and testStatus.run_time = lastTime.lastrun
) testLastStatus on testMain.test_id = testLastStatus.run_test_id
join ( select distinct pass.run_test_id, MAX(pass.run_time) over (partition by pass.run_test_Id) as run_time from runs pass where pass.run_status='p' )
lastPass on lastPass.run_test_id = testMain.test_id
join ( select distinct pass.run_test_id, MAX(pass.run_time) over (partition by pass.run_test_Id) as run_time from runs pass where pass.run_status='f' )
lastFail on lastFail.run_test_id = testMain.test_id
join ( select passTotal.run_test_id, (passTotal.passCount / runTotals.runCount) as passRate  from 
		(select runTotals.run_test_id, COUNT(*) as runCount from runs runTotals group by runTotals.run_test_id) runTotals
	    join(select passCount.run_test_id, COUNT(*) as passCount from runs passCount where passCount.run_status = 'p' group by passCount.run_test_id)
		passTotal on passTotal.run_test_id = runTotals.run_test_id
) passRate on passRate.run_test_id = testMain.test_id
)

-- вычисляем по отдельности статусы, последние запуски и т. д. и присоединяем по id 
