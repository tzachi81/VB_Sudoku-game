   M S B A M L   `   `   `  ����  	SUDUKU-v2    SUDUKU_v2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  
MainWindow��5      $	��450��6   $	��625��6&    � 
ResizeMode$ CanResizeWithGrip=�62   ��5      �� 5   
    �� EndPoint$ 1,0.5&�6    �� 
StartPoint$ 0,0.5&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $ White��6   $ 1Q�6)   5	      .��5      ��  �� Orientation$ Vertical=�6   .+�5   
   � $	��420��6    � RowDefinitions 5      �� 5      $��5 �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��5 �6     � ColumnDefinitions 5      �� 5      $��10 �6#   �� 5      $	��113 �6#   �� 5      $
��Auto �6#   .+�5      �� -   5        	 ��Name$	 btnPlay��5   q   $��3��6   $��center=�6"   $��80��6?   $��353,9,131,8q�6M   $
��Play��6b   $��2��6�   $��90��6�   
 � Cursor$

 Hand��6�   $y�Hidden=�6�   $��2��6�   �� 5      -   5        $	 btnHelp��5   w   $��3��6   $��center=�6"   $��80��6?   $��	13,3,10,3q�6M   $��Instructions��6`   $��1��6�   $��90��6�   $

 Hand��6�   �� 5      -   5        $	 btnFame��5   x   $��4��6   $��center=�6"   $��80��6?   $��
13,4,10,10q�6M   $��Hall of Fame��6a   $��1��6�   $��90��6�   $

 Hand��6�   �� 5       -   5        $	 btnExit��5    p   $��5��6   $��center=�6"   $��80��6?   $��
14,7,9,-34q�6M   $
��Exit��6a   $��1��6�   $��90��6�   $

 Hand��6�   �� 5!      $��2��6   $��10,0,134,15q�6$   $��1��69   $��SUDUKU3.jpg��6F   $
��None=�6[   $��2��6j   �� 5#      -   5         ��Name$
 lbl1��5#   p   $��5��6   $��center=�6"   $��80��6?   $��10,10,140,-105q�6M   $�� ��6e   $��2��6~   $	��424��6�   ���	#FFE4E2E2$y�Visible=�6�   $��5��6�   �� 5$      $��2��6   $��205,1,288,8q�6)   $ Vertical=�6>   $��3��6U   $��2��6b   .+�5%      �� $��choose level��6   $
��Left=�6/   $	��Top=�6J   	� 5&      -   5         	�Name$ 	btnRadio1��5&      $
��Easy��61   $
��Left=�6@   $��center=�6[   6v   	� 5'      -   5        $ 	btnRadio2��5'      $��Medium��61   $
��Left=�6B   $��center=�6]   6x   	� 5(      -   5        $ 	btnRadio3��5(      $
��Hard��61   $
��Left=�6@   $��center=�6[   6v   5)      �� 5*      $��2��6   $��
13,8,441,9q�6)   $ Vertical=�6=   $��3��6T   $��2��6a   .+�5+      �� $
��Left=�6   $
��Wrap=�67   $��Enter your Name:��6K   $	��Top=�6c   $��1,0,0,0q�6{   �� 5,      -	   5         ��Name$
 txt1��5,      $
��Left=�6(   $��23��6C    �� TextWrapping$
 Wrap=�6O   $�� ��6c   $	��Top=�6k   $	��120��6�   �� 5-      -
   5        $
 lbl2��5-      $�� ��6&   $
��Left=�61   $	��Top=�6L   $	��117��6d   ���	#FFFF0941$
��Bold"�6�   5/      �� 51      $��1��6   $
��Left=�6(   $��45,199,0,-214q�6C   $��5��6Z   $
��Wrap=�6g   $[��UThis Sudoku puzzle was made by Oshrat Asouline and Tzachi Halevy. All rights reserved��6{   $	��Top=�6�   $��2��6�   $��21��6  $	��508��6  ���	#FFDAB610���	#FF0F0D0D63  52      53      54      