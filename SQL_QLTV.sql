create database QL_THUVIEN
go 

use QL_THUVIEN
go

create table KhoaHoc
(
	MaKH varchar(10) not null primary key,
	TenKH nvarchar(30)not null
)
go

create table HocSinh
(
	MaHS int not null primary key,
	HoTenHS nvarchar(50)not null,
	GioiTinh bit not null,
	NgaySinh date not null,
	DiaChi nvarchar(100)not null,
	DienThoai varchar(11),
	GhiChu nvarchar(100)
)
go

create table TacGia
(
	MaTG int not null primary key,
	HoTenTG nvarchar(50)not null	
)
go

create table TheLoaiSach
(
	MaTL varchar(20)not null primary key,
	TenTL nvarchar(50)not null
)
go

create table HocSinhKhoaHoc
(
	MaHS int not null references HocSinh(MaHS),
	MaKH varchar(10) not null references KhoaHoc(MaKH),
	NamBD int not null,
	NamKT int not null,
	primary key(MaHS,MaKH)
)
go

create table Sach
(
	MaSach varchar(10)not null primary key,
	MaTL varchar(20)not null references TheLoaiSach(MaTL),
	TenSach nvarchar(50)not null,
	SoLuong int not null,
	GiaSach int not null,
	NgayXuatBan date not null
)
go



create table SachTacGia
(
	MaTG int not null references TacGia(MaTG),
	MaSach varchar(10) not null references Sach(MaSach),
	primary key(MaTG,MaSach)
)
go

create table ChiTietMuonSach
(
	MaMuonSach int not null primary key,
	MaSach varchar(10) not null references Sach(MaSach),
	MaHS int not null references HocSinh(MaHS),
	SLMuon int not null,
	NgayMuon date not null,
	NgayHenTra date not null,
	NgayTra varchar(11)
)
go

set dateformat dmy

insert into KhoaHoc(MaKH,TenKH) values('K10',N'Niên khoá 2019'),
										('K11',N'Niên khoá 2020'),
										('K12',N'Niên khoá 2021'),
										('K13',N'Niên khoá 2022')
go

insert into HocSinh(MaHS,HoTenHS,GioiTinh,NgaySinh,DiaChi,DienThoai,GhiChu)
values('101',N'Nguyễn Quang Phúc',1,'08/11/2002',N'Gia Lai','0923567489',N''),
	  ('102',N'Nguyễn Thị Ngọc Trinh',0,'08/04/2002',N'Bình Định','0382536271',N''),
	  ('103',N'Phạm Huy Hoàng',1,'',N'','0382536271',N'')
go

insert into TacGia(MaTG,HoTenTG)
values(1,N'Charles Darwin'),
	  (2,N'Shannon Thomas'),
	  (3,N'J.R.R.Tolkien'),
	  (4,N' Stephen Hawking')
go

insert into TheLoaiSach(MaTL,TenTL) values('KH',N'Khoa học'),('TLH',N'Tâm lí học'),('TT',N'Truyền thuyết')
go

insert into Sach(MaSach,MaTL,TenSach,SoLuong,GiaSach,NgayXuatBan)values
				('KH001','KH',N'Nguồn gốc các loài',10,165000,'10/03/2012'),
				('KH002','KH',N'Lược sử thời gian',10,260000,'10/03/2012'),
				('TLH001','TLH',N'Thao túng tâm lý',6,186000,'02/06/2010'),
				('TT001','TT',N'Chúa tể của những chiếc nhẫn',2,189000,'18/12/2015')
go

insert into SachTacGia(MaTG,MaSach)values(1,'KH001'),(2,'TLH001'),(3,'TT001'),(4,'KH002')
go

set dateformat dmy

insert into ChiTietMuonSach(MaMuonSach,MaSach,MaHS,SLMuon,NgayMuon,NgayHenTra,NgayTra)values
						   (1,'KH001','102',2,'08/03/2022','18/03/2022','')						   
go

create table NhanVien
(
	MaNV int IDENTITY(1,1) primary key  not null,
	HoTen nvarchar(50)not null,
	TaiKhoan varchar(30)not null,
	MatKhau varchar(50)not null,
	LoaiTK int not null
)

set dateformat dmy
insert into NhanVien(HoTen,TaiKhoan,MatKhau,LoaiTK)values(N'Nguyễn Thị Ngọc Trinh','admin','admin',1),
															(N'ABC','abc','1234',2)
go
-----------------------------------------------------------------------------------------------------------
--Hàm
create function PSP_TacGia_Select (@MaTG int)
returns table
as
return (select * from TacGia)
go

