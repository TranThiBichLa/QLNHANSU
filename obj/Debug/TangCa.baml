   M S B A M L   `   `   `  ����C  ?QLNHANSU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:QLNHANSUQLNHANSU  5          QLNHANSU.TangCa   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:QLNHANSU     5      c _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      .��5
      � 
�������.+�5   
   � $��72��6   $��3,-380,3,400q�6   ���	#FF72CCF6  � ColumnDefinitions  5      �� 5      $
��366* �6#   �� 5      $
��366* �6#   �� 5      $
��235* �6#   �� 5      $
��Auto �6#   $��271.998��60   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0��62   $��Center=�6B   $��
-28,0,26,0q�6]   $��44��6q   .+�5      �� $��Mã Nhân Viên:��6   $	��162��61   $��Center=�6=   $
��Bold"�6X   $��20(�6j   $��50,0,0,0q�6x   �� 5      -   5         ��Name$ txtMaNhanVien��5   ,   $��0,7,0,0q�6   $	��157��6C   $��44��6O   $��20(�6[   5      �� 5      $ 
Horizontal=�6   $��Center=�62   $��
337,0,10,0q�6M   $��2��6a   $��44��6u   .+�5      �� $��Ngày Tăng Ca:��6   $	��151��61   $��Center=�6=   $
��Bold"�6X   $��20(�6j   $��40,0,0,0q�6x   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpNgayTangCa��5   /   $��0,7,0,0q�6   $	��199��6E   $��44��6Q   $��20(�6]   5      �� 5       -   5         ��Name$ 
btnTimKiem��5       $��2��6)   ���	#FF0F4DCE
�������$��0q�6c   $	��165��6w   $��44��6�   $��Center=�6�   $��Center=�6�   .��5!      �� $ 
Horizontal=�6   .+�5#      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$ Magnify=�6,   $��20��6;   $��20��6F   $��	10,0,10,0q�6R   $��Center=�6e   
�������6�   �� 5$      $��Tìm kiếm��6    $��Center=�60   
�������$��20(�6^   $
��Bold"�6l   5%      5&      �� 5*      $ 
Horizontal=�6   $��3��62   $��5,0,5,0q�6B   $��Center=�6S   $��54��6n   .+�5+      �� $��
Loại Ca:��6   $��Center=�6,   $��5q�6G   $��20(�6R   $
��Bold"�6`   �� 5,      -   5         ��Name$ cbLoaiCa��5,      $	��129��6-   $��5q�69   $��44��6D   $��20(�6}   $
��Bold"�6�   .@�5-      �� $��
Tất cả��6#   �� 5.      $��Ngày��6#   �� 5/      $��Đêm��6#   �� 50      $��Ngày nghỉ��6#   �� 51      $��
Ngày lễ��6#   52      53      54      &   System.Windows.Controls.DataGrid  57   
   -   5          Name$ TangCaDataGrid��57         AutoGenerateColumns .  $��
3,71,10,10q�59      $��20(�6'   $
��Bold"�65   	   Columns	 5:      0  *System.Windows.Controls.DataGridTextColumn  5;      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Mã Nhân Viên��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
MaNhanVien+6Z     5<      $
 Mã Tăng Ca��6%   $ * 6V    69   �� *
MaTangCa+6V     5=      $
 Ngày Tăng Ca��6%   $ * 6Z    6;   �� *
NgayTangCa+6Z     5>      $
 
Số Giờ��6%   $ * 6O    65   �� *SoGio+6O     5?      $
 Lương 1 Giờ��6%   $ * 6^    6:   �� *Luong1GioTangCa+6^     5@      $
 	Loại Ca��6%   $ * 6T    66   �� *	TenLoaiCa+6T     5A      $
 Hệ Số Loại Ca��6%   $ * 6[    6<   �� *
HeSoLoaiCa+6[     5B      $
 Lương Tăng Ca��6%   $ * 6\    6<   �� *LuongTangCa+6\   5E      5G      5H      