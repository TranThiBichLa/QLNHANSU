   M S B A M L   `   `   `  ����C  ?QLNHANSU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:QLNHANSUQLNHANSU  5          QLNHANSU.XttTangCa   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:QLNHANSU     5      $	��810��5      $
��1245��6   .��5	      � 
�������$
��1245��6   $	��810��6,   .+�5   
   ��   �� CornerRadius$  20��6    �� Padding$ 20q�6#   $��
13,0,70,10q�60   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5      �  � Color$ 	#FF72CCF6��6#    � Offset$ 0Q�65   � 5      $ 	#FF0F4DCE��6#   $ 1Q�65   5      ._�5      �  � ColumnDefinitions 5      �� 5      $
��577* �6'   �� 5      $
��545* �6'    � RowDefinitions 5      �� 5      $��80 �6$   �� 5      $��80 �6$   �� 5      $��80 �6$   �� 5      $��80 �6$   �� 5      $��80 �6$   �� 5       $��80 �6$   �� 5!      $��* �6$   .+�5%      �� ���	#FF0F4DCE$ 10q�60   $��-20,-20,345,10q�6=   $  	20,0,20,0��6U   ._�5&      �� $��Thông Tin Tăng Ca��6    $��20(�69   $
��Bold"�6G   
������� �� TextAlignment$ Center=�6l   $��Center=�6�   5'      &   System.Windows.Controls.DataGrid  5*      -   5        	  Name$	 dgHDLD��5*      $��1��6+   $��6��68   $��2��6I   
   AutoGenerateColumns
 .     
IsReadOnly . $��20(�6�   $
��Bold"�6�      Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã Tăng Ca��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *
MaTangCa+6^     5.      $ Ngày tăng Ca��6-   $	 300 6b    6C   �� *
NgayTangCa+6b     5/      $ Số Giờ Tăng Ca��6-   $	 190 6e    6E   �� *SoGioTangCa+6e     50      $ Mã Loại Ca��6-   $	 150 6^    6A   �� *
MaLoaiCa+6^     51      $ Hệ Số Loại Ca��6-   $	 150 6d    6D   �� *
HeSoLoaiCa+6d     52      $ Lương Tăng Ca ��6-   $	 150 6e    6E   �� *LuongTangCa+6e   54      55      56      57      58      