create function PSP_HocSinh_Select (@MaHS int)
returns table
as
return (select * from HocSinh)
go 

create proc PSP_TacGia_Select1 
as
	select MaTG from TacGia
go			

 create function PSP_TheLoaiSach_Select (@matl varchar(20) , @tentl nvarchar(50))
 returns table
 as
	return (select MaTL,TenTL from TheLoaiSach)
 go

 create proc PSP_TheLoaiSach_Select1
 as
	select MaTL from TheLoaiSach
 go

create function PSP_Sach_Show (@TenSach nvarchar(50))
returns table 
as
	return (Select Sach.MaSach, GiaSach, SoLuong, NgayXuatBan, HoTenTG
			from Sach right join SachTacGia on Sach.MaSach=SachTacGia.MaSach
				right join TacGia on SachTacGia.MaTG=TacGia.MaTG
				where TenSach = @TenSach)
go

create function PSP_ShowTenSach ( @TenSach nvarchar(50))
returns table
as
	return (select TenSach
				from Sach)
go

create function PSP_CTMuonSach_Select (@MaMuonSach int)
returns table
as
return 
	(Select * From ChiTietMuonSach)
go

-----------------------------------------------------------------------------------------
--Thủ tục
create proc PSP_TacGia_Select
as
select * from TacGia
go

create proc PSP_TacGia_Select1
as
	select MaTG from TacGia
go

create proc PSP_HocSinh_Select
as
select * from HocSinh
go 
 
 create proc PSP_TheLoaiSach_Select
 as
	select MaTL,TenTL from TheLoaiSach
 go
 
 create proc PSP_TheLoaiSach_Select1
 as
	select MaTL from TheLoaiSach
 go

 create proc PSP_Sach_Show
@TenSach nvarchar(50)
as
	Select Sach.MaSach, GiaSach, SoLuong, NgayXuatBan, HoTenTG
	from Sach right join SachTacGia on Sach.MaSach=SachTacGia.MaSach
	right join TacGia on SachTacGia.MaTG=TacGia.MaTG
	where TenSach = @TenSach
go

create proc PSP_ShowTenSach
as
	select TenSach
	from Sach
go


create proc PSP_CTMuonSach_Select
as
	Select * From ChiTietMuonSach
go

create proc PSP_TacGia_Insert
@MaTG int ,
@HoTenTG nvarchar (50)
as
begin 
insert into TacGia(MaTG,HoTenTG)
values (@MaTG,@HoTenTG)
end		

create proc PSP_TacGia_Update
@MaTG int ,
@HoTenTG nvarchar (50)
as
	update TacGia
	set HoTenTG=@HoTenTG
	where MaTG=@MaTG
go
  
create proc PSP_TacGia_Delete
@MaTG int
as
	delete from TacGia
	where MaTG=@MaTG
go

create proc PSP_HocSinh_Insert
@MaHS int,
@HoTenHS nvarchar(50),
@GioiTinh bit,
@NgaySinh date,
@DiaChi nvarchar(100),
@DienThoai  varchar (11),
@GhiChu nvarchar(100)
as
begin
	insert into HocSinh(MaHS,HoTenHS,GioiTinh,NgaySinh,DiaChi,DienThoai,GhiChu)
	values (@MaHS,@HoTenHS,@GioiTinh,@NgaySinh,@DiaChi,@DienThoai,@GhiChu)
end
		
create proc PSP_HocSinh_Update
@MaHS int,
@HoTenHS nvarchar(50),
@GioiTinh bit,
@NgaySinh date,
@DiaChi nvarchar(100),
@DienThoai  varchar (11),
@GhiChu nvarchar(100)
as
begin
	update HocSinh
	set HoTenHS=@HoTenHS,GioiTinh=@GioiTinh,
	NgaySinh=@NgaySinh,DiaChi=@DiaChi,DienThoai=@DienThoai,GhiChu=@GhiChu
	where MaHS=@MaHS
end

create proc PSP_HocSinh_Delete
@MaHS int
as
begin
delete from HocSinh
where MaHS=@MaHS
end

create proc PSP_SachTacGia_Insert
@matg int,
@masach varchar(10)
as
begin
	insert into SachTacGia(MaTG,MaSach)
	values (@matg,@masach)
end

create proc PSP_QuanLySach_Insert
@masach varchar(10),
@matl varchar(20),
@tensach nvarchar (50),
@soluong int,
@giasach int,
@ngayxuatban date
as
begin
	
    insert into Sach(MaSach,MaTL,TenSach,SoLuong,GiaSach,NgayXuatBan)
    values (@masach, @matl,@tensach,@soluong,@giasach,@ngayxuatban)   
