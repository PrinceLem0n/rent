   M S B A M L   `   `   `  ����?  ;Rent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Rent.Models.WindowsRent.Models.Windows  5   	       Rent.Models.Windows.Log   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Rent.Models.Windows     5   	   -   5          � Title$  
MainWindow��5      $	��450��6$   $	��400��61    � WindowStartupLocation$ CenterScreen=�6T    � WindowStyle$
 None=�6y    � 
ResizeMode$ NoResize=�6�   .��5	      � 
���   � � RowDefinitions 5
   
   �� 5      $	��100 �6   �� 5      $��* �6   .+�5   
   � $��0��6   .+�5      �� -   5         ��Name$
 logo��5      $��/Img/logo1.png��6"   $��Center=�6:   $��80��6W   $��Center=�6c   $
��auto��6~   5      � 5   
   $��1��6   .+�5      �� -   5         ��Name$ Log_tb��5      ���#121212 �� 
CaretBrush
 �����$��0q�6N   $��Center=�6b   $��0,48,0,0q�6    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��320��6�   $��45��6�   $��34(�6�   $��Century)�6�   6  � 5      ���#121212$	��320��6(   $��45��64   $��	0,128,0,0q�6@   $��Center=�6S   $	��Top=�6p   .+�5      �� -   5        $ Pas_tb��5      ���#121212
 �����$��0q�6R   $
��Left=�6f   $
 Wrap=�6�   $	��Top=�6�   $	��275��6�   $��45��6�   $��34(�6�   $��Century)�6�   6   �� 5      -   5        $��open_eye��5      $��/Img/open_eye_white.png��6(   $��Right=�6I   $��45��6e   $��0q�6q   $	��Top=�6|   $
��auto��6�   	 � Cursor$
	 Hand��6�   6�   �� 5      -   5        $��	close_eye��5      $��/Img/close_eye_white.png��6)   $��Right=�6K   $��45��6g   $��0q�6s   $	��Top=�6~   $
��auto��6�   $
	 Hand��6�   6�   5      �� 5      -   5        
 ��Name$
 	Enter_btn��5      $��0q�6(   $��
Войти��6<   $��Center=�6L   $	��Top=�6i   $��	0,213,0,0q�6�   $��50��6�   $	��150��6�   $
	 Hand��6�   $��25(�6�   $��Century)�6�   $
��Bold"�6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value �#00a81667   �� 5       � �� 5      �� 5   "    �� 
TargetType" ��62   .R�5   &   ��  �� CornerRadius$ 20��6-   #��z��6?   ._�5    *   �� $��Center=�6;   $��Center=�6X   5!   '   5"   #   5$       �� Triggers 5%      P� 5&       P� Property �@J�IsMouseOver P� Value . . �5'   "   ��  � �� �#008a126?   5(      5*      5,      �� 5-      -   5         ��Name$ Reg_lb��5-      $%��Ещё нет аккаунта?��6$   $��Center=�6@   $��	0,293,0,0q�6]   $	��Top=�6p   
�������$
	 Hand��6�   $��Century)�6�   $��14(�6�   6
  5.      5/      50      