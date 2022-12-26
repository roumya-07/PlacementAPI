Create Table StudentDetails
(
Sl_No Int Primary Key Identity(1,1),
Roll_No Varchar(100),
Name Varchar(100),
DOB Date,
BranchID Int,
Passing_Year Varchar(20),
CGPA numeric(4,2),
BackLog Varchar(10),
);


Create Table Branch
(
BranchID Int Primary Key Identity(1,1),
BranchName Varchar(100)
);


Insert Into Branch
Values('B.Tech')

Insert Into Branch
Values('Diploma')

Insert Into Branch
Values('MCA')

Insert Into Branch
Values('MBA')


Create Table Department
(
DepartmentID Int Primary Key Identity(1,1),
BranchID Int,
DepartmentName Varchar(100)
);



Insert Into Department
Values(1,'CSE')

Insert Into Department
Values(1,'Mech')

Insert Into Department
Values(1,'EEE')

Insert Into Department
Values(1,'Agri')

Insert Into Department
Values(1,'EE')

Create table Passing_Year
(
YearID Int Primary Key Identity(1,1),
Year Varchar(10)
);


Alter Procedure SP_Admin_Student_OP
(
@Sl_No Int = 0,
@Roll_No Varchar(100)=null,
@Name Varchar(100)=null,
@DOB Date=null,
@BranchID Int=0,
@Passing_Year Varchar(20)=null,
@CGPA numeric(4,2)=0,
@BackLog Varchar(10)=null,
@Action Varchar(50)
)
As
Begin
If(@Action='FillTable')
Select A.Sl_No,A.Roll_No,A.Name,A.DOB,B.BranchName,A.Passing_Year,A.CGPA,A.BackLog From StudentDetails A, Branch B Where A.BranchID=B.BranchID
Else if(@Action='FillBranch')
Select * from Branch;
Else if(@Action='FillDepartment')
Select * from Department Where BranchID=@BranchID;
Else if(@Action='SelectOne')
Select * from StudentDetails Where Sl_No=@Sl_No;
Else if(@Action='InsertOrUpdateStudent')
Begin
If(@Sl_No=0)
Begin
Declare @BName Varchar(100);
Select @BName=BranchName From Branch Where BranchID=@BranchID
Insert into StudentDetails
values(((SELECT SUBSTRING(@BName, 1, 2))+'/'+(SELECT RIGHT(1000 + (Select Count(*) from StudentDetails)+1, 3))+'/'+(SELECT RIGHT(YEAR(getdate()),2))),@Name,@DOB,@BranchID,@Passing_Year,@CGPA,@BackLog);
End
Else
Update StudentDetails
Set Name=@Name,DOB=@DOB,BranchID=@BranchID,Passing_Year=@Passing_Year,CGPA=@CGPA,BackLog=@BackLog Where Sl_No=@Sl_No
End
Else if(@Action='Delete')
Delete From StudentDetails Where Sl_No=@Sl_No
End


Insert into StudentDetails
values(((SELECT SUBSTRING('CSE', 1, 2))+'/'+(SELECT RIGHT(1000 + (Select Count(*) from StudentDetails)+1, 3))+'/'+(SELECT RIGHT(YEAR(getdate()),2))),'Roumya Ranjan Behera','07/02/1997',1,(Select DATENAME(YEAR, GETDATE())),'9','No');


SELECT (YEAR(getdate())+1);
SELECT (YEAR(getdate())+2);

Declare @Count Int;
select @count=(Select Count(*) from Passing_Year)
Select @Count
if(@Count<4)
Insert Into Passing_Year
Values(YEAR(getdate())+(@Count+1));


Select * from Passing_Year

