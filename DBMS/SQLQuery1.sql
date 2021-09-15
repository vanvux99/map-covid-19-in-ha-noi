use [COVID-19]
go 

create table Table_CaBenhTheoNgay
(
	ID int identity(1,1),
	TenHuyen nvarchar(100),
	CaDuongTinhCuaHuyen int default 0,
	Ngay date default getdate(),
)
go

insert into Table_CaBenhTheoNgay (TenHuyen, CaDuongTinhCuaHuyen, Ngay) values (N'Ha Noi', null, null )
go 

--select * from  Table_CaBenhTheoNgay
--drop table Table_CaBenhTheoNgay

create proc Proc_CRUDTable_CaBenhTheoNgay
(
	@TenHuyen nvarchar(100) = null,
	@CaDuongTinhCuaHuyen int = 0,
	@Ngay date = null,
	@Select int = null
)
as
begin
	if (@Select = 1)
	begin
		insert into Table_CaBenhTheoNgay (TenHuyen, CaDuongTinhCuaHuyen, Ngay)
		values (@TenHuyen, @CaDuongTinhCuaHuyen, @Ngay)

		if (@@ROWCOUNT < 1) select 'false'
	end

	if (@Select = 2)
	begin
		update Table_CaBenhTheoNgay set CaDuongTinhCuaHuyen = @CaDuongTinhCuaHuyen where TenHuyen = @TenHuyen and Ngay = @Ngay
	end
end
go

alter proc Proc_CheckRowIdentityTable_CaBenhTheoNgay ( @TenHuyen nvarchar(100) = null, @Ngay date = null )
as
begin
	DECLARE @CountRow int = ( select count(ID) from Table_CaBenhTheoNgay where TenHuyen = @TenHuyen and Ngay = @Ngay )
	if (@CountRow = 1)
	begin 
		select 'true'
	end
	else 
	begin 
		select 'false'
	end
end
go 
