   M S B A M L   `   `   `  ����C  ?QLNHANSU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:QLNHANSUQLNHANSU  5          QLNHANSU.QuanLyBangLuong   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:QLNHANSU     5      c _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      .��5
      � 
�������.+�5   
   � $��72��6   $��3,-380,3,400q�6   ���	#FF72CCF6  � ColumnDefinitions  5      �� 5      $	��300 �6#   �� 5      $	��300 �6#   �� 5      $	��300 �6#   �� 5      $
��Auto �6#   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0��62   $��Center=�6B   $��
-28,0,26,0q�6]   $��44��6q   .+�5      �� $��MANV:��6   $
��Auto��6)   $��Center=�66   $
��Bold"�6Q   $��20(�6c   $��50,0,0,0q�6q   �� 5      -   5         ��Name$ txtMaNhanVien��5   ,   $��0,7,0,0q�6   $	��200��6C   $��44��6O   $��20(�6[   $��5,0,0,0q�6i   6z   5      �� 5      $ 
Horizontal=�6   $��1��62   $��5,0,5,0q�6B   $��Center=�6S   $��54��6n   .+�5      �� $��Tháng:��6   $��Center=�6*   $��5q�6E   $��20(�6P   $
��Bold"�6^   �� 5      -   5         ��Name$ cbThang��5      $	��200��6,   $��5q�68   $��44��6C   $��20(�6{   $
��Bold"�6�   .@�5      �� $��1��6#   �� 5       $��2��6#   �� 5!      $��3��6#   �� 5"      $��4��6#   �� 5#      $��5��6#   �� 5$      $��6��6#   �� 5%      $��7��6#   �� 5&      $��8��6#   �� 5'      $��9��6#   �� 5(      $��10��6#   �� 5)      $��11��6#   �� 5*      $��12��6#   5+      5,      �� 5.      $ 
Horizontal=�6   $��2��62   $��5,0,5,0q�6B   $��Center=�6S   $��54��6n   .+�5/      �� $��Năm:��6   $��Center=�6(   $��5q�6C   $��20(�6N   $
��Bold"�6\   �� 50      -   5        $ cbNam��50      $	��200��6*   $��5q�66   $��44��6A   $��20(�6w   $
��Bold"�6�   .@�51      �� $
��2023��6#   �� 52      $
��2024��6#   �� 53      $
��2025��6#   54      55      �� 5;      $ 
Horizontal=�6   $��3��62   $��5,0,5,0q�6B   $��Center=�6S   $��54��6n   .+�5<      �� $��Mức Lương :��6   $��Center=�6/   $��5q�6J   $��20(�6U   $
��Bold"�6c   �� 5=      -   5        $ 
cbMucLuong��5=      $	��180��6/   $��5q�6;   $��44��6F   $��20(�6�   $
��Bold"�6�   .@�5>      �� $	��A-Z��6#   �� 5?      $	��Z-A��6#   5A      5B      5C      &   System.Windows.Controls.DataGrid  5F   
   -   5          Name$ TangCaDataGrid��5F         AutoGenerateColumns .  $��
3,71,10,10q�5H      $��20(�6'   $
��Bold"�65   $
��Auto=�5I      $
��Auto=�5J      $��4��6C      Columns 5K      0  *System.Windows.Controls.DataGridTextColumn  5L      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã Bảng Lương��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6\   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6<   �� *MaBangLuong+6\     5M      $ Mã Nhân Viên��6%   $ * 6T   	 6;   �� *MANV+6T     5N      $ Tháng��6%   $ * 6N   	 64   �� *Thang+6N     5O      $
 Năm��6%   $ * 6J   	 62   �� *Nam+6J     5P      $ Lương Cơ Bản��6%   $ * 6Z   	 6;   �� *
LuongCoBan+6Z     5Q      $ Lương Tăng Ca��6%   $ * 6\   	 6<   �� *LuongTangCa+6\     5R      $ Phụ Cấp��6%   $ * 6Q   	 66   �� *PhuCap+6Q     5S      $ Tiền Trừ��6%   $ * 6T   	 67   �� *
TIENBTRU+6T     5T      $ Tổng Lương��6%   $ * 6W   	 69   �� *	TongLuong+6W   5W      5Y      5Z      