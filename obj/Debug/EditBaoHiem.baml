   M S B A M L   `   `   `  ����C  ?QLNHANSU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:QLNHANSUQLNHANSU  5   	       QLNHANSU.EditBaoHiem   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:QLNHANSU     5   	   c _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5   	     � Title$'  !Chỉnh sửa Bảo Hiểm Y Tế��5	   	   $	��500��6)   $	��700��66    � WindowStartupLocation$ CenterScreen=�6B   .��5
      � .+�5   
   ��  �� CornerRadius$ 20��6    �� Padding$ 20q�6#   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5      �  � Color$ 	#FF72CCF6��6#    � Offset$ 0Q�65   � 5      $ 	#FF72CCF6��6#   $ 1Q�65   5      ._�5      �  � RowDefinitions 5      �� 5      $��60 �6$   �� 5      $	��100 �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   �� 5"      $��80 �6$   .+�5'      �� ���	#FF0F4DCE$ 	20,20,0,0��60   $��-20,-21,-20,2q�6I   ._�5(      �� $��Bảo Hiểm Y Tế��6    $��24(�65   $
��Bold"�6C   
�������$��Center=�6h   $��Center=�6�   5)      �� 5,      	 �� Orientation$	 
Horizontal=�6   $��1��66   $��	0,50,0,10q�6C   .+�5-      �� $��Số bảo hiểm y tế:��6    $	��180��69   $��Center=�6E   $��16(�6`   $
��Bold"�6n   �� 5.      -   5        
 ��Name$
 	txtSoBHYT��5.      $	��400��61   $��44��6=   $��16(�6I    �� 
IsReadOnly . 6W   5/      �� 52      $	 
Horizontal=�6   $��2��66   $
��0,10q�6C   .+�53      �� $��Ngày cấp:��6    $	��180��61   $��Center=�6=   $��16(�6X   $
��Bold"�6f   (  "System.Windows.Controls.DatePicker  54      -   5          Name$ 	dpNgayCap��54   !   $	��400��64   $��44��6@   $��20(�6L   55      �� 58      $	 
Horizontal=�6   $��3��66   $
��0,10q�6C   .+�59      �� $��Giá trị sử dụng:��6    $	��180��68   $��Center=�6D   $��16(�6_   $
��Bold"�6m     5:      -   5        $ txtGiaTriSuDung��5:   !   $	��400��6:   $��44��6F   $��16(�6R   5;      �� 5>      $	 
Horizontal=�6   $��4��66   $
��0,10q�6C   .+�5?      �� $��Ngày hết hạn:��6    $	��180��65   $��Center=�6A   $��16(�6\   $
��Bold"�6j     5@      -   5        $ dpNgayHetHan��5@   !   $	��400��67   $��44��6C   $��16(�6O   5A      �� 5D      $	 
Horizontal=�6   $��5��66   $
��0,10q�6C   .+�5E      �� $��Nơi khám bệnh:��6    $	��180��66   $��Center=�6B   $��16(�6]   $
��Bold"�6k   �� 5F      -   5        $
 txtNoiKhamBenh��5F      $	��400��66   $��43��6B   $��16(�6N   5G      �� 5J      $��6��6   $	 
Horizontal=�6*   $��Center=�6C   $
��0,20q�6`   .+�5K      �� -   5        $��Lưu Thay Đổi��5K      $	��150��64   $��40��6@   $��16(�6L   ���	#FF0F4DCE
�������$��0q�6�   6�   5L      5M      5N      5O      5P      