   M S B A M L   `   `   `  ����G  CItalianPicza, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ItalianPiczaItalianPicza  5      5    /ItalianPicza.GUI_RegistrarTransaccionFinanciera   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      &localclr-namespace:ItalianPicza  5        N� Title$(  "GUI_RegistrarTransaccionFinanciera��5
      .��5      � .+�5   
   �� $
��Left=�6   $	��720��6/   
���   �$��Center=�6K   $	��230��6f   
�������6r   �� 5   
   $
��Left=�6   $��78��6/   $��
469,15,0,0q�6;   $	��Top=�6O   $	��616��6g   ���#231B1B �� RadiusX$ 25��6�    �� RadiusY$ 25��6�   
���  ��$��5��6�   �� 5   
   $��Registrar transacción��6   $
��Left=�60   $��60��6K   $��
581,27,0,0q�6W   $	��Top=�6k   $	��483��6�   
������ 
�������$��36(�6�   $��Rockwell Extra Bold)�6�   �� 5   
   $
��Left=�6   $��60��6+   $��
499,29,0,0q�67   $	��Top=�6K   $��56��6c   $!��/Imagenes/Logos/money 1.png��6n   �� 5   
   $
��Left=�6   $	��536��6/   
���   �$	��Top=�6K   $	��867��6c   
�������$��314,132,0,0q�6|   �� 5   
   $��Tipo de transacción:��6   $
��Left=�6/   $��32��6J   $��330,162,0,0q�6V   $	��Top=�6k   $	��246��6�   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5   
   -   5        $��cbTipoTransaccion��5      $
��Left=�6,   $��46��6G   $��606,155,0,0q�6S   $	��Top=�6h   $	��284��6�   $��16(�6�    "  MaterialDesignOutlinedComboBox#��[  6�   $��5,0,5,0q�6�   .@�5      �� $��Entrada��6   �� 5      $��Salida��6   5      �� 5   
   $��Fecha de realización:��6   $
��Left=�60   $��32��6K   $��330,241,0,0q�6W   $	��Top=�6l   $	��246��6�   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5   
   -   5        $
��Left=�5      $��63��6,   $��684,591,0,0q�68   $	��Top=�6M   $	��186��6e   ���#FF0000��5      �� 5       �� 
TargetType" ��6"   .R�5      �� #��z��6    �� CornerRadius$ 20��5      #��z��5      $��0q�5       �� Padding$ 10q�5       ._�5!      ��  �� Orientation$ 
Horizontal=�6%   $��Center=�6>   $��Center=�6[   .+�5"      �� $��Cancelar��6(   $��Center=�5#      
�������$
��Bold"�5%      $��22(�5&      5'      5(      5)      5+      �� 5,   
   -   5        $
��Left=�5,      $��62��6,   $��901,592,0,0q�68   $	��Top=�6M   $	��280��6e   ���#008000��5-      �� 5.      " ��6"   .R�5/      �� #��z��6   $ 20��50      #��z��51      $��0q�52      $ 10q�53      $��0,0,19,0q�6&   ._�54      �� $ 
Horizontal=�6%   $��Center=�6>   $��Center=�6[   .+�55      �� $��Agregar transacción��6(   $��Center=�56   !   
�������$
��Bold"�58   !   $��22(�59   !   5:      5;      5<      5>      (  "System.Windows.Controls.DatePicker  5?   
   -   5        $��cuadroFecha��5@      $
��Left=�5?      $��50��5A      $��606,232,0,0q�5B      $	��Top=�5C      $	��284��5D      
���   �$��1q�5F      
�������$��20(�5H      �� 5I   
   $��	Cantidad:��6   $
��Left=�6$   $��32��6?   $��453,323,0,0q�6K   $	��Top=�6`   $	��123��6x   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5J   
   -   5         ��Name$ cuadroTextoCantidad��5J      $
��Left=�6/   $��56��6J   $��606,311,0,0q�6V    �� TextWrapping$
 Wrap=�6k   $��20(�6   $	��Top=�6�   $	��284��6�    ! MaterialDesignOutlinedTextBox#��[ 6�   $��5,0,5,0q�6�   	 �� 	MaxLength$	 23��62  .��6A  )  #MaterialDesignThemes.Wpf.HintAssist
   Hint
 5K      �� 5L      $��Cantidad��6   
�������$��18(�6>   .��5M   .   5N      �� 5O   
   $��Descripción:��6   $
��Left=�6'   $��32��6B   $��426,401,0,0q�6N   $	��Top=�6c   $	��150��6{   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5P   
   -   5        $ cuadroTextoDescripcion��5P      $
��Left=�62   $	��170��6M   $��606,389,0,0q�6Z   $
 Wrap=�6o   $��20(�6�   $	��Top=�6�   $	��458��6�   #��[ 6�   $��5,0,5,0q�6�   $		 250��6�   .��6  
 5Q      �� 5R      $��Descripción��6   
�������$��18(�6A   .��5S   .   5T      �� 5U   
   $
��Left=�6   $��58��6/   $��330,588,0,0q�6;   $
��Wrap=�6P   $	��Top=�6d   $	��310��6|   {�.  �� 6�   $1�+Todos los campos presentes son obligatorios��6�   
���   �$��Segoe UI Black)�6�   $��18(�6  �� 6  $� ��6  �� 6   $�*��6$  
���  ��$��18(�6>  6O  5V      5W      