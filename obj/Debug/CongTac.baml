   M S B A M L   `   `   `  ����C  ?QLNHANSU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:QLNHANSUQLNHANSU  5          QLNHANSU.CongTac   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:QLNHANSU     5      .��5
      � 
�������.+�5   
   � $��72��6   $��3,-380,3,400q�6   ���	#FF72CCF6  � ColumnDefinitions  5      �� 5      $��1* �6#   �� 5      $��1* �6#   �� 5      $��0.75* �6#   �� 5      $
��Auto �6#    � RowDefinitions 5      �� 5      $
��Auto �6    .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0��62   $��Center=�6B   $��	10,0,10,0q�6]   $��60��6p   .+�5      �� $��Mã Nhân Viên:��6   $	��162��61   $��Center=�6=   $
��Bold"�6X   $��20(�6j   $��50,5,0,0q�6x   $��28��6�   �� 5      -   5         ��Name$ txtMaNhanVien��5      $	��125��61   $��44��6=   $��20(�6I   $��0,10,0,0q�6W   5      �� 5      $ 
Horizontal=�6   $��1��62   $��Center=�6B   $��	10,0,10,0q�6]   $��60��6p   .+�5      �� $��Mã HĐLĐ:��6   $	��120��6,   $��Center=�68   $
��Bold"�6S   $��20(�6e   $��40,5,0,0q�6s   �� 5       -   5        $ txtMaHD��5       $	��221��6+   $��44��67   $��20(�6C   $��0,10,0,0q�6Q   5!      �� 5$      -   5         ��Name$ 
btnTimKiem��5$      $��2��6)   ���	#FF0F4DCE
�������$��0q�6c   $	��243��6w   $��44��6�   $��Center=�6�   $
��Left=�6�   $��	19,10,0,0q�6�   .��5%      �� $ 
Horizontal=�6   .+�5&      �� $��Tìm kiếm��6    $��Center=�60   
�������$��20(�6^   $
��Bold"�6l   5'      5)      �� 5*      $ 
Horizontal=�6   $��3��62   $��5,0,5,0q�6B   $��Center=�6S   $��64��6n   .+�5+      �� $��Trạng thái:��6   $��Center=�6/   $��5,15,5,5q�6J   $��20(�6\   $
��Bold"�6j   $��28��6|   �� 5,      -   5         ��Name$ cbTrangThai��5,      $	��129��60   $��5,15,5,5q�6<   $��44��6N   $��20(�6�   $
��Bold"�6�   .@�5-      �� $��Chức vụ mới��6#   �� 5.      $��Thôi chức vụ��6#   5/      50      52      &   System.Windows.Controls.DataGrid  53   
   -   5          Name$ CongTacDataGrid��53         AutoGenerateColumns .  $��	0,71,0,10q�55      $��20(�6   $
��Bold"�6,      Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Mã Nhân Viên��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
MaNhanVien+6Z     58      $	 
Mã HĐLĐ��6%   $
 * 6O    66   �� *MaHD+6O     59      $	 Mã Phòng Ban��6%   $
 * 6Z    6;   �� *
MaPhongBan+6Z     5:      $	 Mã Bộ Phận��6%   $
 * 6V    69   �� *
MaBoPhan+6V     5;      $	 Mã Chức Vụ��6%   $
 * 6V    69   �� *
MaChucVu+6V     5<      $	 Ngày Quyết Định��6%   $
 * 6`    6>   �� *NgayQuyetDinh+6`     5=      $	 Tình Trạng Công Tác��6%   $
 * 6g    6B   �� *TinhTrangCongTac+6g   5?      5A      5B      