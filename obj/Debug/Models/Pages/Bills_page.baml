   M S B A M L   `   `   `  ����?  ;Rent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Rent.Models.PagesRent.Models.Pages  5      "    Rent.Models.Pages.Bills_page   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Rent.Models.Pages     5      -   5          N� Title$  
Bills_page��5	       N� 
Background
 �   �.��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Datagrid��5      
���   �$   HorizontalScrollBarVisibility$
 Auto=�5      "   VerticalScrollBarVisibility$
 Auto=�5         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5         MaxColumnWidth$	 167Q�5         
IsReadOnly .    CanUserSortColumns .  	   CanUserReorderColumns	 .  
   CanUserResizeColumns
 .     CanUserResizeRows .     HorizontalGridLinesBrush �#6b6b6b   VerticalGridLinesBrush �#6b6b6b���#121212$	��200��5      $	��500��5      $	��500��5      $	��Top=�5      $��Center=�5      $��0,50,0,0q�5         ColumnHeaderStyle 5       �� 5!       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5"      ��  �� Property � �� �� Value �#26252563   �� 5#       � �� �#bab8b863   �� 5$       � ��$
 auto��6/   �� 5%       � ��$ 20(�61   �� 5&       � ��$ 5q�60   5'         RowHeaderStyle 5)      �� 5*      :  4System.Windows.Controls.Primitives.DataGridRowHeader"  6   .�5+      ��  � �� �#12121263   �� 5,       � ��
 �����63   5-         RowStyle 5/      �� 50      )  #System.Windows.Controls.DataGridRow"  6   .�51      ��  � �� �#12121263   �� 52       � ��
 �����63   53      55      � 56   
   $��20��6   $	��500��6   $��Center=�6'   $	��Top=�6D   $��0,25,0,0q�6\   .+�57      � $��20��6   $	��200��6   $
��Left=�6+   .+�58      	� -   5        $��Not_paid_rb��58       	� 	GroupName$ Status��61   $��Не оплаченые��6D   ���#121212
���   �
�������$��11(�6�   $��20��6�   $	��120��6�   $
��Left=�6�   6�   	� 59      -   5        $��Paid_rb��59      $ Status��6-   $��Оплаченные��6@   ���#121212
���   �
�������$��11(�6�   $��20��6�   $��80��6�   $��Right=�6�   6�   5:      � 5;      $��20��6   $	��170��6   ���#121212$��Right=�6@   $	��Top=�6\   $��0,0,1,0q�6t   .+�5<      �� -   5         ��Name$ 	Search_tb��5<      $��15(�6-   ���#121212$��0q�6P   $
��Left=�6d    �� TextWrapping$
 Wrap=�6   $�� ��6�   $	��Top=�6�   $��20��6�   $	��150��6�   6  �� 5=      $��/Img/search_white.png��6   $��20��67   $��20��6C   $��Right=�6N   5>      5?      � 5@   
   -   5        $��Btn_grid��5@      $��50��6   $	��260��6+   $��Center=�67   $	��Top=�6T   $��	0,308,0,0q�6l   .+�5A      �� -   5         ��Name$ 
Delete_btn��5A      $��Отменить��6)   $
��Left=�6<   $	��Top=�6W   $��50��6o   $	��120��6{   ��5B      �� 5C      " ��6   .�5D      ��  � �� �#00a81667   �� 5E       � �� 5F      �� 5G   "    �� 
TargetType" ��62   .R�5H   &   ��  �� CornerRadius$ 20��6-   #��z��6?   ._�5I   *   �� $��Center=�6;   $��Center=�6X   5J   '   5K   #   5M       �� Triggers 5N      P� 5O       P� Property �@J�IsMouseOver P� Value . . �5P   "   ��  � �� �#008a126?   5Q      5S      5U      �� 5V      -   5        $ 	Enter_btn��5V      $��Оплачено��6(   $��Right=�6;   $	��Top=�6W   $��50��6o   $	��120��6{   ��5W      �� 5X      " ��6   .�5Y      ��  � �� �#00a81667   �� 5Z       � �� 5[      �� 5\   "   " ��62   .R�5]   &   �� $ 20��6-   #��z��6?   ._�5^   *   �� $��Center=�6;   $��Center=�6X   5_   '   5`   #   5b       5c      P� 5d       �@J�IsMouseOver . . �5e   "   ��  � �� �#008a126?   5f      5h      5j      5k      5l      5m      