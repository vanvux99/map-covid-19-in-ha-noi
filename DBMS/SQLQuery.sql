USE[COVID-19] 
GO 

--backup database [COVID-19] to disk = 'D:\_save\_db\COVID-19\backup_db_covid19.bak'
--restore database [COVID-19] from disk = 'D:\_save\_db\COVID-19\backup_db_covid19.bak'

-- Phân bố số ca mắc COVID-19 theo ngày tại Việt Nam tới ngày 28-08-2021
CREATE TABLE Table_SoCaMacCOVIDTheoNgay
(
	ID INT IDENTITY(1,1),
    Ngay DATE DEFAULT GETDATE(),
	CaNhiem INT DEFAULT 0,
	CaTuVong INT DEFAULT 0
)
GO 
--DROP TABLE dbo.Table_SoCaMacCOVIDTheoNgay
--SELECT * FROM dbo.Table_SoCaMacCOVIDTheoNgay




-- Số mắc COVID-19 theo ngày tại Thành phố Hà Nội từ ngày 27/4/2021 đến 28-08-2021
CREATE TABLE Table_CaNhiemBenhTaiHaNoi
(
	ID INT IDENTITY(1,1),
    Ngay DATE DEFAULT GETDATE(),
	CaNhiem INT DEFAULT 0
)
GO 
--DROP TABLE dbo.Table_CaNhiemBenhTaiHaNoi
--SELECT * FROM dbo.Table_CaNhiemBenhTaiHaNoi 



-- số ca dương tính với bệnh từ ngày 29-4 trong các huyện tại hà nội 
CREATE TABLE Table_SoCaMacBenhTheoHuyen_HaNoi
(
	ID INT IDENTITY(1,1),
	NgayMacBenh DATE DEFAULT GETDATE(),
	BaDinh INT DEFAULT 0,
	BaVi INT DEFAULT 0,
	BacTuLiem INT DEFAULT 0,
	CauGiay INT DEFAULT 0,
	ChuongMy INT DEFAULT 0,
	DanPhuong INT DEFAULT 0,
	DongAnh INT DEFAULT 0,
	DongDa INT DEFAULT 0,
	GiaLam INT DEFAULT 0,
	HaDong INT DEFAULT 0,
	HaiBaTrung INT DEFAULT 0,
	HoaiDuc INT DEFAULT 0,
	HoanKiem INT DEFAULT 0,
	HoangMai INT DEFAULT 0,
	LongBien INT DEFAULT 0,
	MeLinh INT DEFAULT 0,
	MyDuc INT DEFAULT 0,
	NamTuLiem INT DEFAULT 0,
	PhuXuyen INT DEFAULT 0,
	PhucTho INT DEFAULT 0,
	QuocOai  INT DEFAULT 0,
	SocSon INT DEFAULT 0,
	SonTay INT DEFAULT 0,
	TayHo INT DEFAULT 0,
	ThachThat INT DEFAULT 0,
	ThanhOai INT DEFAULT 0,
	ThanhTri INT DEFAULT 0,
	ThanhXuan INT DEFAULT 0,
	ThuongTin INT DEFAULT 0,
	UngHoa INT DEFAULT 0,
)
GO 
--DROP TABLE dbo.Table_SoCaMacBenhTheoHuyen_HaNoi
--SELECT * FROM dbo.Table_SoCaMacBenhTheoHuyen_HaNoi


-- Cũng là số ca mắc bệnh theo huyện, nhưng là tính theo từng ngày. -- do không thể pivot đc
CREATE TABLE Table_SoCaMacBenhTheoHuyen2_HaNoi
(
	ID INT IDENTITY(1,1),
    TenHuyen NVARCHAR(100)
)
GO 
--DROP TABLE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi
--SELECT * FROM dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi

