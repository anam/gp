select (SUM(Debit)-SUM(Credit)) as Diff,ACC_JournalDetail.JournalMasterID
into #table
 from ACC_JournalDetail
 inner join 
ACC_JournalMaster on ACC_JournalDetail.JournalMasterID=ACC_JournalMaster.ACC_JournalMasterID
--where
-- (ACC_JournalMaster.JournalDate between '2013-01-01' and '2013-01-31')
--and 

group by ACC_JournalDetail.JournalMasterID

select * from #table 
where Diff<>0