end

create proc PSP_NhanVien_test
@taikhoan varchar(30),
@matkhau varchar(30)
as
	select TaiKhoan,MatKhau from NhanVien
	where  TaiKhoan=@taikhoan and MatKhau=@matkhau
go

create proc PSP_TheLoaiSach_Insert
 
 @matl varchar(20),
 @tentl nvarchar(50)
  as
	insert into TheLoaiSach(MaTL,TenTL)
	values (@matl,@tentl)
  go
  
create proc PSP_TheLoaiSach_Delete
  @matl varchar (20)
  as
	delete from TheLoaiSach
	where MaTL=@matl
  go
  
 create proc PSP_TheLoaiSach_Update
 @matl varchar(20),
 @tentl nvarchar(50)
 as
 begin
	update TheLoaiSach
	set TenTL=@tentl
	where MaTL=@matl
 end
 go

 select MAX(MaHS) from HocSinh

create proc PSP_HocSinh_Show
as
	select HoTenHS
	from HocSinh
go
create proc	PsP_MaHS_Select	
@HoTenHS nvarchar(50)
as
	Select MaHS
	From HocSinh Where HoTenHS=@HoTenHS
go
create proc SP_IDCTMuon_Max
as
	select COUNT(MaMuonSach)+1 as MaMuonSach from ChiTietMuonSach
go

create proc PSP_ChiTietMuon_Insert
@MaMuonSach int,
@MaSach varchar(10), 
@MaHS int, 
@SLMuon int,
@NgayMuon date, 
@NgayHenTra date, 
@NgayTra varchar(11)
as
	insert into ChiTietMuonSach(MaMuonSach,MaSach,MaHS,SLMuon,NgayMuon,NgayHenTra,NgayTra) 
	values(@MaMuonSach,@MaSach,@MaHS,@SLMuon,@NgayMuon,@NgayHenTra,@NgayTra)
go

create proc PSP_QuanLySach_Search
@search nvarchar(20)
as
	select Sach.MaSach,TenSach, TenTL,HoTenTG,SoLuong,GiaSach,NgayXuatBan from Sach left join SachTacGia 
	on Sach.MaSach=SachTacGia.MaSach left join TacGia
	on SachTacGia.MaTG=TacGia.MaTG left join TheLoaiSach
	on Sach.MaTL=TheLoaiSach.MaTL
	where TheLoaiSach.TenTL like('%'+@search+'%') or HoTenTG like('%'+@search+'%') or TenSach like('%'+@search+'%')
go


create proc PSP_SachDelete
@MaSach varchar(10)
as
	delete from Sach
	where MaSach=@MaSach
go

create proc  PSP_NhanVien_Update
@TaiKhoan varchar(30),
@MatKhau varchar(30)
as
	begin 
		update NhanVien
		set  MatKhau=@MatKhau
		where TaiKhoan=@TaiKhoan
	end
go

create proc PSP_NhanVien_Show
as
	select TaiKhoan from NhanVien 
go

create proc PSP_NhanVien_test1
@matkhau varchar(30)
as
	select MatKhau from NhanVien
	where  MatKhau=@matkhau
go

create proc PSP_NhanVien_Loai
@TaiKhoan varchar(30)
as
	select LoaiTK from NhanVien
	where TaiKhoan=@TaiKhoan
go

create proc PSP_NhanVien_Insert

@HoTen nvarchar(50),
@TaiKhoan varchar(30),
@MatKhau varchar(30),
@LoaiTK int
as
	insert into NhanVien(HoTen,TaiKhoan,MatKhau,LoaiTK)
	values (@HoTen,@TaiKhoan,@MatKhau,@LoaiTK)
go

create proc PSP_ThongKeSachCon
as
	select 
		Sach.MaSach, MaTL, TenSach, HoTenTG, SoLuong, GiaSach, NgayXuatBan,
		isnull(AVG(SoLuong)-SUM(SLMuon),SoLuong)as SLConLai
	from Sach left join ChiTietMuonSach
	on Sach.MaSach=ChiTietMuonSach.MaSach
	inner join SachTacGia on Sach.MaSach=SachTacGia.MaSach
	inner join TacGia on SachTacGia.MaTG=TacGia.MaTG
	group by Sach.MaSach,MaTL,TenSach,HoTenTG,SoLuong,GiaSach,NgayXuatBan
	order by TenSach asc
go

create proc PSP_SLConLai
@MaSach varchar(10)
as
	select isnull(AVG(SoLuong)-SUM(SLMuon),SoLuong)as SLConLai
	from Sach left join ChiTietMuonSach
	on Sach.MaSach=ChiTietMuonSach.MaSach
	where Sach.MaSach=@MaSach
	group by Sach.MaSach,SoLuong
