   M S B A M L   `   `   `  ����G  CItalianPicza, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ItalianPiczaItalianPicza  5           ItalianPicza.GUI_Productos   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      &localclr-namespace:ItalianPicza  5        N� Title$  GUI_Productos��5
       � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius	 �� SelectionBoxItem
 �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource F�Name �� IsDropDownOpen � AllowsTransparency K� IsItemsHost��%�  '��       �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � ��$ 4q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��
 �����6,   �� 5       � �� �#2B2B2B6+   �� 5       � ��$ 1q�60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �  5      �� 5   "   $��* �63   �� 5   "   $��30 �63   .+�5"      �� $ Border��5#      #��z��5$      #��z��5%      #��z��5&      $ 5��5'      $��2��5(      ._�5*   "   � .+�5,   &   �� $��4q�5-   %   $��Center=�5.   %   $
��Left=�5/   %   #��z	 50   %   51   #   52      d� 55      $��ToggleButton��57      $��1��56      
������ 
������ |�.  $
 Press=�5<       5;      �� $ IsDropDownOpen� �� *TemplatedParent+.��5=   "   F� $ Arrow��5>   !   
�������$��Center=�5@   !   $��Center=�5A   !   "����        � 	=  	= � z     	5B   !   5C      � 5F      $��Popup��6$   $��Bottom=�5G       #��z 5H        . |�.  $��Slide=�5K       .��5L   "   �� #��z��5M   !   
�������$��1q�5O   !   ._�5P   &   �� $��4q�63   .��5Q   *   ��  . 65   5R   '   5S   #   5T      5U      5V      5X      5Y      5Z      .��5\      � .+�5]   
   �� $
��Left=�6   $	��710��6/   
���   �$	��Top=�6K   $	��230��6c   ���#2B2B2B6o   �� 5_   
   $
��Left=�6   $��83��6/   
���   �$	��Top=�6J   $��92��6b   
�������$��
250,15,0,0q�6z   $��2��6�    �� RadiusX$ 20��6�    �� RadiusY$ 20��6�   �� 5`   
   $��Regresar��6   $
��Left=�6#   $��36��6>   $��
261,70,0,0q�6J   $	��Top=�6^   $��70��6v   
������ 
���   �$��16(�6�   �� 5a   
   -   5        $��240,10,922,630q�5a      $��80��6k   ��6>   Z� ��6T   Z� .��5b      �� $��/Imagenes/Uses/return.png��6   $
��Fill=�67   $��80��6F   $��70��6Q   5c      �� 5e   
   $
��Left=�6   $��78��6/   $��
580,15,0,0q�6;   $	��Top=�6O   $	��418��6g   ���#231B1B$ 25��6�   $ 25��6�   
���  ��$��5��6�   �� 5f   
   $��	Productos��6   $
��Left=�6$   $��59��6?   $��
713,29,0,0q�6K   $	��Top=�6_   $	��239��6w   
������ 
�������$��36(�6�   $��Rockwell Extra Bold)�6�   �� 5g   
   $
��Left=�6   $��70��6+   $��
605,19,0,0q�67   $	��Top=�6K   $��81��6c   $ ��Imagenes/Logos/users 3.png��6n   � 5i   
   $
��Left=�6   $��60��6*   $��330,-440,0,0q�66   $	��400��6L   .+�5k      �� -   5        $��cuadroTextoNombreEmpleado��5k      $��20(�5l      $��Center=�5m      
������� !  MaterialDesignOutlinedTextBox#��[  5o      $��5,0,55,0q�5p      $��40��6$   .��60   )  #MaterialDesignThemes.Wpf.HintAssist   Hint 5q      �� 5r      $��	Buscar...��6    
���   �$��18(�6D   .��5s   2   5t      '  !MaterialDesignThemes.Wpf.PackIcon  5w         Kind$ Magnify=�6&   $��Center=�5x      $��Right=�5y      $��0,0,10,0q�5z      $��24��5{      $��24��5|      
�������5}      5~      �� 5�   
   $
��Left=�6   $��38��6.   $��942,122,0,0q�6:   $	��Top=�6O   $	��231��6g   $��16(�6s    " MaterialDesignOutlinedComboBox#��[ 6�   $��5,0,5,0q�6�   
�������.@�5�      �� $��	Artículo��6   �� 5�      $��Insumo��6   5�      �� 5�   
   $
��Left=�6   $	��412��6/   $��304,170,0,0q�6<   
���   �$	��Top=�6`   $	��891��6x   ���#231B1B6�   �� 5�   
   $��Filtrar por:��6   $
��Left=�6'   $��44��6B   $��811,118,0,0q�6N   $	��Top=�6c   $	��126��6{   
�������$��24(�6�   �� 5�   
   -   5        $��lvProductos��5�      $��321,182,111,158q�6&   
���   ����	#E5000000$��Georgia)�5�   8   $��12(�6M   $��1,1,1,1q�6[    J� SnapsToDevicePixels .   5�      ��%  '��       �� 5�      " ��6   .�5�      ��  � ��
 �����63   �� 5�       � ��
 �   �63   �� 5�       � ��$ 16(�61   �� 5�       � ��$ Georgia)�63   �� 5�       � ��$
 Bold"�63   �� 5�       � ��$ Center=�6C   5�      5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��
 �����63   �� 5�       � ��
 �   �63   5�       �� View 5�      �� 5�      .P�5�      �� $�� ��6%    �� Width$	 150��60    �� CellTemplate 5�      �� 5�      .R�5�   "   �� $	��110��6B   $	��110��6N   ��6(   �� *imagen+6N   5�      5�      �� 5�      $��Nombre��6%   $	 250��65    5�      �� 5�      .R�5�   "   �� 
���   �$��Center=�5�      $��Center=�5�      $��20(�5�      $��Georgia)�5�      ��5�   ,   �� *nombre+5�      5�      5�      �� 5�      $��	Caducidad��6%   $	 150��68    5�      �� 5�      .R�5�   "   �� 
���   �$��Center=�5�   %   $��Center=�5�   %   $��24(�5�   %   ��5�   ,   �� *	caducidad+5�   %   5�      5�      �� 5�      $��Medida��6%   $	 150��65    5�      �� 5�      .R�5�   "   �� 
���   �$��Center=�5�   %   $��Center=�5�   %   $��18(�5�   %   $��Georgia)�5�   %   $
��Wrap=�5�   %   $	��280��5�   %   ��5�   ,   �� *medida+5�   %   5�      5�      �� 5�      $�� ��6%   $	 250��60    5�      �� 5�      .R�5�   "   ��  �� Orientation$ Vertical=�6-   $��Center=�6D   $��Center=�6a   .+�5�   &   �� $��Modificar artículo��6-   $	��120��6J   $��40��6V   
��� ���$��0,0,0,10q�6y   5�   #   5�      5�      5�      5�      �� 5�   
   $
��Left=�6   $��63��6,   $��616,600,0,0q�68   $	��Top=�6M   $	��280��6e   ���#008000��5�      �� 5�      " ��6"   .R�5�      �� #��z��6   $ 20��5�      #��z��5�      $��0q�5�       �� Padding$ 10q�5�      ._�5�      �� $ 
Horizontal=�6%   $��Center=�6>   $��Center=�6[   .+�5�        $
 Plus=�66   $��Center=�5�      
�������$��24��5�      $��24��5�      �� 5�      $��Agregar producto��6(   $��Center=�5�      
�������$
��Bold"�5�      $��22(�5�      5�      5�      5�      5�      5�      5�      