INSERT INTO dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi ( TenHuyen )
VALUES
(N'Ba Dinh'), (N'Ba Vi'), (N'Bac Tu Liem'), (N'Cau Giay'),
(N'Chuong My'), (N'Dan Phuong'), (N'Dong Anh'), (N'Dong Da'),
(N'Gia Lam'), (N'Ha Dong'), (N'Hai Ba Trung'), (N'Hoai Duc'),
(N'Hoan Kiem'), (N'Hoang Mai'), (N'Long Bien'), (N'Me Linh'),
(N'My Duc'), (N'Nam Tu Liem'), (N'Phu Xuyen'), (N'Phuc Tho'),
(N'Quoc Oai'), (N'Soc Son'), (N'Son Tay'), (N'Tay Ho'),
(N'Thach That'), (N'Thanh Oai'), (N'Thanh Tri'), (N'Thanh Xuan'),
(N'Thuong Tin'), (N'Ung Hoa')
GO 


alter PROCEDURE Proc_CheckColumName --'abc', '01-01-1999', 2 "Proc_CheckColumName 'N_2021_08_29', '8/29/2021 12:00:00 AM', 2 "
    (@date AS NVARCHAR(100) = null,
	@Date_date datetime = null,
	@Select int = null)
AS 
BEGIN
	if @Select = 1
	begin
		set @Date_date = CONVERT(date, @Date_date)
		IF NOT EXISTS ( SELECT 1 FROM Table_SoCaMacBenhTheoHuyen_HaNoi WHERE NgayMacBenh = @Date_date )
		BEGIN
			INSERT INTO Table_SoCaMacBenhTheoHuyen_HaNoi (NgayMacBenh) VALUES (@Date_date)
		END
	end

	if @Select = 2
	begin
		IF COL_LENGTH( 'Table_SoCaMacBenhTheoHuyen2_HaNoi', @date ) IS NOT NULL  -- kiểm tra cột có tồn tại trong table không
		BEGIN 
			SELECT 'true'
		END   
		ELSE
		BEGIN
			SELECT 'false'
		END
	end
	
END
GO


-- viết proc ...
CREATE PROCEDURE Proc_UpdateTable_SoCaMacBenhTheoHuyen2_HaNoi
    @Date NVARCHAR(100),

	@CaNhiem_BaDinh INT = 0,
	@CaNhiem_BaVi INT = 0,
	@CaNhiem_BacTuLiem INT = 0,
	@CaNhiem_CauGiay INT = 0,
	@CaNhiem_ChuongMy INT = 0,
	@CaNhiem_DanPhuong INT = 0,
	@CaNhiem_DongAnh INT = 0,
	@CaNhiem_DongDa INT = 0,
	@CaNhiem_GiaLam INT = 0,
	@CaNhiem_HaDong INT = 0,
	@CaNhiem_HaiBaTrung INT = 0,
	@CaNhiem_HoaiDuc INT = 0,
	@CaNhiem_HoanKiem INT = 0,
	@CaNhiem_HoangMai INT = 0,
	@CaNhiem_LongBien INT = 0,
	@CaNhiem_MeLinh INT = 0,
	@CaNhiem_MyDuc INT = 0,
	@CaNhiem_NamTuLiem INT = 0,
	@CaNhiem_PhuXuyen INT = 0,
	@CaNhiem_PhucTho INT = 0,
	@CaNhiem_QuocOai  INT = 0,
	@CaNhiem_SocSon INT = 0,
	@CaNhiem_SonTay INT = 0,
	@CaNhiem_TayHo INT = 0,
	@CaNhiem_ThachThat INT = 0,
	@CaNhiem_ThanhOai INT = 0,
	@CaNhiem_ThanhTri INT = 0,
	@CaNhiem_ThanhXuan INT = 0,
	@CaNhiem_ThuongTin INT = 0,
	@CaNhiem_UngHoa INT = 0