go

create proc PSP_Update_TraSach
@MaMuon int,
@SLTra int,
@NgayTra varchar(11)
as
	update ChiTietMuonSach
	set SLMuon=SLMuon-@SLTra, NgayTra=@NgayTra
	where MaMuonSach=@MaMuon
go

create proc PSP_SLTra
@MaMuon int
as
	select SLMuon
	from ChiTietMuonSach where MaMuonSach=@MaMuon
go

create proc PSP_UpdateSach
@MaSach varchar(10), 
@MaTL varchar(20), 
@TenSach nvarchar(10), 
@SoLuong int, 
@GiaSach int, 
@NgayXuatBan date
as
	update Sach
	set MaTL=@MaTL, TenSach=@TenSach, SoLuong=@SoLuong, GiaSach=@GiaSach, NgayXuatBan=@NgayXuatBan
	where MaSach=@MaSach
go

create proc PSP_UpdateSachTacGia
@MaTG int, 
@MaSach varchar(10)
as
	update SachTacGia
	set MaTG=@MaTG
	where MaSach=@MaSach
go

create proc PSP_CheckTaiKhoanTonTai
@TaiKhoan varchar(30)
as
	if exists(select TaiKhoan from NhanVien where TaiKhoan=@TaiKhoan)
		select 1 as TT
	else
		select 0 as TT
go
 --------------------------------------------------------------
 ---Trigger
CREATE TRIGGER t_MuonSach ON ChiTietMuonSAch AFTER INSERT
AS BEGIN
UPDATE Sach
SET SoLuong = SoLuong - (SELECT SLMuon FROM inserted WHERE MaSach = Sach.MaSach)
						FROM Sach JOIN inserted ON Sach.MaSach = inserted.MaSach
END

CREATE TRIGGER t_HuyMuonSach ON ChiTietMuonSAch FOR DELETE
AS BEGIN
UPDATE Sach
SET SoLuong = SoLuong + (SELECT SLMuon FROM deleted WHERE MaSach = Sach.MaSach)
FROM Sach JOIN deleted ON Sach.MaSach = deleted.MaSach
END

CREATE TRIGGER t_CapNhatMuonSach ON ChiTietMuonSAch AFTER UPDATE
AS BEGIN
UPDATE Sach
SET SoLuong = SoLuong -(SELECT SLMuon FROM inserted WHERE MaSach = Sach.MaSach) +
						(SELECT SLMuon FROM deleted WHERE MaSach = Sach.MaSach)
						FROM Sach JOIN deleted ON Sach.MaSach = deleted.MaSach
END

CREATE TRIGGER tg_trasach  on ChiTietMuonSAch FOR INSERT, UPDATE
as
	DECLARE @MaMuonSach int,@NgayMuon datetime,
	@NgayHenTra datetime
	--Trường hợp thêm mới
	IF NOT EXISTS (SELECT * FROM deleted)
	BEGIN
		SELECT @MaMuonSach = MaMuonSach,@NgayMuon = NgayMuon
		FROM inserted
		SELECT @NgayMuon = @NgayMuon
		FROM ChiTietMuonSach
		WHERE MaMuonSach=@MaMuonSach
		IF @NgayHenTra<@NgayMuon
		BEGIN
		RAISERROR (N'Ngày hẹn trả phải sau ngày mượn',16,1)
		ROLLBACK
		RETURN
	END
	IF DATEDIFF(DD, @NgayMuon, @NgayHenTra)> 30
	BEGIN
		RAISERROR (N'Ngày hẹn trả - ngày đặt <= 30 ngày',16,1)
		ROLLBACK
		RETURN
		END
	END
	ELSE
	-- Trường hợp sửa
	BEGIN
		IF UPDATE(NgayHenTra)
		BEGIN
			SELECT @MaMuonSach = MaMuonSach,@NgayHenTra = NgayHenTra
			FROM inserted
			SELECT NgayMuon = @NgayMuon
			FROM ChiTietMuonSach
			WHERE MaMuonSach=@MaMuonSach
		IF @NgayHenTra<@NgayMuon
		BEGIN
		RAISERROR (N'Ngày hẹn trả phải sau ngày mượn',16,1)
		ROLLBACK
		RETURN
		END
			IF DATEDIFF(DD, @NgayMuon, @NgayHenTra)> 30
			BEGIN
			RAISERROR (N'Ngày hẹn trả - ngày  mượn <= 30 ngày',16,1)
			ROLLBACK
			RETURN
			END	
		END
END