AS
BEGIN
	-- thêm column theo từng ngày -- xử lý bên editor

	-- nếu có ca bệnh thì cập nhật vào ngày vừa tạo
	IF (@CaNhiem_BaDinh != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_BaDinh WHERE TenHuyen = 'Ba Dinh'
	END

	IF (@CaNhiem_BaVi != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_BaVi WHERE TenHuyen = 'Ba Vi'
	END

	IF (@CaNhiem_BacTuLiem != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_BacTuLiem WHERE TenHuyen = 'Bac Tu Liem'
	END

	IF (@CaNhiem_CauGiay != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_CauGiay WHERE TenHuyen = 'Cau Giay'
	END

	IF (@CaNhiem_ChuongMy != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_ChuongMy WHERE TenHuyen = 'Chuong My'
	END

	IF (@CaNhiem_DanPhuong != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_DanPhuong WHERE TenHuyen = 'Dan Phuong'
	END

	IF (@CaNhiem_DongAnh != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_DongAnh WHERE TenHuyen = 'Dong Anh'
	END

	IF (@CaNhiem_DongDa != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_DongDa WHERE TenHuyen = 'Dong Da'
	END

	IF (@CaNhiem_GiaLam != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_GiaLam WHERE TenHuyen = 'Gia Lam'
	END

	IF (@CaNhiem_HaDong != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_HaDong WHERE TenHuyen = 'Ha Dong'
	END

	IF (@CaNhiem_HaiBaTrung != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_HaiBaTrung WHERE TenHuyen = 'Hai Ba Trung'
	END

	IF (@CaNhiem_HoaiDuc != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_HoaiDuc WHERE TenHuyen = 'Hoai Duc'
	END

	IF (@CaNhiem_UngHoa != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_UngHoa WHERE TenHuyen = 'Ung Hoa'
	END

	IF (@CaNhiem_HoanKiem != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_HoanKiem WHERE TenHuyen = 'Hoan Kiem'
	END

	IF (@CaNhiem_HoangMai != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_HoangMai WHERE TenHuyen = 'Hoang Mai'
	END

	IF (@CaNhiem_LongBien != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_LongBien WHERE TenHuyen = 'Long Bien'
	END

	IF (@CaNhiem_MeLinh != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_MeLinh WHERE TenHuyen = 'Me Linh'
	END

	IF (@CaNhiem_MyDuc != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_MyDuc WHERE TenHuyen = 'My Duc'
	END

	IF (@CaNhiem_NamTuLiem != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_NamTuLiem WHERE TenHuyen = 'Nam Tu Liem'
	END

	IF (@CaNhiem_PhuXuyen != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_PhuXuyen WHERE TenHuyen = 'Phu Xuyen'
	END

	IF (@CaNhiem_PhucTho != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_PhucTho WHERE TenHuyen = 'Phuc Tho'
	END

	IF (@CaNhiem_QuocOai != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_QuocOai WHERE TenHuyen = 'Quoc Oai'
	END

	IF (@CaNhiem_SocSon != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_SocSon WHERE TenHuyen = 'Soc Son'
	END

	IF (@CaNhiem_SonTay != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_SonTay WHERE TenHuyen = 'Son Tay'
	END

	IF (@CaNhiem_TayHo != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_TayHo WHERE TenHuyen = 'Tay Ho'
	END

	IF (@CaNhiem_ThachThat != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_ThachThat WHERE TenHuyen = 'Thach That'
	END

	IF (@CaNhiem_ThanhOai != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_ThanhOai WHERE TenHuyen = 'Thanh Oai'
	END

	IF (@CaNhiem_ThanhTri != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_ThanhTri WHERE TenHuyen = 'Thanh Tri'
	END

	IF (@CaNhiem_ThanhXuan != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_ThanhXuan WHERE TenHuyen = 'Thanh Xuan'
	END

	IF (@CaNhiem_ThuongTin != 0)
	BEGIN
		UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET @Date = @CaNhiem_ThuongTin WHERE TenHuyen = 'Thuong Tin'
	END
END
GO